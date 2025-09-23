// Decompiled with JetBrains decompiler
// Type: VPN.View.SettingsPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using VPN.ViewModel;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View
{
  public sealed class SettingsPage : BasePage, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public Grid LoadedContentGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock TextBlockVersion;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private TextBlock TextBlockYearAndCopyRight;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_share;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_star;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_support;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_tutorial;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas Your_Icon;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private ToggleSwitch RememberPasswordToggle;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Canvas icn_kid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public SettingsPage()
    {
      this.InitializeComponent();
      this.TextBlockVersion.put_Text(string.Format("{0} {1}", (object) "VPN Unlimited", (object) Constants.GetAppVersion()));
      this.TextBlockYearAndCopyRight.put_Text("© 2015 KeepSolid Inc.");
      this.RememberPasswordToggle.put_IsOn(!((string) ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["RememberPassword"] == "don't remember"));
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
      await AppViewModel.Current.NavigateToViewModel((IPageViewModel) new OverviewGalleryPageViewModel()
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
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["RememberPassword"] = this.RememberPasswordToggle.IsOn ? (object) (string) null : (object) "don't remember";
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/SettingsPage.xaml"), (ComponentResourceLocation) 0);
      this.LoadedContentGrid = (Grid) ((FrameworkElement) this).FindName("LoadedContentGrid");
      this.TextBlockVersion = (TextBlock) ((FrameworkElement) this).FindName("TextBlockVersion");
      this.TextBlockYearAndCopyRight = (TextBlock) ((FrameworkElement) this).FindName("TextBlockYearAndCopyRight");
      this.icn_share = (Canvas) ((FrameworkElement) this).FindName("icn_share");
      this.icn_star = (Canvas) ((FrameworkElement) this).FindName("icn_star");
      this.icn_support = (Canvas) ((FrameworkElement) this).FindName("icn_support");
      this.icn_tutorial = (Canvas) ((FrameworkElement) this).FindName("icn_tutorial");
      this.Your_Icon = (Canvas) ((FrameworkElement) this).FindName("Your_Icon");
      this.RememberPasswordToggle = (ToggleSwitch) ((FrameworkElement) this).FindName("RememberPasswordToggle");
      this.icn_kid = (Canvas) ((FrameworkElement) this).FindName("icn_kid");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ButtonBase buttonBase1 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase1.add_Click), new Action<EventRegistrationToken>(buttonBase1.remove_Click), new RoutedEventHandler(this.AppOverviewOnClick));
          break;
        case 2:
          ButtonBase buttonBase2 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase2.add_Click), new Action<EventRegistrationToken>(buttonBase2.remove_Click), new RoutedEventHandler(this.SendFeedbackOnClick));
          break;
        case 3:
          ButtonBase buttonBase3 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase3.add_Click), new Action<EventRegistrationToken>(buttonBase3.remove_Click), new RoutedEventHandler(this.MakeReviewOnClick));
          break;
        case 4:
          ButtonBase buttonBase4 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase4.add_Click), new Action<EventRegistrationToken>(buttonBase4.remove_Click), new RoutedEventHandler(this.ShareOnClick));
          break;
        case 5:
          ToggleSwitch toggleSwitch = (ToggleSwitch) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.RememberPasswordToggleChecked));
          break;
      }
      this._contentLoaded = true;
    }
  }
}
