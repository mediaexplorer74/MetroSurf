// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Items.GuidePageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;

#nullable disable
namespace VPN.ViewModel.Items
{
  [DataContract]
  public class GuidePageViewModel : BaseViewModel
  {
    private string _screenPath;
    private string _devicePath = Constants.IsWin10() ? "/Assets/VPNGuide10/device.png" : "/Assets/VPNGuide/device.png";
    private string _stepNumber;
    private string _stepDescription;
    private string _dataToCopy;
    private bool _isLastPage;
    private bool _isFirstPage;
    private const int TimeSpanBeforeNextButtonIsAppeared = 10000;
    private bool _isNextButtonIsVisible;
    private bool _isSelected;
    private bool _isUserNamePage;
    public int HowManyTimesUserFlippedToThisPage;
    private ICommand _goBackToMainPageCommand;
    private ICommand _goBackToSettingsPageCommand;

    [DataMember]
    public string ScreenPath
    {
      get => this._screenPath;
      set => this.SetProperty<string>(ref this._screenPath, value, nameof (ScreenPath));
    }

    [DataMember]
    public string DevicePath
    {
      get => this._devicePath;
      set => this.SetProperty<string>(ref this._devicePath, value, nameof (DevicePath));
    }

    [DataMember]
    public string StepNumber
    {
      get => this._stepNumber;
      set => this.SetProperty<string>(ref this._stepNumber, value, nameof (StepNumber));
    }

    [DataMember]
    public string StepDescription
    {
      get => this._stepDescription;
      set => this.SetProperty<string>(ref this._stepDescription, value, nameof (StepDescription));
    }

    [DataMember]
    public string DataToCopy
    {
      get => this._dataToCopy;
      set => this.SetProperty<string>(ref this._dataToCopy, value, nameof (DataToCopy));
    }

    [DataMember]
    public bool IsLastPage
    {
      get => this._isLastPage;
      set => this.SetProperty<bool>(ref this._isLastPage, value, nameof (IsLastPage));
    }

    [DataMember]
    public bool IsFirstPage
    {
      get => this._isFirstPage;
      set => this.SetProperty<bool>(ref this._isFirstPage, value, nameof (IsFirstPage));
    }

    public bool IsNextButtonIsVisible
    {
      get => !this.IsLastPage && this._isNextButtonIsVisible;
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

    [DataMember]
    public bool IsUserNamePage
    {
      get => this._isUserNamePage;
      set => this.SetProperty<bool>(ref this._isUserNamePage, value, nameof (IsUserNamePage));
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

    public ICommand GoBackToMainPageCommand
    {
      get
      {
        return !this.IsLastPage ? (ICommand) null : BaseViewModel.GetCommand(ref this._goBackToMainPageCommand, (Action<object>) (async o => await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel())));
      }
    }

    public ICommand GoBackToSettingsPageCommand
    {
      get
      {
        int num;
        return !this.IsFirstPage ? (ICommand) null : BaseViewModel.GetCommand(ref this._goBackToSettingsPageCommand, (Action<object>) (async o => num = await Launcher.LaunchUriAsync(new Uri("ms-settings:network-vpn")) ? 1 : 0));
      }
    }
  }
}
