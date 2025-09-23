// Decompiled with JetBrains decompiler
// Type: VPN.Converters.BoolToColorConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using VPN.ViewModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.Converters
{
  public class BoolToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return !(bool) value ? (object) new SolidColorBrush(Constants.StatusOffColor) : (object) new SolidColorBrush(Constants.StatusOnColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotSupportedException();
    }
  }
}
