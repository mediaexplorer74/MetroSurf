// Decompiled with JetBrains decompiler
// Type: VPN.View.MainPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                WindowsRuntimeMarshal.AddEventHandler<NetworkStatusChangedEventHandler>(
                    new Func<NetworkStatusChangedEventHandler, EventRegistrationToken>(NetworkInformation.add_NetworkStatusChanged), new Action<EventRegistrationToken>(NetworkInformation.remove_NetworkStatusChanged), handler);
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
