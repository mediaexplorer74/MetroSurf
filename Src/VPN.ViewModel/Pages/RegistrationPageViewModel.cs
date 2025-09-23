// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.RegistrationPageViewModel
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
  public class RegistrationPageViewModel : PageViewModel
  {
    private string _email;
    private string _password;
    private string _confirmPassword;
    private bool _isLoadingEnabled;
    private ICommand _registerCommand;

    public string EmailTextBoxPlaceHolder
    {
      get => LocalizedResources.GetLocalizedString("S_EMAIL_TEMPLATE");
    }

    public string RegistrationPageHeader
    {
      get => LocalizedResources.GetLocalizedString("S_SIGN_CREATE").ToUpper();
    }

    public string PasswordTextBoxHeader
    {
      get => LocalizedResources.GetLocalizedString("S_PASSWORD_PLACE");
    }

    public string ConfirmPasswordTextBoxHeader
    {
      get => LocalizedResources.GetLocalizedString("S_CONFIRM_PASS_PLACE");
    }

    public string EmailTextBoxHeader => LocalizedResources.GetLocalizedString("S_EMAIL_PLACE");

    public string RegisterButtonText
    {
      get => LocalizedResources.GetLocalizedString("S_REGISTER").ToLower();
    }

    public string ConfirmEmailDescriptionTextBlock
    {
      get => LocalizedResources.GetLocalizedString("S_CONFIRM_MAIL");
    }

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

    [DataMember]
    public string ConfirmPassword
    {
      get => this._confirmPassword;
      set => this.SetProperty<string>(ref this._confirmPassword, value, nameof (ConfirmPassword));
    }

    public bool IsLoadingEnabled
    {
      get => this._isLoadingEnabled;
      set => this.SetProperty<bool>(ref this._isLoadingEnabled, value, nameof (IsLoadingEnabled));
    }

    public ICommand RegisterCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._registerCommand, (Action<object>) (async o =>
        {
          this.IsLoadingEnabled = true;
          if (string.IsNullOrEmpty(this.Email) || !this.Email.Contains("@"))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INVALID_EMAIL"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (this.Password != this.ConfirmPassword)
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INVALID_CONFIRM"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (string.IsNullOrEmpty(this.Password))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_NO_VALID_PASSWORD"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (this.Password.Length < 6)
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_SMALL_PASS_ALERT"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.RegisterAsync(this._email, this._password))))
          {
            AutoLoginAgent.Current.CredentialsKeepSolidID = new CredentialsKeepSolidID()
            {
              Email = this.Email,
              Password = this.Password
            };
            AppViewModel.Current.ClearNavigationStack();
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new MainPageViewModel());
          }
          this.IsLoadingEnabled = false;
        }));
      }
    }
  }
}
