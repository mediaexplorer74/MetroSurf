// Decompiled with JetBrains decompiler
// Type: VPN.Converters.ToUpperStringConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using Windows.UI.Xaml.Data;

#nullable disable
namespace VPN.Converters
{
  public class ToUpperStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return string.IsNullOrEmpty(value as string) ? (object) null : (object) ((string) value).ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotSupportedException();
    }
  }
}
