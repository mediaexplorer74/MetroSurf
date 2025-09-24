using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VPN.View
{
    public sealed partial class OverviewGalleryPage : BasePage
    {
        public OverviewGalleryPage()
        {
            this.InitializeComponent();
        }

        private void InnerFlipView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Bind to ViewModel if needed
        }

        private void Next(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Implement navigation to next item
        }
    }
}