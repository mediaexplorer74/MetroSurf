// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ContentStorage

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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;

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
      FrameworkElement frameworkElement2 = frameworkElement1;
      try { frameworkElement2.Loaded -= this.ElementOnLoaded; } catch { }
      frameworkElement2.Loaded += this.ElementOnLoaded;
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
      if (cacheFileInfo == null || cacheFileInfo.LocalUri == null)
        throw new ArgumentNullException(nameof(cacheFileInfo));

      Uri localUri = cacheFileInfo.LocalUri;

      if (element is FrameworkElement fe)
      {
        var tcs = new TaskCompletionSource<bool>();
        var _ = fe.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
          try
          {
            if (fe is Image img)
            {
              img.Source = new BitmapImage(localUri);
              tcs.SetResult(true);
              return;
            }
            if (fe is MediaElement me)
            {
              me.Source = localUri;
              tcs.SetResult(true);
              return;
            }
            var prop = element.GetType().GetRuntimeProperty("Source");
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
        return tcs.Task;
      }

      return Task.FromResult(false);
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
