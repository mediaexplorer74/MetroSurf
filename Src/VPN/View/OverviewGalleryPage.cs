// Decompiled with JetBrains decompiler
// Type: VPN.View.OverviewGalleryPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VPN.ViewModel;
using VPN.ViewModel.Items;
using VPN.ViewModel.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View
{
  public sealed partial class OverviewGalleryPage : MvvmPage
  {
    public OverviewGalleryPage()
    {
      this.InitializeComponent();
    }

    private async void Next(object sender, TappedRoutedEventArgs e)
    {
      if (this.InnerFlipView.Items != null && this.InnerFlipView.SelectedIndex < this.InnerFlipView.Items.Count - 1)
      {
        await Task.Delay(100);
        this.InnerFlipView.SelectedIndex = this.InnerFlipView.SelectedIndex + 1;
      }
      else
      {
        CacheAgent.IsNeedToShowApplicationTour = false;
        if (((OverviewGalleryPageViewModel) this.ViewModel).IsLoadedFromSettings)
          AppViewModel.Current.GoBack();
        else
          AppViewModel.Current.NavigateToViewModel((IPageViewModel) new LoginPageViewModel());
      }
    }

    private async void InnerFlipView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.AddedItems == null || e.AddedItems.Count == 0)
        return;
      OverviewPageViewModel addedItem = (OverviewPageViewModel) e.AddedItems[0];
      if (addedItem == null || addedItem.IsLastPage)
        return;
      foreach (OverviewPageViewModel overviewPageViewModel in ((OverviewGalleryPageViewModel) this.ViewModel).Items)
      {
        overviewPageViewModel.IsNextButtonIsVisible = false;
        overviewPageViewModel.IsSelected = false;
      }
      addedItem.IsSelected = true;
      await addedItem.ShowNextButtonAsync();
    }
  }
}
