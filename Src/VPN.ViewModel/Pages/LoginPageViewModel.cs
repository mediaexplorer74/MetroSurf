// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.LoginPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using VPN.Localization;
using VPN.Model.SocialNetworks;
using VPN.ViewModel.Http;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class LoginPageViewModel : BaseTilesPageViewModel
  {
    private bool _isNeedToShowContent;
    private LoginPageViewModel.SocialNetwork provider;
    private ICommand _loginFacebookCommand;
    private ICommand _loginGooglePlusCommand;
    private ICommand _loginCommand;
    private ICommand _registerCommand;

    [IgnoreDataMember]
    public string LoginFacebookString => LocalizedResources.GetLocalizedString("S_SIGN_FACEBOOK");

    [IgnoreDataMember]
    public string LoginGooglePlusString => LocalizedResources.GetLocalizedString("S_SIGN_GOOGLE");

    [IgnoreDataMember]
    public string LoginKeepsolidIdString => LocalizedResources.GetLocalizedString("S_SIGN_NATIVE");

    [IgnoreDataMember]
    public string RegisterNewUserString => LocalizedResources.GetLocalizedString("S_SIGN_CREATE");

    [IgnoreDataMember]
    public string LoginPageDescription => LocalizedResources.GetLocalizedString("S_SIGN_TITLE");

    [IgnoreDataMember]
    public string ContentUnavailableText
    {
      get => LocalizedResources.GetLocalizedString("S_INTERNET_PROBLEM");
    }

    [IgnoreDataMember]
    public string TryAgainText => LocalizedResources.GetLocalizedString("WinPhoneTryAgainButton");

    [IgnoreDataMember]
    public bool IsNeedToShowContent
    {
      get => this._isNeedToShowContent & this.IsLoadedSuccessfully;
      set
      {
        this.SetProperty<bool>(ref this._isNeedToShowContent, value, nameof (IsNeedToShowContent));
      }
    }

    public ICommand LoginFacebookCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loginFacebookCommand, (Action<object>) (async o =>
        {
          if (this.IsLoadingActive)
            return;
          this.IsLoadingActive = true;
          this.provider = LoginPageViewModel.SocialNetwork.Facebook;
          int num = await FacebookAuthenticationAgent.AuthenticateWithApp() ? 1 : 0;
          this.IsLoadingActive = false;
        }));
      }
    }

    public ICommand LoginGooglePlusCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loginGooglePlusCommand, (Action<object>) (async o =>
        {
          if (this.IsLoadingActive)
            return;
          this.IsLoadingActive = true;
          this.provider = LoginPageViewModel.SocialNetwork.Google;
          GoogleAuthenticationAgent.Current.LoginAsync();
        }));
      }
    }

    public ICommand LoginCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loginCommand, (Action<object>) (async o =>
        {
          if (this.IsLoadingActive)
            return;
          await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new LoginKeepSolidIDPageViewModel());
        }));
      }
    }

    public ICommand RegisterCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._registerCommand, (Action<object>) (async o =>
        {
          if (this.IsLoadingActive)
            return;
          await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new RegistrationPageViewModel());
        }));
      }
    }

    public async void Finalize(WebAuthenticationBrokerContinuationEventArgs args)
    {
      if (this.provider == LoginPageViewModel.SocialNetwork.Google)
      {
        if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await GoogleAuthenticationAgent.Current.GetSession(args))))
        {
          if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.LoginGoogleAsync(AutoLoginAgent.Current.CredentialsGoogle))))
          {
            AppViewModel.Current.ClearNavigationStack();
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel());
          }
        }
      }
      else if (this.provider == LoginPageViewModel.SocialNetwork.Facebook)
      {
        CredentialsFacebook credentialsFacebook;
        CredentialsFacebook credentialsFacebook1 = credentialsFacebook;
        credentialsFacebook = await FacebookAuthenticationAgent.Current.Finalize(args);
        if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.LoginFacebookAsync(credentialsFacebook))))
        {
          AppViewModel.Current.ClearNavigationStack();
          await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel());
        }
      }
      this.IsLoadingActive = false;
    }

    public async void FinalizeAppAuthentication(string queryString)
    {
      this.IsLoadingActive = true;
      if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await FacebookAuthenticationAgent.Current.AuthenticationFromAppReceivedAsync(queryString))))
      {
        CredentialsFacebook facebookCredentials = AutoLoginAgent.Current.CredentialsFacebook;
        if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.LoginFacebookAsync(facebookCredentials))))
        {
          AppViewModel.Current.ClearNavigationStack();
          await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel());
        }
      }
      this.IsLoadingActive = false;
    }

    protected override async Task<bool> LoadAsync()
    {
      this.LoadLocaliztion();
      if ((string) ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["RememberPassword"] == "don't remember")
      {
        AutoLoginAgent.Current.CredentialsFacebook = (CredentialsFacebook) null;
        AutoLoginAgent.Current.CredentialsGoogle = (CredentialsGoogle) null;
        AutoLoginAgent.Current.CredentialsKeepSolidID = (CredentialsKeepSolidID) null;
        CacheAgent.DeleteLoginCache();
        return true;
      }
      if (!await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await AutoLoginAgent.AutoLogin())))
      {
        if (this.IsWasProblemsWithConnection)
          return false;
        AutoLoginAgent.Current.CredentialsFacebook = (CredentialsFacebook) null;
        AutoLoginAgent.Current.CredentialsGoogle = (CredentialsGoogle) null;
        AutoLoginAgent.Current.CredentialsKeepSolidID = (CredentialsKeepSolidID) null;
        CacheAgent.DeleteLoginCache();
      }
      return true;
    }

    protected override async Task<bool> UpdateInnerAsync() => await this.LoadAsync();

    private enum SocialNetwork
    {
      Facebook,
      Google,
    }
  }
}
