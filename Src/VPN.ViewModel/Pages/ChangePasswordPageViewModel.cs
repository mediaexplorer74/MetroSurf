// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.ChangePasswordPageViewModel
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
  public class ChangePasswordPageViewModel : PageViewModel
  {
    private string _email;
    private string _password;
    private string _newPassword;
    private bool _isLoadingEnabled;
    private ICommand _changePasswordCommand;

    public string EmailTextBoxPlaceHolder
    {
      get => LocalizedResources.GetLocalizedString("S_EMAIL_TEMPLATE");
    }

    public string ChangePasswordPageHeader
    {
      get => LocalizedResources.GetLocalizedString("S_CHANGE_PASS_TITLE").ToUpper();
    }

    public string PasswordTextBoxHeader
    {
      get => LocalizedResources.GetLocalizedString("S_PASSWORD_PLACE");
    }

    public string NewPasswordTextBoxHeader
    {
      get => LocalizedResources.GetLocalizedString("S_NEW_PASSWORD");
    }

    public string EmailTextBoxHeader => LocalizedResources.GetLocalizedString("S_EMAIL_PLACE");

    public string ChangePasswordCommandButtonText
    {
      get => LocalizedResources.GetLocalizedString("S_CHANGE_PASSWORD_BTN").ToLower();
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
    public string NewPassword
    {
      get => this._newPassword;
      set => this.SetProperty<string>(ref this._newPassword, value, nameof (NewPassword));
    }

    public bool IsLoadingEnabled
    {
      get => this._isLoadingEnabled;
      set => this.SetProperty<bool>(ref this._isLoadingEnabled, value, nameof (IsLoadingEnabled));
    }

    public ICommand ChangePasswordCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._changePasswordCommand, (Action<object>) (async o =>
        {
          if (this.IsLoadingEnabled)
            return;
          this.IsLoadingEnabled = true;
          if (string.IsNullOrEmpty(this.Email) || !this.Email.Contains("@"))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INVALID_EMAIL"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.NewPassword))
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_NO_VALID_PASSWORD"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (this.NewPassword.Length < 6)
            this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_SMALL_PASS_ALERT"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          else if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.ChangePassword(this._password, this._newPassword))))
          {
            AutoLoginAgent.Current.CredentialsKeepSolidID = new CredentialsKeepSolidID()
            {
              Email = this.Email,
              Password = this.NewPassword
            };
            AutoLoginAgent.Current.Session = (string) null;
            if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await AutoLoginAgent.AutoLogin())))
            {
              await BaseViewModel.ShowDialogAsync(LocalizedResources.GetLocalizedString("S_PASS_CHANGED"), LocalizedResources.GetLocalizedString("S_WARNING"), dialogOkTitle: this.DialogOkButtonTitle);
              IPageViewModel pageViewModel = await AppViewModel.Current.GoBack();
            }
          }
          this.IsLoadingEnabled = false;
        }));
      }
    }
  }
}
