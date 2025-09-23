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
  internal class LogManager(LoggingConfiguration configuration) : 
    LogManagerBase(configuration),
    IWinRTLogManager,
    ILogManager
  {
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LogManager.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new LogManager.\u003C\u003Ec__DisplayClass2_0()
      {
        \u003C\u003E4__this = this,
        title = title,
        description = description,
        dtm = DataTransferManager.GetForCurrentView(),
        tcs = new TaskCompletionSource<object>(),
        handler = (TypedEventHandler<DataTransferManager, DataRequestedEventArgs>) null
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass20.handler = new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object) cDisplayClass20, __methodptr(\u003CShareLogFile\u003Eb__0));
      // ISSUE: reference to a compiler-generated field
      DataTransferManager dtm = cDisplayClass20.dtm;
      // ISSUE: reference to a compiler-generated field
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Func<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, EventRegistrationToken>(dtm.add_DataRequested), new Action<EventRegistrationToken>(dtm.remove_DataRequested), cDisplayClass20.handler);
      DataTransferManager.ShowShareUI();
      return (Task) Task.FromResult<bool>(true);
    }

    private void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
    {
      args.Request.Data.Properties.put_Title("Foobar");
      args.Request.Data.SetText("Yay!");
    }
  }
}
