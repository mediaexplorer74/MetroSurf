// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.CommonLanguagesPluralFormatInfo
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.Globalization;

#nullable disable
namespace MetroLab.Common
{
  public class CommonLanguagesPluralFormatInfo : PluralFormatInfo
  {
    private readonly string _defaultTwoLetterISOLanguageName;

    public CommonLanguagesPluralFormatInfo(string defaultTwoLetterIsoLanguageName)
      : base((PluralFormatInfo.PluralRuleDelegate) null)
    {
      this._defaultTwoLetterISOLanguageName = defaultTwoLetterIsoLanguageName;
    }

    public override PluralFormatInfo.PluralRuleDelegate GetPluralRule(CultureInfo cultureInfo)
    {
      return cultureInfo != null ? CommonLanguageRules.GetPluralRule(cultureInfo.TwoLetterISOLanguageName) : CommonLanguageRules.GetPluralRule(this._defaultTwoLetterISOLanguageName);
    }
  }
}
