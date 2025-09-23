// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Items.ServerViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.ViewModel.Items
{
  [DataContract]
  public class ServerViewModel : BaseViewModel
  {
    private string _country;
    private string _flagUrl;
    private string _flagIcon;
    private string _city;
    private string _region;

    [DataMember]
    public string Country
    {
      get => this._country;
      set => this.SetProperty<string>(ref this._country, value, nameof (Country));
    }

    [DataMember]
    public string FlagUrl
    {
      get => this._flagUrl;
      set => this.SetProperty<string>(ref this._flagUrl, value, nameof (FlagUrl));
    }

    [DataMember]
    public string FlagIcon
    {
      get => this._flagIcon;
      set
      {
        this.SetProperty<string>(ref this._flagIcon, value, nameof (FlagIcon));
        this.OnPropertyChanged("BorderBrush");
      }
    }

    [DataMember]
    public string City
    {
      get => this._city;
      set => this.SetProperty<string>(ref this._city, value, nameof (City));
    }

    public string CountryPlusCity => this._country + ", " + this._city;

    public SolidColorBrush BorderBrush
    {
      get
      {
        return !this.FlagIcon.Contains("optimal") ? new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 221, (byte) 221, (byte) 221)) : new SolidColorBrush(Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0));
      }
    }

    [DataMember]
    public string Region
    {
      get => this._region;
      set => this.SetProperty<string>(ref this._region, value, nameof (Region));
    }
  }
}
