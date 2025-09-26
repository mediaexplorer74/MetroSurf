/*using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using MetroLab.Common;

#nullable disable
namespace VPN.View
{
    public sealed partial class GuideGalleryPage : BasePage
    {
        public GuideGalleryPage()
        {
            this.InitializeComponent();
        }

        private void FlipViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If needed, react to selection changes (placeholder mirrors OverviewGallery behavior)
        }

        private void Next(object sender, TappedRoutedEventArgs e)
        {
            if (this.InnerFlipView != null)
            {
                var idx = this.InnerFlipView.SelectedIndex;
                if (idx < this.InnerFlipView.Items?.Count - 1)
                    this.InnerFlipView.SelectedIndex = idx + 1;
            }
        }
    }
}*/
// Decompiled with JetBrains decompiler
// Type: VPN.View.GuideGalleryPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using VPN.ViewModel.Items;
using VPN.ViewModel.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.View
{
    public sealed partial class GuideGalleryPage : MvvmPage
    {
        public GuideGalleryPage()
        {
            this.InitializeComponent();
        }

        private async void Next(object sender, TappedRoutedEventArgs e)
        {
            if (this.InnerFlipView.Items == null || this.InnerFlipView.SelectedIndex >= this.InnerFlipView.Items.Count - 1)
                return;
            await Task.Delay(100);
            this.InnerFlipView.SelectedIndex = this.InnerFlipView.SelectedIndex + 1;
        }

        private async void FlipViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
                return;
            GuidePageViewModel addedItem = (GuidePageViewModel)e.AddedItems[0];
            if (addedItem == null || addedItem.IsLastPage)
                return;
            if (addedItem.DataToCopy != null)
                this.SelectDataToCopy();
            foreach (GuidePageViewModel guidePageViewModel in ((GuideGalleryPageViewModel)this.ViewModel).Items)
            {
                guidePageViewModel.IsNextButtonIsVisible = false;
                guidePageViewModel.IsSelected = false;
            }
            addedItem.IsSelected = true;
            await addedItem.ShowNextButtonAsync();
        }

        public void SelectDataToCopy()
        {
            DependencyObject parent = this.InnerFlipView.ItemContainerGenerator.ContainerFromItem(this.InnerFlipView.SelectedItem) as DependencyObject;
            if (parent == null)
                return;
            var textBox = this.AllChildren(parent).OfType<TextBox>().First(x => x.Name == "DataToCopyTextBox");
            textBox.IsEnabled = true;
            textBox.Focus(FocusState.Programmatic);
            textBox.SelectAll();
        }

        public List<Control> AllChildren(DependencyObject parent)
        {
            List<Control> controlList = new List<Control>();
            for (int index = 0; index < VisualTreeHelper.GetChildrenCount(parent); ++index)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, index);
                if (child is Control)
                    controlList.Add(child as Control);
                controlList.AddRange(this.AllChildren(child));
            }
            return controlList;
        }
    }
}
