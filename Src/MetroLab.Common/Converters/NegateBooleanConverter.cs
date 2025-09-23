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
      return (object) !(bool) value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      return (object) (bool) (!(value is Visibility) ? 0 : ((Visibility) value == 0 ? 1 : 0));
    }
  }
}
