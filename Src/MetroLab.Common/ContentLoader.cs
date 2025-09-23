// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ContentLoader
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using MetroLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace MetroLab.Common
{
  public abstract class ContentLoader
  {
    public static readonly DependencyProperty RemoveOldContentWhenLoadingProperty = DependencyProperty.RegisterAttached("RemoveOldContentWhenLoading", typeof (bool), typeof (ContentLoader), new PropertyMetadata((object) true));
    private static readonly object SupportedElementsLocker = new object();
    private static readonly Dictionary<Type, Type> SupportedElements = new Dictionary<Type, Type>();
    private static readonly HashAlgorithmProvider HashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);

    protected ILogger Log { get; set; }

    public static bool GetRemoveOldContentWhenLoading(DependencyObject frameworkControl)
    {
      return (bool) frameworkControl.GetValue(ContentLoader.RemoveOldContentWhenLoadingProperty);
    }

    public static void SetRemoveOldContentWhenLoading(DependencyObject frameworkControl, bool value)
    {
      frameworkControl.SetValue(ContentLoader.RemoveOldContentWhenLoadingProperty, (object) value);
    }

    public static void AddSupportedElementType(Type objectType, Type cacheHelperType)
    {
      lock (ContentLoader.SupportedElementsLocker)
        ContentLoader.SupportedElements.Add(objectType, cacheHelperType);
    }

    static ContentLoader()
    {
      ContentLoader.AddSupportedElementType(typeof (Image), typeof (ContentLoader.SystemImageCacheHelper));
      ContentLoader.AddSupportedElementType(typeof (TaskCompletionSource<Uri>), typeof (ContentLoader.TaskCompletionSourceCacheHelper));
      ContentLoader.AddSupportedElementType(typeof (ContentLoader.TaskCompletionSourceWithCallbacks<Uri>), typeof (ContentLoader.TaskCompletionSourceWithCallbacksCacheHelper));
      ContentLoader.AddSupportedElementType(typeof (MediaElement), typeof (ContentLoader.MediaElementCacheHelper));
    }

    protected ContentLoader()
    {
      this.Log = LogManagerFactory.DefaultLogManager.GetLogger(this.GetType());
    }

    public static ContentLoader.ElementCacheHelper GetHelper(object targetObject)
    {
      Type type = targetObject.GetType();
      lock (ContentLoader.SupportedElementsLocker)
      {
        if (!ContentLoader.SupportedElements.ContainsKey(type) && Debugger.IsAttached)
          Debugger.Break();
        ContentLoader.ElementCacheHelper instance = (ContentLoader.ElementCacheHelper) Activator.CreateInstance(ContentLoader.SupportedElements[type]);
        instance.InitializeWithObject(targetObject);
        return instance;
      }
    }

    protected static string GetCachedFileName(string serverFilePath)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(serverFilePath);
          output.Seek(0L, SeekOrigin.Begin);
          IBuffer ibuffer = output.ToArray().AsBuffer();
          CryptographicHash hash = ContentLoader.HashProvider.CreateHash();
          hash.Append(ibuffer);
          string cachedFileName = Base32.Base32Encode(hash.GetValueAndReset().ToArray());
          Match match = new Regex("^(([^:/?#]+):)?(//([^/?#]*))?([^?#]*)(\\?([^#]*))?(#(.*))?").Match(serverFilePath);
          if (match.Success)
          {
            string str1 = match.Groups[5].Value;
            if (!string.IsNullOrEmpty(str1))
            {
              int startIndex = str1.LastIndexOf('.');
              if (startIndex > -1)
              {
                string str2 = str1.Substring(startIndex);
                if (str2 != null && !((IEnumerable<char>) Path.GetInvalidPathChars()).Intersect<char>((IEnumerable<char>) str2.ToCharArray()).Any<char>())
                  return cachedFileName + str2;
              }
            }
          }
          return cachedFileName;
        }
      }
    }

    public abstract class ElementCacheHelper
    {
      public abstract object Target { get; }

      public abstract void InitializeWithObject(object target);

      public abstract void RemoveContent();

      public abstract Task<bool> IsSourceEmptyAsync();

      public virtual void SetProgressValue(double value)
      {
      }

      public virtual void SetLoadingText(string value)
      {
      }

      public virtual void SetException(Exception value)
      {
      }

      public virtual bool NeedToInformProgressAndThrowException => false;

      internal bool DoNotUseCache { get; set; }

      public abstract Task SetSourceAsync(Uri localUri);
    }

    public abstract class ElementCacheHelper<T> : ContentLoader.ElementCacheHelper
    {
      public override sealed object Target => (object) this.TargetObject;

      public T TargetObject { get; private set; }

      public override void InitializeWithObject(object target) => this.TargetObject = (T) target;
    }

    protected class SystemImageCacheHelper : ContentLoader.ElementCacheHelper<Image>
    {
      internal static readonly Dictionary<Uri, WeakReference<BitmapImage>> Cache = new Dictionary<Uri, WeakReference<BitmapImage>>();

      public override void RemoveContent() => this.TargetObject.put_Source((ImageSource) null);

      public override async Task SetSourceAsync(Uri localUri)
      {
        Image image = this.TargetObject;
        BitmapImage source = (BitmapImage) null;
        if (!this.DoNotUseCache)
        {
          lock (ContentLoader.SystemImageCacheHelper.Cache)
          {
            WeakReference<BitmapImage> weakReference;
            if (ContentLoader.SystemImageCacheHelper.Cache.TryGetValue(localUri, out weakReference))
              weakReference.TryGetTarget(out source);
          }
        }
        if (source == null)
        {
          source = new BitmapImage();
          StorageFile storageFile;
          if (localUri.OriginalString.StartsWith("ms-appdata:"))
            storageFile = await StorageFile.GetFileFromApplicationUriAsync(localUri);
          else
            storageFile = await StorageFile.GetFileFromPathAsync(localUri.OriginalString);
          using (IRandomAccessStreamWithContentType streamWithContentType = await storageFile.OpenReadAsync())
            ((BitmapSource) source).SetSource((IRandomAccessStream) streamWithContentType);
          lock (ContentLoader.SystemImageCacheHelper.Cache)
          {
            if (!ContentLoader.SystemImageCacheHelper.Cache.ContainsKey(localUri))
              ContentLoader.SystemImageCacheHelper.Cache.Add(localUri, new WeakReference<BitmapImage>(source));
            else
              ContentLoader.SystemImageCacheHelper.Cache[localUri] = new WeakReference<BitmapImage>(source);
          }
        }
        image.put_Source((ImageSource) source);
      }

      public override async Task<bool> IsSourceEmptyAsync() => this.TargetObject.Source == null;
    }

    private class MediaElementCacheHelper : ContentLoader.ElementCacheHelper<MediaElement>
    {
      public override void RemoveContent() => this.TargetObject.put_Source((Uri) null);

      public override async Task SetSourceAsync(Uri localUri)
      {
        this.TargetObject.put_Source(localUri);
      }

      public override async Task<bool> IsSourceEmptyAsync()
      {
        return this.TargetObject.Source == (Uri) null;
      }
    }

    private class TaskCompletionSourceCacheHelper : 
      ContentLoader.ElementCacheHelper<TaskCompletionSource<Uri>>
    {
      public override void RemoveContent()
      {
      }

      public override async Task SetSourceAsync(Uri localUri)
      {
        this.TargetObject.TrySetResult(localUri);
      }

      public override async Task<bool> IsSourceEmptyAsync() => false;
    }

    protected class TaskCompletionSourceWithCallbacks<T> : TaskCompletionSource<T>
    {
      public Action<double> ProgressValueCallback { get; set; }

      public Action<string> LoadingTextCallback { get; set; }
    }

    private class TaskCompletionSourceWithCallbacksCacheHelper : 
      ContentLoader.ElementCacheHelper<ContentLoader.TaskCompletionSourceWithCallbacks<Uri>>
    {
      public override void RemoveContent()
      {
      }

      public override async Task SetSourceAsync(Uri localUri)
      {
        this.TargetObject.TrySetResult(localUri);
      }

      public override async Task<bool> IsSourceEmptyAsync() => false;

      public override void SetProgressValue(double value)
      {
        if (this.TargetObject.ProgressValueCallback == null)
          return;
        this.TargetObject.ProgressValueCallback(value);
      }

      public override void SetLoadingText(string value)
      {
        if (this.TargetObject.LoadingTextCallback == null)
          return;
        this.TargetObject.LoadingTextCallback(value);
      }

      public override void SetException(Exception value)
      {
        if (this.TargetObject.ProgressValueCallback == null)
          return;
        this.TargetObject.TrySetException(value);
      }

      public override bool NeedToInformProgressAndThrowException => true;
    }
  }
}
