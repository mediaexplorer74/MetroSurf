// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ContentStorage
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLab.Common
{
  public class ContentStorage : ContentLoader
  {
    private static volatile ContentStorage _current = new ContentStorage();
    public static readonly DependencyProperty SourceUrlProperty = DependencyProperty.RegisterAttached("SourceUrl", typeof (string), typeof (ContentStorage), new PropertyMetadata((object) null, new PropertyChangedCallback(ContentStorage.Current.OnSourceUriPropertyChanged)));
    private readonly object UriesAndElementsLocker = new object();
    private readonly Dictionary<ContentStorage.CacheFileInfo, List<object>> UriesAndObjects = new Dictionary<ContentStorage.CacheFileInfo, List<object>>();
    private readonly object ObjectsLocker = new object();
    private readonly Dictionary<object, ContentStorage.CacheFileInfo> Objects = new Dictionary<object, ContentStorage.CacheFileInfo>();
    private readonly object PendingUnloadedElementsQueueLocker = new object();
    private List<object> _pendingUnloadedElementsQueue = new List<object>();
    private readonly object CachedFilesLocker = new object();
    private readonly HashSet<string> CachedFiles = new HashSet<string>();
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    private StorageFolder _cacheFolder;
    private KeyValuePairsQueue<object, string> _elementsQueue = new KeyValuePairsQueue<object, string>();
    private readonly object InitializeLocker = new object();
    private volatile Task _initializeTask;

    protected virtual string BinaryCacheFolderName => "BinaryStorage";

    public static ContentStorage Current => ContentStorage._current;

    public static string GetSourceUrl(DependencyObject frameworkControl)
    {
      return (string) frameworkControl.GetValue(ContentStorage.SourceUrlProperty);
    }

    public static void SetSourceUrl(DependencyObject frameworkControl, string value)
    {
      frameworkControl.SetValue(ContentStorage.SourceUrlProperty, (object) value);
    }

    protected async void OnSourceUriPropertyChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      string newServerPath = (string) e.NewValue;
      if (newServerPath != null && !ContentStorage.IsServerPathCorrect(newServerPath))
        throw new InvalidDataException("SourceUri is not correct");
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
      string sourceUrl = ContentStorage.GetSourceUrl((DependencyObject) element);
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
      ContentStorage.CacheFileInfo key;
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
      }
    }

    private static bool IsServerPathCorrect(string serverPath)
    {
      if (serverPath == null)
        throw new ArgumentNullException(nameof (serverPath));
      return serverPath.StartsWith("http://") || serverPath.StartsWith("https://") || serverPath.StartsWith("lazylens://");
    }

    private Task<bool> SetCachedSource(object element, ContentStorage.CacheFileInfo cacheFileInfo)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentStorage.\u003C\u003Ec__DisplayClass23_0 cDisplayClass230 = new ContentStorage.\u003C\u003Ec__DisplayClass23_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.element = element;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.cacheFileInfo = cacheFileInfo;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass230.cacheFileInfo.LocalUri == (Uri) null)
        throw new ArgumentNullException("localUri");
      // ISSUE: reference to a compiler-generated field
      cDisplayClass230.task = new TaskCompletionSource<bool>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClass230.action = new Action<object>(cDisplayClass230.\u003CSetCachedSource\u003Eb__0);
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass230.element is FrameworkElement element1)
      {
        // ISSUE: method pointer
        ((DependencyObject) element1).Dispatcher.RunAsync((CoreDispatcherPriority) -1, new DispatchedHandler((object) cDisplayClass230, __methodptr(\u003CSetCachedSource\u003Eb__1)));
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass230.action(cDisplayClass230.element);
      }
      // ISSUE: reference to a compiler-generated field
      return cDisplayClass230.task.Task;
    }

    public void ReportCachedFilesDeleted()
    {
      lock (this.CachedFilesLocker)
        this.CachedFiles.Clear();
      lock (ContentLoader.SystemImageCacheHelper.Cache)
        ContentLoader.SystemImageCacheHelper.Cache.Clear();
    }

    private bool IsFileCached(ContentStorage.CacheFileInfo cacheFileInfo)
    {
      lock (this.CachedFilesLocker)
        return this.CachedFiles.Contains(cacheFileInfo.LocalFileName);
    }

    public bool IsFileCached(string serverPath)
    {
      return serverPath != null ? ContentStorage.Current.IsFileCached(new ContentStorage.CacheFileInfo(serverPath, ContentStorage.Current)) : throw new ArgumentNullException(nameof (serverPath));
    }

    public event EventHandler<ContentStorage.FileJustCachedEventArgs> FileJustCached;

    protected virtual void OnFileJustCached(ContentStorage.FileJustCachedEventArgs e)
    {
      EventHandler<ContentStorage.FileJustCachedEventArgs> fileJustCached = this.FileJustCached;
      if (fileJustCached == null)
        return;
      fileJustCached((object) this, e);
    }

    private async Task ReportFileJustCachedAsync(ContentStorage.CacheFileInfo cacheFileInfo)
    {
      if (!this.CachedFiles.Contains(cacheFileInfo.LocalFileName))
      {
        this.CachedFiles.Add(cacheFileInfo.LocalFileName);
        this.OnFileJustCached(new ContentStorage.FileJustCachedEventArgs(cacheFileInfo.ServerPath));
      }
      await this.SetJustCachedFileAsSourceOfAllExpectantElementsAsync(cacheFileInfo);
    }

    private async Task SetJustCachedFileAsSourceOfAllExpectantElementsAsync(
      ContentStorage.CacheFileInfo cacheFileInfo)
    {
      List<object> objectList = (List<object>) null;
      if (!(cacheFileInfo.LocalUri != (Uri) null))
        return;
      lock (this.UriesAndElementsLocker)
      {
        if (this.UriesAndObjects.ContainsKey(cacheFileInfo))
        {
          objectList = this.UriesAndObjects[cacheFileInfo];
          this.UriesAndObjects.Remove(cacheFileInfo);
        }
      }
      if (objectList == null)
        return;
      foreach (object element in objectList)
      {
        int num = await this.SetCachedSource(element, cacheFileInfo) ? 1 : 0;
        lock (this.ObjectsLocker)
          this.Objects.Remove(element);
      }
    }

    private void ReportCacheForFileIsBroken(ContentStorage.CacheFileInfo cacheFileInfo)
    {
      lock (this.CachedFilesLocker)
        this.CachedFiles.Remove(cacheFileInfo.LocalFileName);
    }

    private async Task<string> UpdateCachedFileAsync(
      Stream streamOfFileToCache,
      ContentStorage.CacheFileInfo cacheFileInfo)
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
        await this.ReportFileJustCachedAsync(cacheFileInfo);
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
      ContentStorage.CacheFileInfo cacheFileInfo)
    {
      try
      {
        await this._semaphore.WaitAsync();
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
        await this.ReportFileJustCachedAsync(cacheFileInfo);
        lock (ContentLoader.SystemImageCacheHelper.Cache)
          ContentLoader.SystemImageCacheHelper.Cache.Remove(cacheFileInfo.LocalUri);
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
      }
      finally
      {
        this._semaphore.Release();
      }
      return cacheFileInfo.ServerPath;
    }

    public async Task<string> AddFileToCacheAsync(Stream streamOfFileToCache, string filePath)
    {
      ContentStorage.CacheFileInfo cacheFileInfo = new ContentStorage.CacheFileInfo(filePath, this);
      return this.IsFileCached(cacheFileInfo) ? cacheFileInfo.ServerPath : await this.UpdateCachedFileAsync(streamOfFileToCache, cacheFileInfo);
    }

    public async Task<string> AddFileToCacheAsync(
      IRandomAccessStream streamOfFileToCache,
      string filePath)
    {
      ContentStorage.CacheFileInfo cacheFileInfo = new ContentStorage.CacheFileInfo(filePath, this);
      return this.IsFileCached(cacheFileInfo) ? cacheFileInfo.ServerPath : await this.UpdateCachedFileAsync(streamOfFileToCache, cacheFileInfo);
    }

    public async Task<string> UpdateCachedFileAsync(Stream streamOfFileToCache, string filePath)
    {
      return await this.UpdateCachedFileAsync(streamOfFileToCache, new ContentStorage.CacheFileInfo(filePath, this));
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
      ContentStorage contentStorage = this;
      StorageFolder cacheFolder = contentStorage._cacheFolder;
      StorageFolder folderAsync = await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 3);
      contentStorage._cacheFolder = folderAsync;
      contentStorage = (ContentStorage) null;
      foreach (StorageFile storageFile in (IEnumerable<StorageFile>) await this._cacheFolder.GetFilesAsync())
        this.CachedFiles.Add(storageFile.Name);
      this._elementsQueue = new KeyValuePairsQueue<object, string>();
      this._elementsQueue.ProcessItem += new ProcessItemHandler<object, string>(this.ElementsQueue_ProcessItem);
      this._elementsQueue.StartWorker();
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
        ContentStorage.CacheFileInfo newServerPathCacheInfo = new ContentStorage.CacheFileInfo(serverPath, this);
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
        lock (this.UriesAndElementsLocker)
        {
          if (!this.UriesAndObjects.ContainsKey(newServerPathCacheInfo))
            this.UriesAndObjects.Add(newServerPathCacheInfo, new List<object>()
            {
              element
            });
          else if (!this.UriesAndObjects[newServerPathCacheInfo].Contains(element))
            this.UriesAndObjects[newServerPathCacheInfo].Add(element);
        }
        element = (object) null;
        newServerPathCacheInfo = (ContentStorage.CacheFileInfo) null;
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
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
      ContentStorage.CacheFileInfo cacheFileInfo = ContentStorage.IsServerPathCorrect(serverPath) ? new ContentStorage.CacheFileInfo(serverPath, this) : throw new ArgumentException("serverPath is not correct", nameof (serverPath));
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
      ContentStorage.CacheFileInfo cacheFileInfo = ContentStorage.IsServerPathCorrect(serverPath) ? new ContentStorage.CacheFileInfo(serverPath, this) : throw new ArgumentException("serverPath is not correct", nameof (serverPath));
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

    public async Task ClearCachedFiles()
    {
      try
      {
        StorageFolder folderAsync = await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 1);
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    private class CacheFileInfo
    {
      private Uri _serverUri;
      private string _localFileName;
      private Uri _localUri;

      public ContentStorage CurrentContentStorage { get; private set; }

      public string ServerPath { get; private set; }

      public CacheFileInfo(string serverPath, ContentStorage contentStorage)
      {
        this.ServerPath = serverPath != null ? serverPath : throw new ArgumentNullException(nameof (serverPath));
        this.CurrentContentStorage = contentStorage;
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
          return (object) localUri != null ? localUri : (this._localUri = new Uri(this.CurrentContentStorage.GetCachedFilePath(this.LocalFileName)));
        }
      }

      public bool IsAlwaysUpdate { get; set; }

      public override int GetHashCode() => this.ServerPath.GetHashCode();

      public override string ToString() => this.ServerPath;

      public override bool Equals(object obj)
      {
        return obj is ContentStorage.CacheFileInfo cacheFileInfo && this.ServerPath == cacheFileInfo.ServerPath;
      }
    }

    public class FileJustCachedEventArgs : EventArgs
    {
      public string ServerPath { get; private set; }

      internal FileJustCachedEventArgs(string serverPath) => this.ServerPath = serverPath;
    }
  }
}
