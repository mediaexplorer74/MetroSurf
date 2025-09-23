// Decompiled with JetBrains decompiler
// Type: MetroLog.LogManager
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Internal;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace MetroLog
{
  public class LogManager : LogManagerBase, IWinRTLogManager, ILogManager
  {
    public LogManager(LoggingConfiguration configuration)
      : base(configuration)
    {
    }

    public async Task<IStorageFile> GetCompressedLogFile()
    {
      Stream stream = await this.GetCompressedLogs();
      if (stream == null)
        return (IStorageFile) null;
      StorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(string.Format("Log - {0}.zip", (object) DateTime.UtcNow.ToString("yyyy-MM-dd HHmmss", (IFormatProvider) CultureInfo.InvariantCulture)), (CreationCollisionOption) 1);
      using (Stream ras = ((IOutputStream) await file.OpenAsync((FileAccessMode) 1)).AsStreamForWrite())
        await stream.CopyToAsync(ras);
      stream.Dispose();
      return (IStorageFile) file;
    }

    public Task ShareLogFile(string title, string description)
    {
      try
      {
        var dtm = DataTransferManager.GetForCurrentView();
        TypedEventHandler<DataTransferManager, DataRequestedEventArgs> handler = null;
        handler = (sender, args) =>
        {
          try
          {
            args.Request.Data.Properties.Title = title ?? string.Empty;
            args.Request.Data.SetText(description ?? string.Empty);
          }
          finally
          {
            try { dtm.DataRequested -= handler; } catch { }
          }
        };
        dtm.DataRequested += handler;
        DataTransferManager.ShowShareUI();
      }
      catch
      {
        // Ignore failures
      }
      return Task.CompletedTask;
    }

    private void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
    {
      args.Request.Data.Properties.Title = "Foobar";
      args.Request.Data.SetText("Yay!");
    }
  }
}
