// Decompiled with JetBrains decompiler
// Type: VPN.Converters.BoolOppositeConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace VPN.Converters
{
  public class BoolOppositeConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (object) (bool) (!(value is bool flag) ? 1 : (!flag ? 1 : 0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotSupportedException();
    }
  }
}
