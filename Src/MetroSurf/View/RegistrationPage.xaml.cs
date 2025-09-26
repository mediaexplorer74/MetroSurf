using System;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

#nullable disable
namespace VPN.View
{
    public sealed partial class RegistrationPage : BasePage
    {
        public RegistrationPage()
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

        private void PasswordBoxOnKeyUp1(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ConfirmPasswordBox?.Focus(FocusState.Programmatic);
            }
        }

        private void PasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                InputPane.GetForCurrentView()?.TryHide();
            }
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTextbox?.Focus(FocusState.Programmatic);
        }
    }
}
