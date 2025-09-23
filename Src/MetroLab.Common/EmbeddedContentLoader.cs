// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.EmbeddedContentLoader
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;

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

    public async Task<bool> SetCachedSource(object element, string fileRelativePath)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EmbeddedContentLoader.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new EmbeddedContentLoader.\u003C\u003Ec__DisplayClass8_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.element = element;
      if (fileRelativePath == null)
        throw new ArgumentNullException(nameof (fileRelativePath));
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.task = new TaskCompletionSource<bool>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.localUri = Package.Current.InstalledLocation.Path + "\\" + fileRelativePath.Replace('/', '\\');
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClass80.action = new Action<object>(cDisplayClass80.\u003CSetCachedSource\u003Eb__0);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass80.element is FrameworkElement element1)
      {
        // ISSUE: method pointer
        await ((DependencyObject) element1).Dispatcher.RunAsync((CoreDispatcherPriority) -1, new DispatchedHandler((object) cDisplayClass80, __methodptr(\u003CSetCachedSource\u003Eb__1)));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass80.action(cDisplayClass80.element);
      }
      // ISSUE: reference to a compiler-generated field
      return await cDisplayClass80.task.Task;
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
      WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(element.remove_Unloaded), new RoutedEventHandler(this.ElementOnUnloaded));
      FrameworkElement frameworkElement = element;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Unloaded), new Action<EventRegistrationToken>(frameworkElement.remove_Unloaded), new RoutedEventHandler(this.ElementOnUnloaded));
      await this.RemoveAllQueuedElements();
      await this.RemoveElement((object) element);
      this._elementsQueue.Enqueue((object) element, newServerPath);
      this.Log.Info("Ссылка {0} добавлена в очередь на скачивание (сработало SourceUriPropertyChanged)", (object) newServerPath);
    }

    protected void ElementOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement frameworkElement1 = (FrameworkElement) sender;
      FrameworkElement frameworkElement2 = frameworkElement1;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement2.add_Loaded), new Action<EventRegistrationToken>(frameworkElement2.remove_Loaded), new RoutedEventHandler(this.ElementOnLoaded));
      WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(frameworkElement1.remove_Unloaded), new RoutedEventHandler(this.ElementOnUnloaded));
      lock (this.PendingUnloadedElementsQueueLocker)
        this.PendingUnloadedElementsQueue.Add((object) frameworkElement1);
    }

    private async void ElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement element = (FrameworkElement) sender;
      WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(element.remove_Loaded), new RoutedEventHandler(this.ElementOnLoaded));
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
