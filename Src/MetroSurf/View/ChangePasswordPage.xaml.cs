using System;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VPN.View
{
    public sealed partial class ChangePasswordPage : BasePage
    {
        public ChangePasswordPage()
        {
            this.InitializeComponent();
        }

        private void OldPasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                NewPasswordBox?.Focus(FocusState.Programmatic);
            }
        }

        private void NewPasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
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