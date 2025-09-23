// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.SimplePageViewModel
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

#nullable disable
namespace MetroLab.Common
{
  [DataContract]
  public class SimplePageViewModel : BaseViewModel, IPageViewModel
  {
    protected internal virtual async Task<bool> LoadAsync()
    {
      bool flag;
      return flag;
    }

    public virtual async Task InitializeAsync()
    {
    }

    public virtual async Task UpdateAsync()
    {
    }

    [IgnoreDataMember]
    public IPrintHelper PrintHelper { get; set; }

    public virtual async Task RequestData(DataTransferManager manager, DataRequestedEventArgs args)
    {
    }

    public virtual async Task OnSerializingToStorageAsync()
    {
    }

    public virtual async Task OnSerializedToStorageAsync()
    {
    }

    public virtual async Task OnDeserializedFromStorageAsync()
    {
    }

    public virtual async Task OnNavigatingAsync()
    {
    }

    public virtual void UnSubscribeFromEvents()
    {
    }
  }
}
