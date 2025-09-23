using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View.Controls
{
  public sealed partial class XamlIcon : UserControl
  {
    public static readonly DependencyProperty IconNameProperty = DependencyProperty.Register(nameof(IconName), typeof(string), typeof(XamlIcon), new PropertyMetadata((object)null, new PropertyChangedCallback(XamlIcon.IconNamePropertyChnaged)));
    //private Grid LayoutRootGrid;

    public string IconName
    {
      get => (string)this.GetValue(XamlIcon.IconNameProperty);
      set => this.SetValue(XamlIcon.IconNameProperty, (object)value);
    }

    public static void IconNamePropertyChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is XamlIcon xamlIcon)
        xamlIcon.LoadIcon();
    }

    public void LoadIcon()
    {
    }
  }
}
