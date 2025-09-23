// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ContentCache
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLab.Common
{
  public class ContentCache : ContentLoader
  {
    private const int LoadingAttempts = 5;
    private readonly object _resumePulsar = new object();
    private volatile int _pauseCount;
    private static volatile ContentCache _current;
    public static readonly DependencyProperty SourceUrlProperty = DependencyProperty.RegisterAttached("SourceUrl", typeof (string), typeof (ContentCache), new PropertyMetadata((object) null, new PropertyChangedCallback(ContentCache.Current.OnSourceUriPropertyChanged)));
    public static readonly DependencyProperty AlwaysUpdateProperty = DependencyProperty.RegisterAttached("AlwaysUpdate", typeof (bool), typeof (ContentCache), new PropertyMetadata((object) false, new PropertyChangedCallback(ContentCache.Current.OnAlwaysUpdatePropertyChanged)));
    private readonly object _alwaysUpdateElementsLocker = new object();
    private readonly HashSet<DependencyObject> _alwaysUpdateElements = new HashSet<DependencyObject>();
    private readonly object UriesAndElementsLocker = new object();
    private readonly Dictionary<ContentCache.CacheFileInfo, List<object>> UriesAndObjects = new Dictionary<ContentCache.CacheFileInfo, List<object>>();
    private readonly object ObjectsLocker = new object();
    private readonly Dictionary<object, ContentCache.CacheFileInfo> Objects = new Dictionary<object, ContentCache.CacheFileInfo>();
    private readonly object PendingUnloadedElementsQueueLocker = new object();
    private List<object> _pendingUnloadedElementsQueue = new List<object>();
    private readonly object CachedFilesLocker = new object();
    private readonly HashSet<string> CachedFiles = new HashSet<string>();
    private StorageFolder _cacheFolder;
    private KeyValuePairsQueue<object, string> _elementsQueue = new KeyValuePairsQueue<object, string>();
    private readonly object InitializeLocker = new object();
    private volatile Task _initializeTask;
    private readonly object ServerFilesPathsQueueLocker = new object();
    private readonly List<ContentCache.CacheFileInfo> ServerFilesPathsQueue = new List<ContentCache.CacheFileInfo>();
    private readonly object ProblemFilesPathsLocker = new object();
    private readonly List<ContentCache.CacheFileInfo> ProblemFilesPaths = new List<ContentCache.CacheFileInfo>();
    private static readonly Random Rand = new Random();

    protected virtual string BinaryCacheFolderName => "BinaryCache";

    protected virtual int ThreadCount => 10;

    public void Pause()
    {
      lock (this)
        this._pauseCount = 1;
    }

    public void Resume()
    {
      lock (this)
      {
        this._pauseCount = 0;
        lock (this._resumePulsar)
          Monitor.PulseAll(this._resumePulsar);
      }
    }

    public bool IsPaused => this._pauseCount > 0;

    public static ContentCache Current
    {
      get => ContentCache._current ?? (ContentCache._current = new ContentCache());
    }

    public IServerSubstitution ServerSubstitution { get; set; }

    public static string GetSourceUrl(DependencyObject frameworkControl)
    {
      return (string) frameworkControl.GetValue(ContentCache.SourceUrlProperty);
    }

    public static void SetSourceUrl(DependencyObject frameworkControl, string value)
    {
      frameworkControl.SetValue(ContentCache.SourceUrlProperty, (object) value);
    }

    public static bool GetAlwaysUpdate(DependencyObject obj)
    {
      return (bool) obj.GetValue(ContentCache.AlwaysUpdateProperty);
    }

    public static void SetAlwaysUpdate(DependencyObject obj, bool value)
    {
      obj.SetValue(ContentCache.AlwaysUpdateProperty, (object) value);
    }

    private void OnAlwaysUpdatePropertyChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs args)
    {
      int num = (bool) args.OldValue ? 1 : 0;
      bool newValue = (bool) args.NewValue;
      FrameworkElement frameworkElement = (FrameworkElement) dependencyObject;
      if (num != 0)
      {
        if (newValue)
          return;
        lock (this._alwaysUpdateElementsLocker)
        {
          if (!this._alwaysUpdateElements.Contains((DependencyObject) frameworkElement))
            return;
          this._alwaysUpdateElements.Remove((DependencyObject) frameworkElement);
        }
      }
      else
      {
        if (!newValue)
          return;
        lock (this._alwaysUpdateElementsLocker)
        {
          if (this._alwaysUpdateElements.Contains((DependencyObject) frameworkElement))
            return;
          this._alwaysUpdateElements.Add((DependencyObject) frameworkElement);
        }
      }
    }

    protected async void OnSourceUriPropertyChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      string newServerPath = (string) e.NewValue;
      if (newServerPath != null && !ContentCache.IsServerPathCorrect(newServerPath))
        throw new InvalidDataException("SourceUri is not correct");
      FrameworkElement element = (FrameworkElement) o;
      if (ContentLoader.GetRemoveOldContentWhenLoading((DependencyObject) element))
        ContentLoader.GetHelper((object) element).RemoveContent();

      // Use standard event subscribe/unsubscribe
      try { element.Unloaded -= this.ElementOnUnloaded; } catch { }
      element.Unloaded += this.ElementOnUnloaded;

      await this.RemoveAllQueuedElements();
      await this.RemoveElement((object) element);
      this._elementsQueue.Enqueue((object) element, newServerPath);
      this.Log.Info("Ссылка {0} добавлена в очередь на скачивание (сработало SourceUriPropertyChanged)", (object) newServerPath);
    }

    protected void ElementOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement frameworkElement1 = (FrameworkElement) sender;
      // Re-subscribe to Loaded when unloaded
      try { frameworkElement1.Loaded -= this.ElementOnLoaded; } catch { }
      frameworkElement1.Loaded += this.ElementOnLoaded;

      try { frameworkElement1.Unloaded -= this.ElementOnUnloaded; } catch { }
      lock (this.PendingUnloadedElementsQueueLocker)
        this.PendingUnloadedElementsQueue.Add((object) frameworkElement1);
    }

    private async void ElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      FrameworkElement element = (FrameworkElement) sender;
      try { element.Loaded -= this.ElementOnLoaded; } catch { }
      if (!await ContentLoader.GetHelper((object) element).IsSourceEmptyAsync())
        return;
      await this.RemoveAllQueuedElements();
      await this.RemoveElement((object) element);
      string sourceUrl = ContentCache.GetSourceUrl((DependencyObject) element);
      this._elementsQueue.Enqueue((object) element, sourceUrl);
      this.Log.Info("Ссылка {0} добавлена в очередь на скачивание (ElementOnLoaded called)", (object) sourceUrl);
    }

    private Task<bool> SetCachedSource(object element, ContentCache.CacheFileInfo cacheFileInfo)
    {
      if (cacheFileInfo == null || cacheFileInfo.LocalUri == null)
        throw new ArgumentNullException(nameof(cacheFileInfo));

      Uri localUri = cacheFileInfo.LocalUri;

      if (element is FrameworkElement fe)
      {
        var tcs = new TaskCompletionSource<bool>();
        var ignore = fe.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        {
          try
          {
            // Image
            if (fe is Windows.UI.Xaml.Controls.Image img)
            {
              img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(localUri);
              tcs.SetResult(true);
              return;
            }

            // MediaElement
            if (fe is Windows.UI.Xaml.Controls.MediaElement me)
            {
              me.Source = localUri;
              tcs.SetResult(true);
              return;
            }

            // Try reflection: property Source (Uri or ImageSource)
            var prop = element.GetType().GetProperty("Source");
            if (prop != null)
            {
              if (prop.PropertyType == typeof(Uri) && prop.CanWrite)
              {
                prop.SetValue(element, localUri);
                tcs.SetResult(true);
                return;
              }
              if (typeof(Windows.UI.Xaml.Media.ImageSource).IsAssignableFrom(prop.PropertyType) && prop.CanWrite)
              {
                prop.SetValue(element, new Windows.UI.Xaml.Media.Imaging.BitmapImage(localUri));
                tcs.SetResult(true);
                return;
              }
            }

            // No-op fallback
            tcs.SetResult(false);
          }
          catch (Exception)
          {
            tcs.SetResult(false);
          }
        });
        return tcs.Task;
      }

      // Non-UI element - no-op
      return Task.FromResult(false);
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
      {
        await this.RemoveElement(element);
        if (element is DependencyObject)
        {
          lock (this._alwaysUpdateElementsLocker)
            this._alwaysUpdateElements.Remove((DependencyObject) element);
        }
      }
    }

    private async Task RemoveElement(object element)
    {
      await this.Initialize();
      this._elementsQueue.Remove(element);
      ContentCache.CacheFileInfo key;
      lock (this.ObjectsLocker)
      {
        if (!this.Objects.ContainsKey(element))
          return;
        key = this.Objects[element];
        this.Objects.Remove(element);
      }
      lock (this.UriesAndElementsLocker)
      {
        if (!this.UriesAndObjects.ContainsKey(key))
          return;
        this.UriesAndObjects[key].Remove(element);
        if (this.UriesAndObjects[key].Count != 0)
          return;
        this.UriesAndObjects.Remove(key);
        lock (this.ServerFilesPathsQueueLocker)
          this.ServerFilesPathsQueue.Remove(key);
      }
    }

    private static bool IsServerPathCorrect(string serverPath)
    {
      if (serverPath == null)
        throw new ArgumentNullException(nameof (serverPath));
      return serverPath.StartsWith("http://") || serverPath.StartsWith("https://") || serverPath.StartsWith("lazylens://");
    }

    public void ReportCachedFilesDeleted()
    {
      lock (this.CachedFilesLocker)
        this.CachedFiles.Clear();
      lock (ContentLoader.SystemImageCacheHelper.Cache)
        ContentLoader.SystemImageCacheHelper.Cache.Clear();
    }

    private bool IsFileCached(ContentCache.CacheFileInfo cacheFileInfo)
    {
      lock (this.CachedFilesLocker)
        return this.CachedFiles.Contains(cacheFileInfo.LocalFileName);
    }

    public bool IsFileCached(string serverPath)
    {
      if (serverPath == null)
        throw new ArgumentNullException(nameof (serverPath));
      return ContentCache.IsServerPathCorrect(serverPath) ? ContentCache.Current.IsFileCached(new ContentCache.CacheFileInfo(serverPath, ContentCache.Current)) : throw new ArgumentException("serverPath is not correct", nameof (serverPath));
    }

    public event EventHandler<ContentCache.FileJustCachedEventArgs> FileJustCached;

    protected virtual void OnFileJustCached(ContentCache.FileJustCachedEventArgs e)
    {
      EventHandler<ContentCache.FileJustCachedEventArgs> fileJustCached = this.FileJustCached;
      if (fileJustCached == null)
        return;
      fileJustCached((object) this, e);
    }

    private void ReportFileJustCached(ContentCache.CacheFileInfo cacheFileInfo)
    {
      if (this.CachedFiles.Contains(cacheFileInfo.LocalFileName))
        return;
      this.CachedFiles.Add(cacheFileInfo.LocalFileName);
      this.OnFileJustCached(new ContentCache.FileJustCachedEventArgs(cacheFileInfo.ServerPath));
    }

    private void ReportCacheForFileIsBroken(ContentCache.CacheFileInfo cacheFileInfo)
    {
      lock (this.CachedFilesLocker)
        this.CachedFiles.Remove(cacheFileInfo.LocalFileName);
    }

    private async Task<string> UpdateCachedFileAsync(
      Stream streamOfFileToCache,
      ContentCache.CacheFileInfo cacheFileInfo)
    {
      try
      {
        using (StorageStreamTransaction transactedWrite = await (await this._cacheFolder.CreateFileAsync(cacheFileInfo.LocalFileName, (CreationCollisionOption) 1)).OpenTransactedWriteAsync())
        {
          using (Stream streamForRead = streamOfFileToCache)
          {
            IRandomAccessStream outputStream = transactedWrite.Stream;
            Stream destination = ((IOutputStream) outputStream).AsStreamForWrite();
            streamForRead.CopyTo(destination);
            await destination.FlushAsync();
            int num = await ((IOutputStream) outputStream).FlushAsync() ? 1 : 0;
            await transactedWrite.CommitAsync();
            outputStream = (IRandomAccessStream) null;
          }
        }
        this.ReportFileJustCached(cacheFileInfo);
        lock (ContentLoader.SystemImageCacheHelper.Cache)
          ContentLoader.SystemImageCacheHelper.Cache.Remove(cacheFileInfo.LocalUri);
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
      }
      return cacheFileInfo.ServerPath;
    }

    private async Task<string> UpdateCachedFileAsync(
      IRandomAccessStream streamOfFileToCache,
      ContentCache.CacheFileInfo cacheFileInfo)
    {
      try
      {
        using (StorageStreamTransaction transactedWrite = await (await this._cacheFolder.CreateFileAsync(cacheFileInfo.LocalFileName, (CreationCollisionOption) 1)).OpenTransactedWriteAsync())
        {
          using (Stream streamForRead = ((IInputStream) streamOfFileToCache).AsStreamForRead())
          {
            IRandomAccessStream outputStream = transactedWrite.Stream;
            Stream destination = ((IOutputStream) outputStream).AsStreamForWrite();
            streamForRead.CopyTo(destination);
            await destination.FlushAsync();
            int num = await ((IOutputStream) outputStream).FlushAsync() ? 1 : 0;
            await transactedWrite.CommitAsync();
            outputStream = (IRandomAccessStream) null;
          }
        }
        this.ReportFileJustCached(cacheFileInfo);
        lock (ContentLoader.SystemImageCacheHelper.Cache)
          ContentLoader.SystemImageCacheHelper.Cache.Remove(cacheFileInfo.LocalUri);
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
      }
      return cacheFileInfo.ServerPath;
    }

    public async Task<string> AddFileToCacheAsync(Stream streamOfFileToCache, string filePath)
    {
      ContentCache.CacheFileInfo cacheFileInfo = new ContentCache.CacheFileInfo(filePath, this);
      return this.IsFileCached(cacheFileInfo) ? cacheFileInfo.ServerPath : await this.UpdateCachedFileAsync(streamOfFileToCache, cacheFileInfo);
    }

    public async Task<string> AddFileToCacheAsync(
      IRandomAccessStream streamOfFileToCache,
      string filePath)
    {
      ContentCache.CacheFileInfo cacheFileInfo = new ContentCache.CacheFileInfo(filePath, this);
      return this.IsFileCached(cacheFileInfo) ? cacheFileInfo.ServerPath : await this.UpdateCachedFileAsync(streamOfFileToCache, cacheFileInfo);
    }

    public async Task<Uri> AddFileToCacheAndGetLocalUriAsync(
      IRandomAccessStream streamOfFileToCache,
      string filePath)
    {
      ContentCache.CacheFileInfo cacheFileInfo = new ContentCache.CacheFileInfo(filePath, this);
      if (this.IsFileCached(cacheFileInfo))
        return cacheFileInfo.LocalUri;
      string str = await this.UpdateCachedFileAsync(streamOfFileToCache, cacheFileInfo);
      return cacheFileInfo.LocalUri;
    }

    public async Task<string> UpdateCachedFileAsync(Stream streamOfFileToCache, string filePath)
    {
      return await this.UpdateCachedFileAsync(streamOfFileToCache, new ContentCache.CacheFileInfo(filePath, this));
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
      ContentCache contentCache = this;
      StorageFolder cacheFolder = contentCache._cacheFolder;
      StorageFolder folderAsync = await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 3);
      contentCache._cacheFolder = folderAsync;
      contentCache = (ContentCache) null;
      foreach (StorageFile storageFile in (IEnumerable<StorageFile>) await this._cacheFolder.GetFilesAsync())
        this.CachedFiles.Add(storageFile.Name);
      if (!DataLoadViewModel.GetIsConnectedToNetwork())
        this.Pause();

      // Subscribe to network status change directly
      try
      {
        NetworkInformation.NetworkStatusChanged -= this.NetworkInformationOnNetworkStatusChanged;
      }
      catch { }
      NetworkInformation.NetworkStatusChanged += this.NetworkInformationOnNetworkStatusChanged;

      for (int index = 0; index < this.ThreadCount; ++index)
      {
        // Start worker threads that process cache queue
        ThreadPool.RunAsync(async (workItem) => await this.GetLocalPathQueueWorker());
      }
      this._elementsQueue = new KeyValuePairsQueue<object, string>();
      this._elementsQueue.ProcessItem += new ProcessItemHandler<object, string>(this.ElementsQueue_ProcessItem);
      this._elementsQueue.StartWorker();
    }

    private void NetworkInformationOnNetworkStatusChanged(object sender)
    {
      if (DataLoadViewModel.GetIsConnectedToNetwork())
      {
        this.Resume();
        this.ContinueWorkingAfterConnectionTurnedOn();
      }
      else
        this.Pause();
    }

    private async Task ElementsQueue_ProcessItem(
      object sender,
      ProcessItemEventArgs<object, string> args)
    {
      try
      {
        object element = args.Key;
        string serverPath = args.Value;
        if (string.IsNullOrEmpty(serverPath))
          return;
        ContentCache.CacheFileInfo newServerPathCacheInfo = new ContentCache.CacheFileInfo(serverPath, this);
        if (element is DependencyObject)
        {
          DependencyObject dependencyObject = (DependencyObject) element;
          lock (this._alwaysUpdateElementsLocker)
          {
            if (this._alwaysUpdateElements.Contains(dependencyObject))
            {
              newServerPathCacheInfo.IsAlwaysUpdate = true;
              this._alwaysUpdateElements.Remove(dependencyObject);
            }
          }
        }
        if (this.IsFileCached(newServerPathCacheInfo))
        {
          int num = await this.SetCachedSource(element, newServerPathCacheInfo) ? 1 : 0;
          if (!newServerPathCacheInfo.IsAlwaysUpdate)
            return;
        }
        lock (this.ObjectsLocker)
        {
          if (!this.Objects.ContainsKey(element))
            this.Objects.Add(element, newServerPathCacheInfo);
        }
        bool flag = false;
        lock (this.UriesAndElementsLocker)
        {
          if (!this.UriesAndObjects.ContainsKey(newServerPathCacheInfo))
          {
            flag = true;
            this.UriesAndObjects.Add(newServerPathCacheInfo, new List<object>()
            {
              element
            });
          }
          else if (!this.UriesAndObjects[newServerPathCacheInfo].Contains(element))
          {
            flag = this.UriesAndObjects[newServerPathCacheInfo].Count == 0;
            this.UriesAndObjects[newServerPathCacheInfo].Add(element);
          }
        }
        lock (this.ServerFilesPathsQueueLocker)
        {
          if (flag)
            this.ServerFilesPathsQueue.Insert(0, newServerPathCacheInfo);
          Monitor.PulseAll(this.ServerFilesPathsQueueLocker);
        }
        element = (object) null;
        newServerPathCacheInfo = (ContentCache.CacheFileInfo) null;
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    protected void ContinueWorkingAfterConnectionTurnedOn()
    {
      lock (this.ServerFilesPathsQueueLocker)
      {
        lock (this.ProblemFilesPathsLocker)
        {
          this.ServerFilesPathsQueue.AddRange((IEnumerable<ContentCache.CacheFileInfo>) this.ProblemFilesPaths);
          this.ProblemFilesPaths.Clear();
        }
      }
      lock (this.ServerFilesPathsQueueLocker)
        Monitor.PulseAll(this.ServerFilesPathsQueueLocker);
    }

    private async Task GetLocalPathQueueWorker()
    {
label_1:
      while (true)
      {
        if (this.IsPaused)
        {
          lock (this._resumePulsar)
            Monitor.Wait(this._resumePulsar);
        }
        try
        {
          ContentCache.CacheFileInfo nextItem = (ContentCache.CacheFileInfo) null;
          bool flag = false;
          lock (this.ServerFilesPathsQueueLocker)
          {
            if (this.ServerFilesPathsQueue.Count > 0)
            {
              int index = ContentCache.Rand.Next(this.ServerFilesPathsQueue.Count);
              nextItem = this.ServerFilesPathsQueue[index];
              this.ServerFilesPathsQueue.RemoveAt(index);
              flag = true;
            }
          }
          if (flag)
          {
            Uri uri = (Uri) null;
            if (!nextItem.IsAlwaysUpdate && this.IsFileCached(nextItem))
              uri = nextItem.LocalUri;
            List<object> elements = (List<object>) null;
            lock (this.UriesAndElementsLocker)
            {
              if (this.UriesAndObjects.ContainsKey(nextItem))
                elements = new List<object>((IEnumerable<object>) this.UriesAndObjects[nextItem]);
            }
            if (uri == (Uri) null)
            {
              Action<double> setProgress = (Action<double>) null;
              Action<string> setLoading = (Action<string>) null;
              Action<Exception> setException = (Action<Exception>) null;
              if (elements != null)
              {
                List<ContentLoader.ElementCacheHelper> helpersForInformProgress = elements.Select<object, ContentLoader.ElementCacheHelper>(new Func<object, ContentLoader.ElementCacheHelper>(ContentLoader.GetHelper)).Where<ContentLoader.ElementCacheHelper>((Func<ContentLoader.ElementCacheHelper, bool>) (x => x.NeedToInformProgressAndThrowException)).ToList<ContentLoader.ElementCacheHelper>();
                if (helpersForInformProgress.Count > 0)
                {
                  setProgress = (Action<double>) (progress =>
                  {
                    foreach (ContentLoader.ElementCacheHelper elementCacheHelper in helpersForInformProgress)
                      elementCacheHelper.SetProgressValue(progress);
                  });
                  setLoading = (Action<string>) (text =>
                  {
                    foreach (ContentLoader.ElementCacheHelper elementCacheHelper in helpersForInformProgress)
                      elementCacheHelper.SetLoadingText(text);
                  });
                  setException = (Action<Exception>) (exception =>
                  {
                    List<object> objectList = (List<object>) null;
                    lock (this.UriesAndElementsLocker)
                    {
                      if (this.UriesAndObjects.ContainsKey(nextItem))
                        objectList = this.UriesAndObjects[nextItem];
                    }
                    foreach (ContentLoader.ElementCacheHelper elementCacheHelper in helpersForInformProgress)
                    {
                      objectList?.Remove(elementCacheHelper.Target);
                      lock (this.ObjectsLocker)
                        this.Objects.Remove(elementCacheHelper.Target);
                      elementCacheHelper.SetException(exception);
                    }
                  });
                }
              }
              int downloadAttempt = 0;
              Uri result;
              while (true)
              {
                try
                {
                  DownloadFileClient downloadFileClient = new DownloadFileClient();
                  string sourceUrl = nextItem.ServerPath;
                  IServerSubstitution serverSubstitution = this.ServerSubstitution;
                  if (serverSubstitution != null)
                    sourceUrl = serverSubstitution.TransformUrl(sourceUrl);
                  int num1 = nextItem.IsAlwaysUpdate ? 1 : 0;
                  StorageFolder cacheFolder = this._cacheFolder;
                  string serverPath = sourceUrl;
                  string localFileName = nextItem.LocalFileName;
                  Action<double> changeProgress = setProgress;
                  Action<string> changeLoading = setLoading;
                  int num2 = await downloadFileClient.DownloadFileToCache(num1 != 0, cacheFolder, serverPath, localFileName, changeProgress, changeLoading) ? 1 : 0;
                  result = nextItem.LocalUri;
                  this.Log.Debug("Файл {0} успешно скачан", (object) nextItem.ServerPath);
                  break;
                }
                catch (UnauthorizedAccessException ex)
                {
                  lock (this.ServerFilesPathsQueueLocker)
                  {
                    this.ServerFilesPathsQueue.Insert(0, nextItem);
                    goto label_1;
                  }
                }
                catch (WebException ex)
                {
                  if (setProgress != null)
                    setProgress(0.001);
                  if (setLoading != null)
                    setLoading(CommonLocalizedResources.GetLocalizedString("txt_downloading"));
                  ++downloadAttempt;
                  if (downloadAttempt >= 5)
                  {
                    lock (this.ProblemFilesPathsLocker)
                      this.ProblemFilesPaths.Add(nextItem);
                    if (setException != null)
                    {
                      setException((Exception) ex);
                      goto label_1;
                    }
                    else
                      goto label_1;
                  }
                }
                catch (HttpRequestException ex)
                {
                  if (setProgress != null)
                    setProgress(0.001);
                  if (setLoading != null)
                    setLoading(CommonLocalizedResources.GetLocalizedString("txt_downloading"));
                  ++downloadAttempt;
                  if (downloadAttempt >= 5)
                  {
                    lock (this.ProblemFilesPathsLocker)
                      this.ProblemFilesPaths.Add(nextItem);
                    if (setException != null)
                    {
                      setException((Exception) ex);
                      goto label_1;
                    }
                    else
                      goto label_1;
                  }
                }
                catch (IOException ex)
                {
                  if (setProgress != null)
                    setProgress(0.001);
                  if (setLoading != null)
                    setLoading(CommonLocalizedResources.GetLocalizedString("txt_downloading"));
                  ++downloadAttempt;
                  if (downloadAttempt >= 5)
                  {
                    lock (this.ProblemFilesPathsLocker)
                      this.ProblemFilesPaths.Add(nextItem);
                    if (setException != null)
                    {
                      setException((Exception) ex);
                      goto label_1;
                    }
                    else
                      goto label_1;
                  }
                }
                await Task.Delay(1000);
              }
              lock (this.CachedFilesLocker)
              {
                this.ReportFileJustCached(nextItem);
                uri = result;
              }
              setProgress = (Action<double>) null;
              setLoading = (Action<string>) null;
              setException = (Action<Exception>) null;
              result = (Uri) null;
            }
            if (uri != (Uri) null)
            {
              lock (this.UriesAndElementsLocker)
              {
                if (this.UriesAndObjects.ContainsKey(nextItem))
                {
                  elements = this.UriesAndObjects[nextItem];
                  this.UriesAndObjects.Remove(nextItem);
                }
              }
              if (elements != null)
              {
                foreach (object element in elements)
                {
                  int num = await this.SetCachedSource(element, nextItem) ? 1 : 0;
                  lock (this.ObjectsLocker)
                    this.Objects.Remove(element);
                }
              }
            }
            elements = (List<object>) null;
          }
          else
          {
            lock (this.ServerFilesPathsQueueLocker)
              Monitor.Wait(this.ServerFilesPathsQueueLocker, 3000);
          }
        }
        catch (Exception ex)
        {
          if (Debugger.IsAttached)
            Debugger.Break();
        }
      }
    }

    public async Task<Uri> GetFileByServerPathAsync(
      string serverPath,
      bool getOnlyFromCache = false,
      Action<double> progressValueCallback = null,
      Action<string> progressTextCallback = null)
    {
      if (serverPath == null)
        throw new ArgumentNullException(nameof (serverPath));
      ContentCache.CacheFileInfo cacheFileInfo = ContentCache.IsServerPathCorrect(serverPath) ? new ContentCache.CacheFileInfo(serverPath, this) : throw new ArgumentException("serverPath is not correct", nameof (serverPath));
      if (this.IsFileCached(cacheFileInfo))
        return cacheFileInfo.LocalUri;
      if (getOnlyFromCache)
        return (Uri) null;
      TaskCompletionSource<Uri> completionSource;
      if (progressValueCallback != null)
        completionSource = (TaskCompletionSource<Uri>) new ContentLoader.TaskCompletionSourceWithCallbacks<Uri>()
        {
          ProgressValueCallback = progressValueCallback,
          LoadingTextCallback = progressTextCallback
        };
      else
        completionSource = new TaskCompletionSource<Uri>();
      TaskCompletionSource<Uri> key = completionSource;
      this._elementsQueue.Enqueue((object) key, serverPath);
      return await key.Task;
    }

    public async Task<StorageFile> GetCachedFileOrNullAsync(string serverPath)
    {
      string localFileName = serverPath != null ? ContentLoader.GetCachedFileName(serverPath) : throw new ArgumentNullException(nameof (serverPath));
      lock (this.CachedFilesLocker)
      {
        if (!this.CachedFiles.Contains(localFileName))
          return (StorageFile) null;
      }
      return await (await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 3)).GetFileAsync(localFileName);
    }

    private string GetCachedFilePath(string fileName)
    {
      return string.Format("ms-appdata:///local/{0}/{1}", (object) this.BinaryCacheFolderName, (object) fileName);
    }

    public async Task RemoveOutdatedFilesAsync(DateTime cutOffDate)
    {
      try
      {
        foreach (StorageFile storageFile in (IEnumerable<StorageFile>) await (await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 3)).GetFilesAsync())
        {
          if (storageFile.DateCreated < (DateTimeOffset) cutOffDate)
            await storageFile.DeleteAsync();
        }
      }
      catch (Exception ex)
      {
        this.Log.Error("RemoveOutdatedFilesAsync failed", ex);
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    public async Task DeleteFromCacheAsync(string serverPath)
    {
      if (serverPath == null)
        throw new ArgumentNullException(nameof (serverPath));
      ContentCache.CacheFileInfo cacheFileInfo = ContentCache.IsServerPathCorrect(serverPath) ? new ContentCache.CacheFileInfo(serverPath, this) : throw new ArgumentException("serverPath is not correct", nameof (serverPath));
      if (!this.IsFileCached(cacheFileInfo))
        return;
      try
      {
        await (await this._cacheFolder.GetFileAsync(cacheFileInfo.LocalFileName)).DeleteAsync((StorageDeleteOption) 0);
        lock (this.CachedFilesLocker)
          this.CachedFiles.Remove(cacheFileInfo.LocalFileName);
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    public async Task<ulong> GetFileSizeAsync(Uri localUri)
    {
      ulong fileSize = 0;
      try
      {
        fileSize = (await (await this._cacheFolder.GetFileAsync(Path.GetFileName(localUri.OriginalString))).GetBasicPropertiesAsync()).Size;
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
      }
      return fileSize;
    }

    private class CacheFileInfo
    {
      private Uri _serverUri;
      private string _localFileName;
      private Uri _localUri;

      public ContentCache CurrentContentCache { get; private set; }

      public string ServerPath { get; private set; }

      public CacheFileInfo(string serverPath, ContentCache contentCache)
      {
        this.ServerPath = serverPath != null ? serverPath : throw new ArgumentNullException(nameof (serverPath));
        this.CurrentContentCache = contentCache;
      }

      public Uri ServerUri
      {
        get
        {
          Uri serverUri = this._serverUri;
          return (object) serverUri != null ? serverUri : (this._serverUri = new Uri(this.ServerPath));
        }
      }

      public string LocalFileName
      {
        get
        {
          return this._localFileName ?? (this._localFileName = ContentLoader.GetCachedFileName(this.ServerPath));
        }
      }

      public Uri LocalUri
      {
        get
        {
          Uri localUri = this._localUri;
          return (object) localUri != null ? localUri : (this._localUri = new Uri(this.CurrentContentCache.GetCachedFilePath(this.LocalFileName)));
        }
      }

      public bool IsAlwaysUpdate { get; set; }

      public override int GetHashCode() => this.ServerPath.GetHashCode();

      public override string ToString() => this.ServerPath;

      public override bool Equals(object obj)
      {
        return obj is ContentCache.CacheFileInfo cacheFileInfo && this.ServerPath == cacheFileInfo.ServerPath;
      }
    }

    public class FileJustCachedEventArgs : EventArgs
    {
      public string ServerPath { get; private set; }

      internal FileJustCachedEventArgs(string serverPath) => this.ServerPath = serverPath;
    }
  }
}
