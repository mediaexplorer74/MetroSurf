// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.FileSizeFormatProvider
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;

#nullable disable
namespace MetroLab.Common
{
  public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
  {
    private const string FileSizeFormat = "fs";
    private const Decimal OneKiloByte = 1024M;
    private const Decimal OneMegaByte = 1048576M;
    private const Decimal OneGigaByte = 1073741824M;

    public object GetFormat(Type formatType)
    {
      return formatType == typeof (ICustomFormatter) ? (object) this : (object) null;
    }

    public string Format(string format, object arg, IFormatProvider formatProvider)
    {
      if (format == null || !format.StartsWith("fs"))
        return FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
      if (arg is string)
        return FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
      Decimal num;
      try
      {
        num = Convert.ToDecimal(arg);
      }
      catch (InvalidCastException ex)
      {
        return FileSizeFormatProvider.DefaultFormat(format, arg, formatProvider);
      }
      string str1;
      if (num > 1073741824M)
      {
        num /= 1073741824M;
        str1 = "GB";
      }
      else if (num > 1048576M)
      {
        num /= 1048576M;
        str1 = "MB";
      }
      else if (num > 1024M)
      {
        num /= 1024M;
        str1 = "kB";
      }
      else
        str1 = " B";
      string str2 = format.Substring(2);
      if (string.IsNullOrEmpty(str2))
        str2 = "2";
      return string.Format("{0:N" + str2 + "}{1}", (object) num, (object) str1);
    }

    private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
    {
      return arg is IFormattable formattable ? formattable.ToString(format, formatProvider) : arg.ToString();
    }
  }
}
