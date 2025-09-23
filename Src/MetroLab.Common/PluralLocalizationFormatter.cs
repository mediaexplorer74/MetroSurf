// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.PluralLocalizationFormatter
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Globalization;

#nullable disable
namespace MetroLab.Common
{
  public class PluralLocalizationFormatter
  {
    private readonly PluralFormatInfo _defaultPluralFormatInfo;

    public PluralLocalizationFormatter(string defaultTwoLetterISOLanguageName)
    {
      this._defaultPluralFormatInfo = (PluralFormatInfo) new CommonLanguagesPluralFormatInfo(defaultTwoLetterISOLanguageName);
    }

    public PluralLocalizationFormatter(PluralFormatInfo defaultPluralFormatInfo)
    {
      this._defaultPluralFormatInfo = defaultPluralFormatInfo;
    }

    private PluralFormatInfo.PluralRuleDelegate GetPluralRule(IFormatProvider provider)
    {
      PluralFormatInfo pluralFormatInfo = (PluralFormatInfo) null;
      if (provider != null)
        pluralFormatInfo = (PluralFormatInfo) provider.GetFormat(typeof (PluralFormatInfo));
      if (pluralFormatInfo == null)
        pluralFormatInfo = this._defaultPluralFormatInfo;
      if (pluralFormatInfo == null)
        return (PluralFormatInfo.PluralRuleDelegate) null;
      CultureInfo culture = provider as CultureInfo;
      return pluralFormatInfo.GetPluralRule(culture);
    }
  }
}
