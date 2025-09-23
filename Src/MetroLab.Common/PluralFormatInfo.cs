// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.PluralFormatInfo
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Globalization;

#nullable disable
namespace MetroLab.Common
{
  public class PluralFormatInfo : IFormatProvider
  {
    private readonly PluralFormatInfo.PluralRuleDelegate pluralRule;

    public object GetFormat(Type formatType)
    {
      return formatType != typeof (PluralFormatInfo) ? (object) null : (object) this;
    }

    public PluralFormatInfo(PluralFormatInfo.PluralRuleDelegate pluralRule)
    {
      this.pluralRule = pluralRule;
    }

    public virtual PluralFormatInfo.PluralRuleDelegate GetPluralRule(CultureInfo culture)
    {
      return this.pluralRule;
    }

    public delegate int PluralRuleDelegate(Decimal value, int pluralCount);
  }
}
