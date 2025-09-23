// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.PageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace VPN.ViewModel
{
  [DataContract]
  public class PageViewModel : VPNDataLoadViewModel, IPageViewModel
  {
    public virtual void OnNavigating()
    {
    }

    [IgnoreDataMember]
    public IPrintHelper PrintHelper { get; set; }

    public virtual async Task RequestData(DataTransferManager manager, DataRequestedEventArgs args)
    {
    }

    protected async Task<IRandomAccessStream> GetStreamByLocalUri(Uri localUri)
    {
      InMemoryRandomAccessStream imageStream = new InMemoryRandomAccessStream();
      long num1 = (long) await RandomAccessStream.CopyAsync(await (await StorageFile.GetFileFromApplicationUriAsync(localUri)).OpenSequentialReadAsync(), (IOutputStream) imageStream);
      int num2 = await imageStream.FlushAsync() ? 1 : 0;
      return (IRandomAccessStream) imageStream;
    }

    public virtual async Task OnSerializingToStorageAsync()
    {
    }

    public virtual async Task OnSerializedToStorageAsync()
    {
    }

    public virtual async Task OnNavigatingAsync()
    {
    }
  }
}
