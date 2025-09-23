// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.DownloadFileClient
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

#nullable disable
namespace MetroLab.Common
{
  internal class DownloadFileClient
  {
    private TaskCompletionSource<bool> _taskCompletionSource;

    public async Task<bool> DownloadFileToCache(
      bool isAlwaysUpdate,
      StorageFolder cacheFolder,
      string serverPath,
      string localFileName,
      Action<double> changeProgress = null,
      Action<string> changeLoading = null)
    {
      this._taskCompletionSource = new TaskCompletionSource<bool>();
      this.Execute(isAlwaysUpdate, cacheFolder, serverPath, localFileName, changeProgress, changeLoading);
      return await this._taskCompletionSource.Task;
    }

    private async void Execute(
      bool isAlwaysUpdate,
      StorageFolder cacheFolder,
      string requestUriString,
      string localFileName,
      Action<double> changeProgress = null,
      Action<string> changeLoading = null)
    {
      try
      {
        using (HttpClient httpClient = new HttpClient())
        {
          if (isAlwaysUpdate)
            ((ICollection<HttpNameValueHeaderValue>) httpClient.DefaultRequestHeaders.CacheControl).Add(new HttpNameValueHeaderValue("Cache-Control", "no-cache"));
          IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> async = httpClient.GetAsync(new Uri(requestUriString, UriKind.RelativeOrAbsolute), (HttpCompletionOption) 0);
          Task<HttpResponseMessage> task;
          if (changeProgress == null)
          {
            task = async.AsTask<HttpResponseMessage, HttpProgress>();
          }
          else
          {
            string localizedDownloading = CommonLocalizedResources.GetLocalizedString("txt_downloading");
            changeLoading(localizedDownloading);
            Action<HttpProgress> handler = (Action<HttpProgress>) (progress =>
            {
              ulong bytesReceived = progress.BytesReceived;
              if (bytesReceived == 0UL || !progress.TotalBytesToReceive.HasValue)
                return;
              ulong num = progress.TotalBytesToReceive.Value;
              double a = (double) bytesReceived / (double) num * 100.0;
              changeLoading(string.Format("{0} {1}%", (object) localizedDownloading, (object) Math.Round(a).ToString((IFormatProvider) CultureInfo.InvariantCulture).PadLeft(3, ' ')));
              changeProgress(a);
            });
            task = async.AsTask<HttpResponseMessage, HttpProgress>((IProgress<HttpProgress>) new Progress<HttpProgress>(handler));
          }
          using (HttpResponseMessage response = await task)
          {
            response.EnsureSuccessStatusCode();
            using (IInputStream responseStream = await response.Content.ReadAsInputStreamAsync())
            {
              using (StorageStreamTransaction transactedWrite = await (await cacheFolder.CreateFileAsync(localFileName, (CreationCollisionOption) 1)).OpenTransactedWriteAsync())
              {
                IRandomAccessStream outputStream = transactedWrite.Stream;
                Stream destination = ((IOutputStream) outputStream).AsStreamForWrite();
                responseStream.AsStreamForRead().CopyTo(destination);
                await destination.FlushAsync();
                int num = await ((IOutputStream) outputStream).FlushAsync() ? 1 : 0;
                await transactedWrite.CommitAsync();
                outputStream = (IRandomAccessStream) null;
              }
            }
          }
        }
        this._taskCompletionSource.TrySetResult(true);
      }
      catch (Exception ex)
      {
        this._taskCompletionSource.TrySetException(ex);
      }
    }
  }
}
