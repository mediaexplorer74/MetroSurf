// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.XamlIcon
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN.View.Controls
{
  public sealed class XamlIcon : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty IconNameProperty = DependencyProperty.Register(nameof (IconName), typeof (string), typeof (XamlIcon), new PropertyMetadata((object) null, new PropertyChangedCallback(XamlIcon.IconNamePropertyChnaged)));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid LayoutRootGrid;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public XamlIcon()
    {
      this.InitializeComponent();
      (this.Content as FrameworkElement).put_DataContext((object) this);
    }

    public string IconName
    {
      get => (string) ((DependencyObject) this).GetValue(XamlIcon.IconNameProperty);
      set => ((DependencyObject) this).SetValue(XamlIcon.IconNameProperty, (object) value);
    }

    public static void IconNamePropertyChnaged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is XamlIcon xamlIcon))
        return;
      xamlIcon.LoadIcon();
    }

    public void LoadIcon()
    {
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/Controls/XamlIcon.xaml"), (ComponentResourceLocation) 0);
      this.LayoutRootGrid = (Grid) ((FrameworkElement) this).FindName("LayoutRootGrid");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;
  }
}
