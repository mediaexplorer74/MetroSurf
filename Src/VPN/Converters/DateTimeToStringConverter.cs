// Decompiled with JetBrains decompiler
// Type: VPN.Converters.DateTimeToStringConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace VPN.Converters
{
  public class DateTimeToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return value != null ? (object) ((DateTime) value).ToString(parameter == null || string.IsNullOrEmpty((string) parameter) ? "HH:mm yyyy.MM.dd" : (string) parameter) : (object) null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
