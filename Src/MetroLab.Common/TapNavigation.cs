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
      Hyperlink hyperlink1 = (Hyperlink) d;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(new Action<EventRegistrationToken>(hyperlink1.remove_Click), new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>((object) null, __methodptr(HyperlinkOnClick)));
      if (args.NewValue == null)
        return;
      Hyperlink hyperlink2 = hyperlink1;
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(new Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken>(hyperlink2.add_Click), new Action<EventRegistrationToken>(hyperlink2.remove_Click), new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>((object) null, __methodptr(HyperlinkOnClick)));
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
