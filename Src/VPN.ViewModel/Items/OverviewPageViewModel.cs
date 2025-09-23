// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Items.OverviewPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System.Runtime.Serialization;
using System.Threading.Tasks;

#nullable disable
namespace VPN.ViewModel.Items
{
  [DataContract]
  public class OverviewPageViewModel : BaseViewModel
  {
    private string _imagePath;
    private string _title;
    private string _text1;
    private string _text2;
    private string _text3;
    private string _text4;
    private bool _isLastPage;
    private const int TimeSpanBeforeNextButtonIsAppeared = 10000;
    private bool _isNextButtonIsVisible;
    private bool _isSelected;
    public int HowManyTimesUserFlippedToThisPage;

    [DataMember]
    public string ImagePath
    {
      get => this._imagePath;
      set => this.SetProperty<string>(ref this._imagePath, value, nameof (ImagePath));
    }

    [DataMember]
    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    [DataMember]
    public string Text1
    {
      get => this._text1;
      set => this.SetProperty<string>(ref this._text1, value, nameof (Text1));
    }

    [DataMember]
    public string Text2
    {
      get => this._text2;
      set => this.SetProperty<string>(ref this._text2, value, nameof (Text2));
    }

    [DataMember]
    public string Text3
    {
      get => this._text3;
      set => this.SetProperty<string>(ref this._text3, value, nameof (Text3));
    }

    [DataMember]
    public string Text4
    {
      get => this._text4;
      set => this.SetProperty<string>(ref this._text4, value, nameof (Text4));
    }

    [DataMember]
    public bool IsLastPage
    {
      get => this._isLastPage;
      set => this.SetProperty<bool>(ref this._isLastPage, value, nameof (IsLastPage));
    }

    public bool IsNextButtonIsVisible
    {
      get => this.IsLastPage || this._isNextButtonIsVisible;
      set
      {
        if (this.IsLastPage)
          return;
        this.SetProperty<bool>(ref this._isNextButtonIsVisible, value, nameof (IsNextButtonIsVisible));
      }
    }

    public bool IsSelected
    {
      get => this._isSelected;
      set => this.SetProperty<bool>(ref this._isSelected, value, nameof (IsSelected));
    }

    public async Task ShowNextButtonAsync()
    {
      ++this.HowManyTimesUserFlippedToThisPage;
      await Task.Delay(10000);
      --this.HowManyTimesUserFlippedToThisPage;
      if (this.HowManyTimesUserFlippedToThisPage != 0 || !this.IsSelected)
        return;
      this.IsNextButtonIsVisible = true;
    }
  }
}
