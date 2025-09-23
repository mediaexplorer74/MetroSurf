// Decompiled with JetBrains decompiler
// Type: VPN.Converters.NotValidToBorderRedBrushConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

#nullable disable
namespace VPN.Converters
{
  public class NotValidToBorderRedBrushConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return value is bool flag && flag ? (object) new SolidColorBrush(Colors.Red) : ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextBoxBorderThemeBrush"];
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotSupportedException();
    }
  }
}
