// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.Header
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using VPN.Localization;
using VPN.ViewModel.Http;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View.Controls
{
  public sealed class Header : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty IsStatusNeededProperty = DependencyProperty.Register(nameof (IsStatusNeeded), typeof (bool), typeof (Header), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsStatusBarColorNeededProperty = DependencyProperty.Register(nameof (IsStatusBarColorNeeded), typeof (bool), typeof (Header), new PropertyMetadata((object) null, new PropertyChangedCallback(Header.OnOrOffPropertyChnaged)));
    public static readonly DependencyProperty OnOrOffProperty = DependencyProperty.Register(nameof (OnOrOff), typeof (bool), typeof (Header), new PropertyMetadata((object) null, new PropertyChangedCallback(Header.OnOrOffPropertyChnaged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public Header()
    {
      this.InitializeComponent();
      (this.Content as FrameworkElement).put_DataContext((object) this);
      if (VPNServerAgent.Current.AccountStatus != null)
        this.OnOrOff = VPNServerAgent.Current.AccountStatus.User.IsVPNActive;
      StatusBar forCurrentView = StatusBar.GetForCurrentView();
      if (!this.IsStatusBarColorNeeded)
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 97, (byte) 97, (byte) 97)));
      else if (this.OnOrOff)
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 131, (byte) 186, (byte) 31)));
      else
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 229, (byte) 20, (byte) 0)));
      forCurrentView.put_ForegroundColor(new Color?(Colors.White));
      forCurrentView.put_BackgroundOpacity(1.0);
    }

    public bool IsStatusNeeded
    {
      get => (bool) ((DependencyObject) this).GetValue(Header.IsStatusNeededProperty);
      set => ((DependencyObject) this).SetValue(Header.IsStatusNeededProperty, (object) value);
    }

    public bool IsStatusBarColorNeeded
    {
      get => (bool) ((DependencyObject) this).GetValue(Header.IsStatusBarColorNeededProperty);
      set
      {
        ((DependencyObject) this).SetValue(Header.IsStatusBarColorNeededProperty, (object) value);
      }
    }

    public bool OnOrOff
    {
      get => (bool) ((DependencyObject) this).GetValue(Header.OnOrOffProperty);
      set => ((DependencyObject) this).SetValue(Header.OnOrOffProperty, (object) value);
    }

    public static void OnOrOffPropertyChnaged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Header header = d as Header;
      StatusBar forCurrentView = StatusBar.GetForCurrentView();
      if (!header.IsStatusBarColorNeeded)
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 97, (byte) 97, (byte) 97)));
      else if (header.OnOrOff)
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 131, (byte) 186, (byte) 31)));
      else
        forCurrentView.put_BackgroundColor(new Color?(Color.FromArgb(byte.MaxValue, (byte) 229, (byte) 20, (byte) 0)));
      forCurrentView.put_ForegroundColor(new Color?(Colors.White));
      forCurrentView.put_BackgroundOpacity(1.0);
    }

    public string ApplicationName => LocalizedResources.GetLocalizedString("app_name");

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/Controls/Header.xaml"), (ComponentResourceLocation) 0);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
