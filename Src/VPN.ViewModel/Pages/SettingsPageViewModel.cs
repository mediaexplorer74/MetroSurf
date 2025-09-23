// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.SettingsPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using VPN.Localization;
using VPN.ViewModel.Http;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Popups;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class SettingsPageViewModel : BaseTilesPageViewModel
  {
    private string _email;
    private string _url;
    private ICommand _loginCommand;
    private ICommand _changePasswordCommand;
    private ICommand _FAQCommand;
    private ICommand _userCabinetCommand;
    private ICommand _facebookAppCommand;
    private ICommand _twitterAppCommand;
    private ICommand _privacyPolicyCommand;

    public string SettingsHeaderText
    {
      get => LocalizedResources.GetLocalizedString("action_settings").ToUpper();
    }

    public string ChangePasswordCommandButtonText
    {
      get => LocalizedResources.GetLocalizedString("S_CHANGE_PASSWORD_BTN").ToLower();
    }

    public string LogoutButtonText => LocalizedResources.GetLocalizedString("S_LOGOUT_BTN");

    public string AboutHeaderText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneAboutHeader").ToLower();
    }

    public string ProfileHeaderText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneProfileHeader").ToLower();
    }

    public string MakeAppBetterTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneMakeAppBetterTitle");
    }

    public string InformationTitleText => LocalizedResources.GetLocalizedString("S_INFO");

    public string FAQTitleText => LocalizedResources.GetLocalizedString("S_FAQ");

    public string PrivacyPolicyText
    {
      get => LocalizedResources.GetLocalizedString("WinPhonePrivacyPolicyTitle");
    }

    public string SocialChanelsTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneSocialChanelsTitle");
    }

    public string FacebookLinkText => LocalizedResources.GetLocalizedString("S_FOLLOW_FACEBOOK");

    public string TwitterLinkText => LocalizedResources.GetLocalizedString("S_FOLLOW_TWITTER");

    public string UserCabinetButtonText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10UserCabinetButton");
    }

    [DataMember]
    public string UserEmail
    {
      get => this._email;
      set => this.SetProperty<string>(ref this._email, value, nameof (UserEmail));
    }

    public string Url
    {
      get => this._url;
      set => this.SetProperty<string>(ref this._url, value, nameof (Url));
    }

    public bool IsUserLoggedUsingKeepSolidAccount
    {
      get => AutoLoginAgent.Current.CredentialsKeepSolidID != null;
    }

    public ICommand LogoutCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loginCommand, (Action<object>) (async o =>
        {
          IUICommand iuiCommand = await new MessageDialog(LocalizedResources.GetLocalizedString("S_LOGOUT_CONFIRMATION"), 
              LocalizedResources.GetLocalizedString("S_INFORMATION_WARNING"))
          {
            Commands = {
              (IUICommand) new UICommand(LocalizedResources.GetLocalizedString("S_OK"), 
              new UICommandInvokedHandler((object) this, __methodptr(CommandHandlers))),
              (IUICommand) new UICommand(LocalizedResources.GetLocalizedString("S_CANCEL"), 
              new UICommandInvokedHandler((object) this, __methodptr(CommandHandlers)))
            }
          }.ShowAsync();
        }));
      }
    }

    public async void CommandHandlers(IUICommand commandLabel)
    {
      if (!(commandLabel.Label == LocalizedResources.GetLocalizedString("S_OK")))
        return;
      await AutoLoginAgent.Current.Logout();
      AppViewModel.Current.ClearNavigationStack();
      AppViewModel.Current.GoHome();
    }

    public ICommand ChangePasswordCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._changePasswordCommand, (Action<object>) (async o =>
        {
          if (AutoLoginAgent.Current.CredentialsKeepSolidID == null)
            return;
          await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new ChangePasswordPageViewModel()
          {
            Email = AutoLoginAgent.Current.CredentialsKeepSolidID.Email
          });
        }));
      }
    }

    public ICommand FAQCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._FAQCommand, (Action<object>) (async o =>
        {
          if (Constants.GetCurrentLanguage().Equals("ru"))
          {
            int num1 = await Launcher.LaunchUriAsync(new Uri("https://www.vpnunlimitedapp.com/ru/help")) ? 1 : 0;
          }
          else
          {
            int num2 = await Launcher.LaunchUriAsync(new Uri("https://www.vpnunlimitedapp.com/help")) ? 1 : 0;
          }
        }));
      }
    }

    public ICommand UserCabinetCommand
    {
      get
      {
        int num;
        return BaseViewModel.GetCommand(ref this._userCabinetCommand, 
            (Action<object>) (async o => num = await Launcher.LaunchUriAsync(new Uri(AutoLoginAgent.Current.CabinetUrl)) ? 1 : 0));
      }
    }

    public ICommand FacebookAppCommand
    {
      get
      {
        int num;
        return BaseViewModel.GetCommand(ref this._facebookAppCommand, 
            (Action<object>) (async o => num = await Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/vpnunlimitedapp")) ? 1 : 0));
      }
    }

    public ICommand TwitterAppCommand
    {
      get
      {
        int num;
        return BaseViewModel.GetCommand(ref this._twitterAppCommand, 
            (Action<object>) (async o => num = await Launcher.LaunchUriAsync(new Uri("https://twitter.com/vpnunlimited")) ? 1 : 0));
      }
    }

    public ICommand PrivacyPolicyCommand
    {
      get
      {
        int num;
        return BaseViewModel.GetCommand(ref this._privacyPolicyCommand, 
            (Action<object>) (async o => num = await Launcher.LaunchUriAsync(new Uri("https://www.keepsolid.com/privacy")) ? 1 : 0));
      }
    }

    protected override async Task<bool> LoadAsync()
    {
      this.UserEmail = AutoLoginAgent.Current.UserEmail;
      return true;
    }

    public string SupportString => LocalizedResources.GetLocalizedString("S_CONTACT_SUPPORT");

    public string RateUsString => LocalizedResources.GetLocalizedString("S_RATE_US");

    public string ApplicationOverviewString
    {
      get => LocalizedResources.GetLocalizedString("S_QUICK_TOUR_LABEL");
    }

    public string ShareString => LocalizedResources.GetLocalizedString("S_SHARE");

    public override async Task RequestData(DataTransferManager manager, DataRequestedEventArgs args)
    {
      DataPackage data = args.Request.Data;
      DataPackagePropertySet properties = data.Properties;
      properties.ApplicationName = LocalizedResources.GetLocalizedString("app_name");
      properties.Title = LocalizedResources.GetLocalizedString("WinPhoneShareTitle");
      data.SetText(LocalizedResources.GetLocalizedString("S_SHARE_FACEBOOK_TEXT"));
    }
  }
}
