using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace VPN
{
  public sealed partial class ExtendedSplash : UserControl
  {
    private DispatcherTimer _showWindowTimer;

    public ExtendedSplash()
    {
      this.InitializeComponent();

      // Wire events and configure visual elements. Named elements are provided by generated partial class from XAML.
      this.InnerImage.ImageOpened += this.ExtendedSplashImageImageOpened;
      this.ProgressRing.Width = 45;
      this.ProgressRing.Height = 45;
      this.ProgressRing.Margin = new Thickness(0, 0, 0, 180);
      this.ProgressRing.IsActive = true;
      this.InnerImage.Visibility = Visibility.Visible;
    }

    private void OnShowWindowTimer(object sender, object e)
    {
      if (this._showWindowTimer != null)
      {
        this._showWindowTimer.Tick -= this.OnShowWindowTimer;
        this._showWindowTimer.Stop();
        this._showWindowTimer = null;
      }
      Window.Current.Activate();
    }

    private void ExtendedSplashImageImageOpened(object sender, RoutedEventArgs e)
    {
      var dispatcherTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
      this._showWindowTimer = dispatcherTimer;
      this._showWindowTimer.Tick += this.OnShowWindowTimer;
      this._showWindowTimer.Start();
    }

    public void SetImageSource(Uri uri)
    {
      if (uri == null)
        return;
      this.InnerImage.Source = new BitmapImage(uri);
    }
  }
}
