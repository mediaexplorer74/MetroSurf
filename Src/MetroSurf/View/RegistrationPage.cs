using System;
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
  public sealed class RegistrationPage : BasePage
  {
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
  }
}
