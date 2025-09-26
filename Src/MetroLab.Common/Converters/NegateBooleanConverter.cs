// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Converters.NegateBooleanConverter
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace MetroLab.Common.Converters
{
  public class NegateBooleanConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value is bool b)
      {
        if (targetType == typeof(Visibility))
          return b ? Visibility.Collapsed : Visibility.Visible;
        return !b;
      }
      if (value is Visibility v)
      {
        // invert visibility
        if (targetType == typeof(bool))
          return v != Visibility.Visible;
        return v == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
      }
      return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      if (value is bool b)
        return !b;
      if (value is Visibility v)
        return v != Visibility.Visible;
      return DependencyProperty.UnsetValue;
    }
  }
}
