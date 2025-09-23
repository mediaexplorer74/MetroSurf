// Decompiled with JetBrains decompiler
// Type: VPN.Converters.NullOrEmptyToVisibleConverter
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable disable
namespace VPN.Converters
{
  public class NullOrEmptyToVisibleConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (object) (Visibility) (string.IsNullOrEmpty(value as string) ? 0 : 1);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotSupportedException();
    }
  }
}
