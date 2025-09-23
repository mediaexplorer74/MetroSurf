// Decompiled with JetBrains decompiler
// Type: VPN.View.RegistrationPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View
{
  public sealed class RegistrationPage : BasePage, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid LayoutRoot;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ProgressBar ProgressBar;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBox LoginTextbox;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private PasswordBox PasswordBox;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private PasswordBox ConfirmPasswordBox;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public RegistrationPage() => this.InitializeComponent();

    private void LoginTextBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != 13)
        return;
      ((Control) this.PasswordBox).Focus((FocusState) 2);
    }

    private void PasswordBoxOnKeyUp1(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != 13)
        return;
      ((Control) this.ConfirmPasswordBox).Focus((FocusState) 2);
    }

    private void PasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != 13)
        return;
      InputPane.GetForCurrentView().TryHide();
    }

    private void BasePage_Loaded(object sender, RoutedEventArgs e)
    {
      ((Control) this.LoginTextbox).Focus((FocusState) 2);
    }

    private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
    {
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/RegistrationPage.xaml"), (ComponentResourceLocation) 0);
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.ProgressBar = (ProgressBar) ((FrameworkElement) this).FindName("ProgressBar");
      this.LoginTextbox = (TextBox) ((FrameworkElement) this).FindName("LoginTextbox");
      this.PasswordBox = (PasswordBox) ((FrameworkElement) this).FindName("PasswordBox");
      this.ConfirmPasswordBox = (PasswordBox) ((FrameworkElement) this).FindName("ConfirmPasswordBox");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          FrameworkElement frameworkElement = (FrameworkElement) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.BasePage_Loaded));
          break;
        case 2:
          UIElement uiElement1 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<KeyEventHandler>(new Func<KeyEventHandler, EventRegistrationToken>(uiElement1.add_KeyUp), new Action<EventRegistrationToken>(uiElement1.remove_KeyUp), new KeyEventHandler(this.LoginTextBoxOnKeyUp));
          break;
        case 3:
          UIElement uiElement2 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<KeyEventHandler>(new Func<KeyEventHandler, EventRegistrationToken>(uiElement2.add_KeyUp), new Action<EventRegistrationToken>(uiElement2.remove_KeyUp), new KeyEventHandler(this.PasswordBoxOnKeyUp1));
          break;
        case 4:
          UIElement uiElement3 = (UIElement) target;
          WindowsRuntimeMarshal.AddEventHandler<KeyEventHandler>(new Func<KeyEventHandler, EventRegistrationToken>(uiElement3.add_KeyUp), new Action<EventRegistrationToken>(uiElement3.remove_KeyUp), new KeyEventHandler(this.PasswordBoxOnKeyUp));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
