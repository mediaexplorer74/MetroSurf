// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.LoginKeepSolidIDPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using VPN.Localization;
using VPN.Model.SocialNetworks;
using VPN.ViewModel.Http;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class LoginKeepSolidIDPageViewModel : PageViewModel
  {
    private string _email;
    private string _password;
    private bool _areButtonsDisabled;
    private ICommand _loginCommand;
    private ICommand _remindPasswordCommand;

    public string EmailTextBoxPlaceHolder
    {
      get => LocalizedResources.GetLocalizedString("S_EMAIL_TEMPLATE");
    }

    public string LoginKeepSolidIdPageHeader
    {
      get => LocalizedResources.GetLocalizedString("S_LOGIN_TITLE").ToUpper();
    }

    public string PasswordTextBoxHeader
    {
      get => LocalizedResources.GetLocalizedString("S_PASSWORD_PLACE");
    }

    public string EmailTextBoxHeader => LocalizedResources.GetLocalizedString("S_EMAIL_PLACE");

    public string ForgotPasswordText => LocalizedResources.GetLocalizedString("S_FORGOT_PASS");

    public string LoginButtonText => LocalizedResources.GetLocalizedString("S_LOGIN").ToLower();

    [DataMember]
    public string Email
    {
      get => this._email;
      set => this.SetProperty<string>(ref this._email, value, nameof (Email));
    }

    [DataMember]
    public string Password
    {
      get => this._password;
      set => this.SetProperty<string>(ref this._password, value, nameof (Password));
    }

    public bool AreButtonsDisabled
    {
      get => this._areButtonsDisabled;
      set
      {
        this.SetProperty<bool>(ref this._areButtonsDisabled, value, nameof (AreButtonsDisabled));
      }
    }

    public ICommand LoginCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loginCommand, (Action<object>) (async o =>
        {
          if (this.AreButtonsDisabled)
            return;
          this.AreButtonsDisabled = true;
          if (string.IsNullOrEmpty(this.Email) || !this.Email.Contains("@"))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INVALID_EMAIL"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (string.IsNullOrEmpty(this.Password))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_NO_VALID_PASSWORD"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.LoginAsync(this._email, this._password, false))))
          {
            AutoLoginAgent.Current.CredentialsKeepSolidID = new CredentialsKeepSolidID()
            {
              Email = this.Email,
              Password = this.Password
            };
            AppViewModel.Current.ClearNavigationStack();
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel());
          }
          this.AreButtonsDisabled = false;
        }));
      }
    }

    public ICommand RemindPasswordCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._remindPasswordCommand, (Action<object>) (async o =>
        {
          if (this.AreButtonsDisabled)
            return;
          this.AreButtonsDisabled = true;
          if (string.IsNullOrEmpty(this.Email) || !this.Email.Contains("@"))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INVALID_EMAIL"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.RemindPassword(this._email))))
            await BaseViewModel.ShowDialogAsync(LocalizedResources.GetLocalizedString("S_RECOVERY_MAIL_SENT").Replace("%s", this._email), LocalizedResources.GetLocalizedString("S_WARNING"), dialogOkTitle: this.DialogOkButtonTitle);
          this.AreButtonsDisabled = false;
        }));
      }
    }

    protected override Task<CacheState> LoadFromCacheAsync()
    {
      string keepSolidEmail = AutoLoginAgent.Current.GetKeepSolidEmail();
      if (keepSolidEmail != null)
        this.Email = keepSolidEmail;
      return base.LoadFromCacheAsync();
    }
  }
}
