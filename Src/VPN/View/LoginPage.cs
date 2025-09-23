// Decompiled with JetBrains decompiler
// Type: VPN.View.LoginPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using VPN.Common;
using VPN.ViewModel.Http;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View
{
  public sealed class LoginPage : BasePage, IWebAuthenticationContinuable, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid LayoutRoot;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ProgressBar ProgressBar;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ProgressBar ProgressBarForAutoLogin;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid LoginButtons;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_add;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_kid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_g;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_f;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas logo_keepsolid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public static LoginPage Current { get; private set; }

    public LoginPage()
    {
      this.InitializeComponent();
      LoginPage.Current = this;
    }

    public void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
    {
      ((LoginPageViewModel) this.ViewModel).Finalize(args);
    }

    public void ContinueAppAuthentication(string queryString)
    {
      ((LoginPageViewModel) this.ViewModel).FinalizeAppAuthentication(queryString);
    }

    protected override async void OnViewModelLoadedOrUpdatedSuccessfully()
    {
      if (AutoLoginAgent.Current.Session != null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        await ((DependencyObject) this).Dispatcher.RunAsync((CoreDispatcherPriority) 0, LoginPage.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (LoginPage.\u003C\u003Ec.\u003C\u003E9__7_0 = new DispatchedHandler((object) LoginPage.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003COnViewModelLoadedOrUpdatedSuccessfully\u003Eb__7_0))));
      }
      base.OnViewModelLoadedOrUpdatedSuccessfully();
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/LoginPage.xaml"), (ComponentResourceLocation) 0);
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.ProgressBar = (ProgressBar) ((FrameworkElement) this).FindName("ProgressBar");
      this.ProgressBarForAutoLogin = (ProgressBar) ((FrameworkElement) this).FindName("ProgressBarForAutoLogin");
      this.LoginButtons = (Grid) ((FrameworkElement) this).FindName("LoginButtons");
      this.icn_add = (Canvas) ((FrameworkElement) this).FindName("icn_add");
      this.icn_kid = (Canvas) ((FrameworkElement) this).FindName("icn_kid");
      this.icn_g = (Canvas) ((FrameworkElement) this).FindName("icn_g");
      this.icn_f = (Canvas) ((FrameworkElement) this).FindName("icn_f");
      this.logo_keepsolid = (Canvas) ((FrameworkElement) this).FindName("logo_keepsolid");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
