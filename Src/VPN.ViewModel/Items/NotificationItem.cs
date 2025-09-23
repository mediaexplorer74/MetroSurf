// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Items.NotificationItem
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System.Runtime.Serialization;

#nullable disable
namespace VPN.ViewModel.Items
{
  [DataContract]
  public class NotificationItem : BaseViewModel
  {
    private string _name;
    private string _iconName;
    private string _url;

    [DataMember]
    public string Name
    {
      get => this._name;
      set => this.SetProperty<string>(ref this._name, value, nameof (Name));
    }

    [DataMember]
    public string IconName
    {
      get => this._iconName;
      set => this.SetProperty<string>(ref this._iconName, value, nameof (IconName));
    }

    [DataMember]
    public string Url
    {
      get => this._url;
      set => this.SetProperty<string>(ref this._url, value, nameof (Url));
    }

    [DataMember]
    public NotificationItem.NotificationType Type { get; set; }

    public enum NotificationType
    {
      Browser,
      Share,
      Rate,
    }
  }
}
