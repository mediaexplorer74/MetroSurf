using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VPN.View
{
    public sealed partial class LoginKeepSolidIDPage : BasePage
    {
        public LoginKeepSolidIDPage()
        {
            this.InitializeComponent();
        }

        private void LoginTextBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                PasswordBox?.Focus(FocusState.Programmatic);
            }
        }

        private void PasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                InputPane.GetForCurrentView()?.TryHide();
            }
        }
    }
}