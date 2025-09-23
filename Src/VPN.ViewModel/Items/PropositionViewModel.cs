// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Items.PropositionViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System.Runtime.Serialization;

#nullable disable
namespace VPN.ViewModel.Items
{
  [DataContract]
  public class PropositionViewModel : BaseViewModel
  {
    public string ID;
    private string _price;
    private string _period;
    private string _name;
    private int _discount;
    private int _daysCount;

    [DataMember]
    public string Price
    {
      get => this._price;
      set
      {
        if (!this.SetProperty<string>(ref this._price, value, nameof (Price)))
          return;
        this.OnPropertyChanged(nameof (Price));
      }
    }

    [DataMember]
    public string Period
    {
      get => this._period;
      set => this.SetProperty<string>(ref this._period, value, nameof (Period));
    }

    [DataMember]
    public string Name
    {
      get => this._name;
      set => this.SetProperty<string>(ref this._name, value, nameof (Name));
    }

    public string NamePeriod => this._name.ToUpper();

    [DataMember]
    public int Discount
    {
      get => this._discount;
      set => this.SetProperty<int>(ref this._discount, value, nameof (Discount));
    }

    [DataMember]
    public int DaysCount
    {
      get => this._daysCount;
      set => this.SetProperty<int>(ref this._daysCount, value, nameof (DaysCount));
    }

    public string DiscountString
    {
      get => this._discount != 0 ? this._discount.ToString() + "%" : (string) null;
    }
  }
}
