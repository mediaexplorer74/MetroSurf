// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.EmbeddedContentLoader

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;

#nullable disable
namespace MetroLab.Common
{
  public class EmbeddedContentLoader : ContentLoader
  {
    private static volatile EmbeddedContentLoader _current;
    private KeyValuePairsQueue<object, string> _elementsQueue = new KeyValuePairsQueue<object, string>();
    private readonly object InitializeLocker = new object();
    private volatile Task _initializeTask;
    public static readonly DependencyProperty SourceUrlProperty = DependencyProperty.RegisterAttached("SourceUrl", typeof (string), typeof (EmbeddedContentLoader), new PropertyMetadata((object) null, new PropertyChangedCallback(EmbeddedContentLoader.Current.OnSourceUriPropertyChanged)));
    private readonly object PendingUnloadedElementsQueueLocker = new object();
    private List<object> _pendingUnloadedElementsQueue = new List<object>();

    public static EmbeddedContentLoader Current
    {
      get
      {
        return EmbeddedContentLoader._current ?? (EmbeddedContentLoader._current = new EmbeddedContentLoader());
      }
    }

    public Task Initialize()
    {
      if (this._initializeTask == null)
      {
        lock (this.InitializeLocker)
        {
          if (this._initializeTask == null)
            return this._initializeTask = this.InitializeInner();
        }
      }
      return this._initializeTask;
    }

    private async Task InitializeInner()
    {
      this._elementsQueue = new KeyValuePairsQueue<object, string>();
      this._elementsQueue.ProcessItem += new ProcessItemHandler<object, string>(this.ElementsQueue_ProcessItem);
      this._elementsQueue.StartWorker();
    }

    // Simplified implementation for MVP: attempt to set source on UI element from packaged file path
    public async Task<bool> SetCachedSource(object element, string fileRelativePath)
    {
      if (fileRelativePath == null)
        throw new ArgumentNullException(nameof(fileRelativePath));

      // Build ms-appx URI for packaged content
      var uriString = fileRelativePath.StartsWith("ms-appx:/", StringComparison.OrdinalIgnoreCase) ? fileRelativePath : (fileRelativePath.StartsWith("/") ? "ms-appx://" + fileRelativePath : "ms-appx:///" + fileRelativePath);
      var localUri = new Uri(uriString);

      if (element is FrameworkElement fe)
      {
        var tcs = new TaskCompletionSource<bool>();
        await fe.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
          try
          {
            // Image
            if (fe is Image img)
            {
              img.Source = new BitmapImage(localUri);
              tcs.SetResult(true);
              return;
            }

            // MediaElement
            if (fe is MediaElement me)
            {
              me.Source = localUri;
              tcs.SetResult(true);
              return;
            }

            // Try reflection: property Source (Uri or ImageSource)
            var type = element.GetType();
            var prop = type.GetRuntimeProperty("Source");
            if (prop != null && prop.CanWrite)
            {
              if (prop.PropertyType == typeof(Uri))
              {
                prop.SetValue(element, localUri);
                tcs.SetResult(true);
                return;
              }
              if (typeof(ImageSource).GetTypeInfo().IsAssignableFrom(prop.PropertyType.GetTypeInfo()))
              {
                prop.SetValue(element, new BitmapImage(localUri));
                tcs.SetResult(true);
                return;
              }
            }

            tcs.SetResult(false);
          }
          catch (Exception)
          {
            tcs.SetResult(false);
          }
        });
        return await tcs.Task;
      }

      return false;
    }

    private async Task ElementsQueue_ProcessItem(
      object sender,
      ProcessItemEventArgs<object, string> args)
    {
      try
      {
        object key = args.Key;
        string fileRelativePath = args.Value;
        if (string.IsNullOrEmpty(fileRelativePath))
          return;
        int num = await this.SetCachedSource(key, fileRelativePath) ? 1 : 0;
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    public static string GetSourceUrl(DependencyObject frameworkControl)
    {
      return (string) frameworkControl.GetValue(EmbeddedContentLoader.SourceUrlProperty);
    }

    public static void SetSourceUrl(DependencyObject frameworkControl, string value)
    {
      frameworkControl.SetValue(EmbeddedContentLoader.SourceUrlProperty, (object) value);
    }

    protected async void OnSourceUriPropertyChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      string newServerPath = (string) e.NewValue;
      FrameworkElement element = (FrameworkElement) o;
      if (ContentLoader.GetRemoveOldContentWhenLoading((DependencyObject) element))
        ContentLoader.GetHelper((object) element).RemoveContent();
      try
      {
        element.Unloaded -= this.ElementOnUnloaded;
      }
      catch { }
      element.Unloaded += this.ElementOnUnloaded;
      await this.RemoveAllQueuedElements();
      await this.RemoveElement((object) element);
      this._elementsQueue.Enqueue((object) element, newServerPath);
      this.Log.Info("Ссылка {0} добавлена в очередь на скачивание (сработало SourceUriPropertyChanged)", (object) newServerPath);
    }

    protected void ElementOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement frameworkElement1 = (FrameworkElement) sender;
      FrameworkElement frameworkElement2 = frameworkElement1;
      try { frameworkElement2.Loaded -= this.ElementOnLoaded; } catch { }
      frameworkElement2.Loaded += this.ElementOnLoaded;
      frameworkElement1.Unloaded -= this.ElementOnUnloaded;
      lock (this.PendingUnloadedElementsQueueLocker)
        this.PendingUnloadedElementsQueue.Add((object) frameworkElement1);
    }

    private async void ElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement element = (FrameworkElement) sender;
      element.Loaded -= this.ElementOnLoaded;
      if (!await ContentLoader.GetHelper((object) element).IsSourceEmptyAsync())
        return;
      await this.RemoveAllQueuedElements();
      await this.RemoveElement((object) element);
      string sourceUrl = EmbeddedContentLoader.GetSourceUrl((DependencyObject) element);
      this._elementsQueue.Enqueue((object) element, sourceUrl);
      this.Log.Info("Ссылка {0} добавлена в очередь на скачивание (ElementOnLoaded called)", (object) sourceUrl);
    }

    private List<object> PendingUnloadedElementsQueue => this._pendingUnloadedElementsQueue;

    protected async Task RemoveAllQueuedElements()
    {
      List<object> unloadedElementsQueue;
      lock (this.PendingUnloadedElementsQueueLocker)
      {
        unloadedElementsQueue = this.PendingUnloadedElementsQueue;
        this._pendingUnloadedElementsQueue = new List<object>();
      }
      if (unloadedElementsQueue == null)
        return;
      foreach (object element in unloadedElementsQueue)
        await this.RemoveElement(element);
    }

    private async Task RemoveElement(object element)
    {
      await this.Initialize();
      this._elementsQueue.Remove(element);
    }
  }
}
