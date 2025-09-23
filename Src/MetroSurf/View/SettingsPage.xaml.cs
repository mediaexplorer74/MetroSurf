// Decompiled with JetBrains decompiler
// Type: VPN.View.SettingsPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using VPN.ViewModel;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View
{
    public sealed partial class SettingsPage : BasePage
    {
        //public Grid LoadedContentGrid;
        //private TextBlock TextBlockVersion;
        //private TextBlock TextBlockYearAndCopyRight;
        //private Canvas icn_share;
        //private Canvas icn_star;
        //private Canvas icn_support;
        //private Canvas icn_tutorial;
        //private Canvas Your_Icon;
        //private ToggleSwitch RememberPasswordToggle;
        //private Canvas icn_kid;

        public SettingsPage()
        {
            this.InitializeComponent();
            // set version and copyright if elements exist in XAML
            if (TextBlockVersion != null)
                TextBlockVersion.Text = string.Format("{0} {1}", "VPN Unlimited", Constants.GetAppVersion());
            if (TextBlockYearAndCopyRight != null)
                TextBlockYearAndCopyRight.Text = "© 2015 KeepSolid Inc.";
            if (RememberPasswordToggle != null)
                RememberPasswordToggle.IsOn = !((string)ApplicationData.Current.LocalSettings.Values["RememberPassword"] == "don't remember");
        }

        private async void SendFeedbackOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await RatingsHelper.SendFeedback();
            }
            catch (Exception ex)
            {
                if (!Debugger.IsAttached)
                    return;
                Debugger.Break();
            }
        }

        private async void AppOverviewOnClick(object sender, RoutedEventArgs e)
        {
            await AppViewModel.Current.NavigateToViewModel(new OverviewGalleryPageViewModel()
            {
                IsLoadedFromSettings = true
            });
        }

        private async void MakeReviewOnClick(object sender, RoutedEventArgs e)
        {
            await RatingsHelper.MakeReview();
        }

        private async void ShareOnClick(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void RememberPasswordToggleChecked(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["RememberPassword"] = RememberPasswordToggle.IsOn ? (object)null : (object)"don't remember";
        }

        
    }
}
