// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.AppViewModel
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Networking.Connectivity;
using Windows.UI.Core;

#nullable disable
namespace MetroLab.Common
{
  public class AppViewModel : BindableBase
  {
    private static readonly Dictionary<CoreDispatcher, AppViewModel> AppViewModels = new Dictionary<CoreDispatcher, AppViewModel>();

    public CoreDispatcher Dispatcher { get; private set; }

    public static AppViewModel Current
    {
      get
      {
        return AppViewModel.GetAppViewModelForWindow(CoreWindow.GetForCurrentThread() ?? CoreApplication.MainView.CoreWindow);
      }
    }

    public static async Task<AppViewModel> GetMainAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AppViewModel.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new AppViewModel.\u003C\u003Ec__DisplayClass7_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.window = CoreApplication.MainView.CoreWindow;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.appViewModel = (AppViewModel) null;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      await cDisplayClass70.window.Dispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) cDisplayClass70, __methodptr(\u003CGetMainAsync\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      return cDisplayClass70.appViewModel;
    }

    public MvvmFrame MvvmFrame { get; set; }

    public event EventHandler<string> NewLogEntry;

    public virtual void RaiseNewLogEntry(string e)
    {
      EventHandler<string> newLogEntry = this.NewLogEntry;
      if (newLogEntry == null)
        return;
      newLogEntry((object) this, e);
    }

    private static AppViewModel GetAppViewModelForWindow(CoreWindow window)
    {
      if (window == null)
        return (AppViewModel) null;
      lock (AppViewModel.AppViewModels)
      {
        AppViewModel viewModelForWindow;
        if (!AppViewModel.AppViewModels.TryGetValue(window.Dispatcher, out viewModelForWindow))
        {
          viewModelForWindow = new AppViewModel();
          AppViewModel.AppViewModels.Add(window.Dispatcher, viewModelForWindow);
          viewModelForWindow.Dispatcher = window.Dispatcher;
        }
        return viewModelForWindow;
      }
    }

    public string NoInternetMessage
    {
      get
      {
        return CommonLocalizedResources.GetLocalizedString("exceptionMsg_ServerNotRespondOrConnectionFailure");
      }
    }

    public string NoInternetHeader
    {
      get
      {
        return CommonLocalizedResources.GetLocalizedString("exceptionHeader_ServerNotRespondOrConnectionFailure");
      }
    }

    public string LoadFailedMessage
    {
      get => CommonLocalizedResources.GetLocalizedString("exceptionMsg_LoadFailed");
    }

    public string LoadFailedHeader
    {
      get => CommonLocalizedResources.GetLocalizedString("exceptionHeader_LoadFailed");
    }

    private AppViewModel()
    {
      AppViewModel.CurrentDataTransferManager = DataTransferManager.GetForCurrentView();
      DataTransferManager dataTransferManager = AppViewModel.CurrentDataTransferManager;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Func<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, EventRegistrationToken>(dataTransferManager.add_DataRequested), new Action<EventRegistrationToken>(dataTransferManager.remove_DataRequested), new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object) this, __methodptr(CurrentDataTransferManagerDataRequested)));
      WindowsRuntimeMarshal.AddEventHandler<NetworkStatusChangedEventHandler>(new Func<NetworkStatusChangedEventHandler, EventRegistrationToken>(NetworkInformation.add_NetworkStatusChanged), new Action<EventRegistrationToken>(NetworkInformation.remove_NetworkStatusChanged), new NetworkStatusChangedEventHandler(this.NetworkInformationOnNetworkStatusChanged));
      AppViewModel.UpdateGlobalIsWorkingOffline();
    }

    private async void CurrentDataTransferManagerDataRequested(
      DataTransferManager sender,
      DataRequestedEventArgs args)
    {
      DataRequestDeferral defferal = args.Request.GetDeferral();
      if (this.MvvmFrame.Content is MvvmPage content)
      {
        IPageViewModel viewModel = content.ViewModel;
        if (viewModel != null)
          await viewModel.RequestData(sender, args);
      }
      defferal.Complete();
    }

    private void NetworkInformationOnNetworkStatusChanged(object sender)
    {
      AppViewModel.UpdateGlobalIsWorkingOffline();
    }

    public static bool GetIsConnectedToNetwork()
    {
      ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
      return connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == 3;
    }

    public async Task RunAtDispatcherAsync(DispatchedHandler action)
    {
      CoreDispatcher dispatcher = this.Dispatcher;
      if (dispatcher != null && !dispatcher.HasThreadAccess)
        dispatcher.RunAsync((CoreDispatcherPriority) 0, action);
      else
        action.Invoke();
    }

    public static bool IsWorkingOffline { get; set; }

    public static void UpdateGlobalIsWorkingOffline()
    {
      if (AppViewModel.GetIsConnectedToNetwork())
      {
        if (!AppViewModel.IsWorkingOffline)
          return;
        AppViewModel.IsWorkingOffline = false;
        AppViewModel.OnInternetConnected();
      }
      else
      {
        if (AppViewModel.IsWorkingOffline)
          return;
        AppViewModel.IsWorkingOffline = true;
        AppViewModel.OnInternetDisconnected();
      }
    }

    public static event EventHandler InternetConnected;

    protected static void OnInternetConnected()
    {
      EventHandler internetConnected = AppViewModel.InternetConnected;
      if (internetConnected == null)
        return;
      internetConnected((object) null, EventArgs.Empty);
    }

    protected static event EventHandler InternetDisconnected;

    protected static void OnInternetDisconnected()
    {
      EventHandler internetDisconnected = AppViewModel.InternetDisconnected;
      if (internetDisconnected == null)
        return;
      internetDisconnected((object) null, EventArgs.Empty);
    }

    public event EventHandler GoBackEvent;

    protected virtual void FireGoBackEvent()
    {
      EventHandler goBackEvent = this.GoBackEvent;
      if (goBackEvent == null)
        return;
      goBackEvent((object) this, EventArgs.Empty);
    }

    public Task NavigateToViewModel(IPageViewModel pageViewModel)
    {
      lock (this)
        return this.MvvmFrame.Navigate(pageViewModel);
    }

    public void ClearNavigationStack() => this.MvvmFrame.ClearNavigationStack();

    public ICommand GoBackCommand
    {
      get
      {
        ActionCommand goBackCommand = new ActionCommand();
        goBackCommand.IsCanExecute = this.MvvmFrame.CanGoBack;
        goBackCommand.ExecuteAction += (Action<object>) (o => this.GoBack());
        return (ICommand) goBackCommand;
      }
    }

    public bool CanGoBack => this.MvvmFrame.CanGoBack;

    public Task<IPageViewModel> GoBack() => this.MvvmFrame.GoBack();

    public Type HomePageViewModelType { get; set; }

    public IPageViewModel GoHome()
    {
      lock (this)
      {
        IPageViewModel instance = (IPageViewModel) Activator.CreateInstance(this.HomePageViewModelType);
        this.NavigateToViewModel(instance);
        return instance;
      }
    }

    private static DataTransferManager CurrentDataTransferManager { get; set; }
  }
}
