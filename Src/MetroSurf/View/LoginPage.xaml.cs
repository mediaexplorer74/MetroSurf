using System;
using VPN.Common;
using VPN.ViewModel.Http;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View
{
  public sealed partial class LoginPage : BasePage
  {
    public static LoginPage Current { get; private set; }

    public LoginPage()
    {
      this.InitializeComponent();
      LoginPage.Current = this;
    }

    public void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
    {
      var vm = this.DataContext as LoginPageViewModel;
      vm?.Finalize(args);
    }

    public void ContinueAppAuthentication(string queryString)
    {
      var vm = this.DataContext as LoginPageViewModel;
      vm?.FinalizeAppAuthentication(queryString);
    }

    protected override async void OnViewModelLoadedOrUpdatedSuccessfully()
    {
      if (AutoLoginAgent.Current.Session != null)
      {
        var dispatcher = Window.Current?.Dispatcher;
        if (dispatcher != null)
          await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { /* UI update for auto-login can be placed here */ });
      }
      base.OnViewModelLoadedOrUpdatedSuccessfully();
    }
  }
}
