// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.ServerCredentialsPageViewModelWin10
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Windows.Input;
using VPN.Localization;
using VPN.Model;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class ServerCredentialsPageViewModelWin10 : PageViewModel
  {
    private ServerViewModel _server;
    private Config _config;
    private ICommand _openGuideGalleryPage;

    public string ServerDetailsMessageText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneServerDetailsMessage");
    }

    public string ProtocolValueText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneConnectionTypeL2TP");
    }

    public string PasswordTitleText
    {
      get => LocalizedResources.GetLocalizedString("S_PASSWORD_PLACE") + ":";
    }

    public string UserNameText => LocalizedResources.GetLocalizedString("S_PROXY_USER_NAME") + ":";

    public string ProtocolTypeTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneProtocolTypeTitle") + ":";
    }

    public string ManualButtonText
    {
      get => LocalizedResources.GetLocalizedString("S_HOW_TO").ToLowerInvariant();
    }

    public string VPNProviderTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10VPNProviderTitle") + ":";
    }

    public string VPNProviderValueText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10VPNProviderValue");
    }

    public string ServerNameTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10ServerNameTitle") + ":";
    }

    public string VPNTypeTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10VPNTypeTitle") + ":";
    }

    public string PreSharedTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10PreSharedTitle") + ":";
    }

    public string UsernameTitleText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10UsernameTitle") + ":";
    }

    public string VPNTypeValueText
    {
      get => LocalizedResources.GetLocalizedString("WinPhone10VPNTypeValue");
    }

    [DataMember]
    public ServerViewModel Server
    {
      get => this._server;
      set => this.SetProperty<ServerViewModel>(ref this._server, value, nameof (Server));
    }

    [DataMember]
    public Config Config
    {
      get => this._config;
      set => this.SetProperty<Config>(ref this._config, value, nameof (Config));
    }

    public ServerCredentialsPageViewModelWin10(ServerViewModel server, Config config)
    {
      this.Server = server;
      this.Config = config;
    }

    public ICommand OpenGuideGalleryPage
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._openGuideGalleryPage, (Action<object>) (async o => await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new GuideGalleryPageViewModel(this.Config))));
      }
    }
  }
}
