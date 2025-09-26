// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.MainPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using VPN.Localization;
using VPN.Model;
using VPN.ViewModel.Adapters;
using VPN.ViewModel.Http;
using VPN.ViewModel.Items;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Store;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.ViewModel
{
  [DataContract]
  public class MainPageViewModel : BaseTilesPageViewModel
  {
    private ObservableCollection<ServerViewModel> _servers;
    private ServerViewModel _selectedServer;
    private List<PropositionViewModel> _propositionsList;
    private string _realIP;
    private string _currentIP;
    private string _timeLeft;
    private bool onOrOff;
    private bool _isExpired;
    private bool _areButtonsDisabled;
    private bool IsUserKnowHowToConnect;
    private const string TaskEntryPoint = "VPN.BackgroundAgent.UpdateApplicationTileTask";
    private const string OnTimerUpdater = "VPN is not connected on timer task";
    private const string OnInternetUpdater = "VPN on internet available task";
    private const string OnMaintenanceUpdater = "VPN on maintenance timer task";
    private ICommand _settingsCommand;
    private ICommand _addFriend;
    private ICommand _loadInAppsCommand;

    [DataMember]
    public ObservableCollection<ServerViewModel> Servers
    {
      get => this._servers;
      set
      {
        if (!this.SetProperty<ObservableCollection<ServerViewModel>>(ref this._servers, value, nameof (Servers)))
          return;
        this.OnPropertyChanged(nameof (Servers));
      }
    }

    [DataMember]
    public ServerViewModel SelectedServer
    {
      get => this._selectedServer;
      set
      {
        if (!this.SetProperty<ServerViewModel>(ref this._selectedServer, value, nameof (SelectedServer)))
          return;
        this.OnPropertyChanged(nameof (SelectedServer));
        this.OnPropertyChanged("CurrentServerText");
      }
    }

    [DataMember]
    public List<PropositionViewModel> PropositionsList
    {
      get => this._propositionsList;
      set
      {
        if (!this.SetProperty<List<PropositionViewModel>>(ref this._propositionsList, value, nameof (PropositionsList)))
          return;
        this.OnPropertyChanged(nameof (PropositionsList));
      }
    }

    [IgnoreDataMember]
    public string RealIP
    {
      get => LocalizedResources.GetLocalizedString("S_MAP_TITLE") + ": " + this._realIP;
      set => this.SetProperty<string>(ref this._realIP, value, nameof (RealIP));
    }

    [IgnoreDataMember]
    public string CurrentIP
    {
      get
      {
        return LocalizedResources.GetLocalizedString("WinPhoneVitrualIPTitle") + ": " + this._currentIP;
      }
      set => this.SetProperty<string>(ref this._currentIP, value, nameof (CurrentIP));
    }

    [IgnoreDataMember]
    public string TimeLeft
    {
      get
      {
        return this.IsExpired ? LocalizedResources.GetLocalizedString("WinPhoneTimeIsExperied") : LocalizedResources.GetLocalizedString("S_SUBSCRIPTION_REMAINING") + ": " + this._timeLeft;
      }
      set => this.SetProperty<string>(ref this._timeLeft, value, nameof (TimeLeft));
    }

    [IgnoreDataMember]
    public string CurrentServerText
    {
      get
      {
        return this.SelectedServer == null || this.SelectedServer.CountryPlusCity == null ? (string) null : LocalizedResources.GetLocalizedString("S_CONNECTED_TO").Replace("%s", this.SelectedServer.CountryPlusCity);
      }
    }

    [IgnoreDataMember]
    public string SelectServerTitle
    {
      get => LocalizedResources.GetLocalizedString("S_MAP_LIST_TITLE") + ":";
    }

    public string ConnectionProblemTitle
    {
      get => LocalizedResources.GetLocalizedString("S_CONNECTION_PROBLEM");
    }

    [IgnoreDataMember]
    public bool OnOrOff
    {
      get => this.onOrOff;
      set
      {
        if (this.SetProperty<bool>(ref this.onOrOff, value, nameof (OnOrOff)))
        {
          this.OnPropertyChanged("ConnectionBlockText");
          this.OnPropertyChanged("ConnectionBlockForeground");
        }
        this.OnPropertyChanged(nameof (OnOrOff));
      }
    }

    public bool IsExpired
    {
      get => this._isExpired;
      set => this.SetProperty<bool>(ref this._isExpired, value, nameof (IsExpired));
    }

    [IgnoreDataMember]
    public string ConnectionBlockText
    {
      get
      {
        return this.OnOrOff ? LocalizedResources.GetLocalizedString("S_MAP_FOOTER_TITLE_ON") : LocalizedResources.GetLocalizedString("S_MAP_FOOTER_TITLE_OFF");
      }
    }

    [IgnoreDataMember]
    public SolidColorBrush ConnectionBlockForeground
    {
      get
      {
        return this.OnOrOff ? new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 131, (byte) 186, (byte) 31)) : new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 229, (byte) 20, (byte) 0));
      }
    }

    [IgnoreDataMember]
    public string ContentUnavailableText
    {
      get => LocalizedResources.GetLocalizedString("S_INTERNET_PROBLEM");
    }

    [IgnoreDataMember]
    public string TryAgainText => LocalizedResources.GetLocalizedString("WinPhoneTryAgainButton");

    public string ConnectionHeaderText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneConnectionTitle").ToLower();
    }

    public string PurchasesHeaderText
    {
      get => LocalizedResources.GetLocalizedString("S_PURCHASES").ToLower();
    }

    public string GetForFreeButtonText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneGetForFreeButton").ToLower();
    }

    public string BuyMoreTitleText => LocalizedResources.GetLocalizedString("S_BUY_MORE") + ":";

    public string SettingsButtonText
    {
      get => LocalizedResources.GetLocalizedString("S_SETTINGS").ToLower();
    }

    public bool AreButtonsDisabled
    {
      get => this._areButtonsDisabled;
      set
      {
        this.SetProperty<bool>(ref this._areButtonsDisabled, value, nameof (AreButtonsDisabled));
      }
    }

    public ICommand SettingsCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._settingsCommand, (Action<object>) (async o => await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new SettingsPageViewModel())));
      }
    }

    public ICommand AddFriend
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._addFriend, (Action<object>) (async o => await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new InviteFriendPageViewModel())));
      }
    }

    [IgnoreDataMember]
    public ICommand LoadInAppsCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._loadInAppsCommand, (Action<object>) (o => this.LoadPurchases()));
      }
    }

    private async Task LoadPurchases()
    {
      // Dispatch to UI thread - original code invoked UI logic here; leave as no-op placeholder
      await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { /* UI work placeholder */ });
    }

    protected override async Task<bool> LoadAsync()
    {
      this.LoadLocalization();
      this.IsUserKnowHowToConnect = CacheAgent.IsUserKnowHowToConnect;
      await this.LoadPurchases();
      return await this.LoadAsync(VPNServerAgent.Current);
    }

    public async void SelectServer(ServerViewModel selectedServer)
    {
      if (this.AreButtonsDisabled)
        return;
      this.AreButtonsDisabled = true;
      if (!this.IsExpired)
      {
        if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.GetConnectionCredentialsAsync(selectedServer.Region))))
        {
          Config config = VPNServerAgent.Current.VPNConnectionConfig;
          if (!CacheAgent.IsUserKnowHowToConnect)
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new GuideGalleryPageViewModel(config));
          else if (Constants.IsWin10())
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new ServerCredentialsPageViewModelWin10(selectedServer, config));
          else
            await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new ServerCredentialsPageViewModel(selectedServer, config));
          config = (Config) null;
        }
      }
      this.AreButtonsDisabled = false;
    }

    protected async Task<bool> LoadAsync(VPNServerAgent agent)
    {
      if (!await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await agent.GetVPNServersListAsync())))
        return false;
      ServerToServerViewModelAdapter viewModelAdapter = new ServerToServerViewModelAdapter();
      VPNServerAgent.Current.ServersList.Items.Sort(new Comparison<Server>(Server.Compare));
      this.Servers = new ObservableCollection<ServerViewModel>(VPNServerAgent.Current.ServersList.Items.Select<Server, ServerViewModel>(new Func<Server, ServerViewModel>(((BaseAdapter<Server, ServerViewModel>) viewModelAdapter).Convert)));
      int num = await this.UpdateStatusAsync() ? 1 : 0;
      return true;
    }

    public async Task<bool> UpdateStatusAsync()
    {
      bool isSuccess = false;
      try
      {
        isSuccess = await VPNServerAgent.Current.GetAccountAsync();
      }
      catch (Exception ex)
      {
        this.OnOrOff = false;
        return false;
      }
      if (!isSuccess || VPNServerAgent.Current.ServersList == null)
      {
        this.OnOrOff = false;
        return false;
      }
      ResponceOnAccountStatus account = VPNServerAgent.Current.AccountStatus;
      this.OnOrOff = account.User.IsVPNActive;
      this.RealIP = account.User.RealIP;
      this.CurrentIP = account.User.CurrentIP;
      if ((int) account.User.TimeLeft == 0)
        this.IsExpired = true;
      TimeSpan timeSpan = new TimeSpan(0, 0, (int) account.User.TimeLeft);
      if (timeSpan.TotalDays > 18250.0)
        this.TimeLeft = LocalizedResources.GetLocalizedString("S_INIFINITE_PLAN");
      else if (timeSpan.TotalDays > 365.0)
        this.TimeLeft = ((int) (timeSpan.TotalDays / 365.0)).ToString() + " " + LocalizedResources.GetLocalizedString("S_YEAR") + " " + (object) (int) (timeSpan.TotalDays % 365.0 / 30.0) + " " + LocalizedResources.GetLocalizedString("S_MONTHS");
      else if (timeSpan.TotalDays > 30.0)
        this.TimeLeft = ((int) (timeSpan.TotalDays / 30.0)).ToString() + " " + LocalizedResources.GetLocalizedString("S_MONTHS") + " " + (object) (int) (timeSpan.TotalDays % 30.0) + " " + LocalizedResources.GetLocalizedString("S_DAYS");
      else
        this.TimeLeft = ((int) timeSpan.TotalDays).ToString() + " " + LocalizedResources.GetLocalizedString("S_DAYS") + " " + (object) timeSpan.Hours + " " + LocalizedResources.GetLocalizedString("S_HOURS");
      if (this.OnOrOff)
      {
        if (!this.IsUserKnowHowToConnect)
        {
          CacheAgent.IsUserKnowHowToConnect = true;
          this.IsUserKnowHowToConnect = true;
        }
        this.SelectedServer = this.Servers.Where<ServerViewModel>((Func<ServerViewModel, bool>) (f => f.Region == account.User.VPNRegion)).First<ServerViewModel>();
      }
      return true;
    }

    protected override async Task<bool> UpdateInnerAsync() => await this.LoadAsync();

    public async void SelectProposition(PropositionViewModel selectedProposition)
    {
      if (this.AreButtonsDisabled)
        return;
      this.AreButtonsDisabled = true;
      try
      {
        PurchaseResults purchaseResults = await CurrentApp.RequestProductPurchaseAsync(selectedProposition.ID);

        if (purchaseResults != null && purchaseResults.Status == ProductPurchaseStatus.Succeeded)
        {
          // Try to report consumable fulfillment if applicable, ignore failures
          try
          {
            await CurrentApp.ReportConsumableFulfillmentAsync(selectedProposition.ID, purchaseResults.TransactionId);
          }
          catch
          {
          }

          ProductLicense productLicense = null;
          if (CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(selectedProposition.ID, out productLicense))
          {
            if (await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.PurchaseAsync(purchaseResults.ReceiptXml, purchaseResults.TransactionId.ToString()))))
            {
              // show confirmation dialog and call CommandHandlers when OK pressed
              await BaseViewModel.ShowDialogAsync(LocalizedResources.GetLocalizedString("S_PURCHASE_CONFIRMED"), (string) null, new UICommandInvokedHandler(this.CommandHandlers));
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      this.AreButtonsDisabled = false;
    }

    public async void CommandHandlers(IUICommand commandLabel)
    {
      int num = await this.UpdateStatusAsync() ? 1 : 0;
    }
  }
}
