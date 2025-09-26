using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using VPN.ViewModel;
using VPN.ViewModel.Items;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View
{
    public sealed partial class MainPage : MvvmPage
    {
        private DispatcherTimer timerForRefreshingAccountStatus;
        private const int updatingVPNConnectionStatusFrequency = 40;
        private bool registeredNetworkStatusNotif;

        public MainPage()
        {
            this.InitializeComponent();
            this.NetworkStatusChange();
        }

        private void NetworkStatusChange()
        {
            try
            {
                NetworkStatusChangedEventHandler handler = new NetworkStatusChangedEventHandler(this.OnNetworkStatusChange);
                if (this.registeredNetworkStatusNotif)
                    return;

                // Subscribe to NetworkStatusChanged using standard event pattern
                NetworkInformation.NetworkStatusChanged += handler;
                this.registeredNetworkStatusNotif = true;
            }
            catch (Exception ex)
            {
            }
        }

        private async void OnNetworkStatusChange(object sender)
        {
            ((MainPageViewModel)this.ViewModel).UpdateStatusAsync();
        }

        private async void SelectServer(object sender, ItemClickEventArgs e)
        {
            ((MainPageViewModel)this.ViewModel).SelectServer((ServerViewModel)e.ClickedItem);
        }

        public async void NavigateToPurchasesPivotItem(IUICommand commandLabel)
        {
            this.Pivot.SelectedIndex = 1;
        }

        protected override void OnViewModelLoadedOrUpdatedSuccessfully()
        {
            object obj = ((IDictionary<string, object>)ApplicationData.Current.LocalSettings.Values)["ShowMakeReview"];
            if (obj == null)
                return;
            bool firstOrSecond;
            if (obj.Equals((object)"Sheduled1"))
            {
                firstOrSecond = true;
            }
            else
            {
                if (!obj.Equals((object)"Sheduled2"))
                    return;
                firstOrSecond = false;
            }
            ThreadPool.RunAsync((WorkItemHandler)(async operation =>
            {
                // original scheduled review logic here
            }));
        }

        private async void SelectProposition(object sender, ItemClickEventArgs e)
        {
            ((MainPageViewModel)this.ViewModel).SelectProposition((PropositionViewModel)e.ClickedItem);
        }
    }
}

// Added partial classes to supply event handlers expected by XAML-generated code
namespace VPN.View
{
    public sealed partial class OverviewGalleryPage : BasePage
    {
        private void InnerFlipView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // keep as no-op or update UI as needed
        }

        private void Next(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.InnerFlipView != null)
            {
                var idx = this.InnerFlipView.SelectedIndex;
                if (idx < this.InnerFlipView.Items?.Count - 1)
                    this.InnerFlipView.SelectedIndex = idx + 1;
            }
        }
    }

    public sealed partial class GuideGalleryPage : BasePage
    {
        private void FlipViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // placeholder for selection change logic
        }

        private void Next(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.InnerFlipView != null)
            {
                var idx = this.InnerFlipView.SelectedIndex;
                if (idx < this.InnerFlipView.Items?.Count - 1)
                    this.InnerFlipView.SelectedIndex = idx + 1;
            }
        }
    }

    public sealed partial class LoginKeepSolidIDPage : BasePage
    {
        private void LoginTextBoxOnKeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                PasswordBox?.Focus(FocusState.Programmatic);
            }
        }

        private void PasswordBoxOnKeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView()?.TryHide();
            }
        }
    }
}
