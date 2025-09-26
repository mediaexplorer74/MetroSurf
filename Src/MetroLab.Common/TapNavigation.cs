// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.TapNavigation
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

#nullable disable
namespace MetroLab.Common
{
  public class TapNavigation
  {
    public static readonly DependencyProperty NavigateUriProperty = DependencyProperty.RegisterAttached("NavigateUri", typeof (string), typeof (TapNavigation), new PropertyMetadata((object) null, new PropertyChangedCallback(TapNavigation.OnNavigateUriPropertyChanged)));

    public static string GetNavigateUri(DependencyObject frameworkControl)
    {
      return (string) frameworkControl.GetValue(TapNavigation.NavigateUriProperty);
    }

    public static void SetNavigateUri(DependencyObject frameworkControl, string value)
    {
      frameworkControl.SetValue(TapNavigation.NavigateUriProperty, (object) value);
    }

    private static void OnNavigateUriPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(d is Hyperlink hyperlink))
        return;
      try { hyperlink.Click -= HyperlinkOnClick; } catch { }
      if (args.NewValue == null)
        return;
      hyperlink.Click += HyperlinkOnClick;
    }

    private static void HyperlinkOnClick(Hyperlink sender, HyperlinkClickEventArgs args)
    {
      string navigateUri = TapNavigation.GetNavigateUri((DependencyObject) sender);
      if (string.IsNullOrEmpty(navigateUri))
        return;
      Launcher.LaunchUriAsync(new Uri(navigateUri, UriKind.RelativeOrAbsolute));
    }
  }
}
