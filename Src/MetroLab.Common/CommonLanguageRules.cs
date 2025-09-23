// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.CommonLanguageRules
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;

#nullable disable
namespace MetroLab.Common
{
  public static class CommonLanguageRules
  {
    public static PluralFormatInfo.PluralRuleDelegate GetPluralRule(string twoLetterISOLanguageName)
    {
      switch (twoLetterISOLanguageName)
      {
        case "af":
        case "bem":
        case "bg":
        case "bn":
        case "brx":
        case "ca":
        case "cgg":
        case "chr":
        case "da":
        case "de":
        case "dv":
        case "ee":
        case "el":
        case "en":
        case "eo":
        case "es":
        case "et":
        case "eu":
        case "fi":
        case "fo":
        case "fur":
        case "fy":
        case "gl":
        case "gsw":
        case "gu":
        case "ha":
        case "haw":
        case "he":
        case "is":
        case "it":
        case "kk":
        case "kl":
        case "ku":
        case "lb":
        case "lg":
        case "lo":
        case "mas":
        case "ml":
        case "mn":
        case "mr":
        case "nah":
        case "nb":
        case "ne":
        case "nl":
        case "nn":
        case "no":
        case "nyn":
        case "om":
        case "or":
        case "pa":
        case "pap":
        case "ps":
        case "pt":
        case "rm":
        case "saq":
        case "so":
        case "sq":
        case "ssy":
        case "sv":
        case "sw":
        case "syr":
        case "ta":
        case "te":
        case "tk":
        case "tr":
        case "ur":
        case "wae":
        case "xog":
        case "zu":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            switch (c)
            {
              case 2:
                return !(n == 1M) ? 1 : 0;
              case 3:
                if (n == 0M)
                  return 0;
                return !(n == 1M) ? 2 : 1;
              case 4:
                if (n < 0M)
                  return 0;
                if (n == 0M)
                  return 1;
                return !(n == 1M) ? 3 : 2;
              default:
                return -1;
            }
          });
        case "ak":
        case "am":
        case "bh":
        case "fil":
        case "guw":
        case "hi":
        case "ln":
        case "mg":
        case "nso":
        case "ti":
        case "tl":
        case "wa":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => !(n == 0M) && !(n == 1M) ? 1 : 0);
        case "ar":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 0M)
              return 0;
            if (n == 1M)
              return 1;
            if (n == 2M)
              return 2;
            if ((n % 100M).Between(3M, 10M))
              return 3;
            return !(n % 100M).Between(11M, 99M) ? 5 : 4;
          });
        case "az":
        case "bm":
        case "bo":
        case "dz":
        case "fa":
        case "hu":
        case "id":
        case "ig":
        case "ii":
        case "ja":
        case "jv":
        case "ka":
        case "kde":
        case "kea":
        case "km":
        case "kn":
        case "ko":
        case "ms":
        case "my":
        case "root":
        case "sah":
        case "ses":
        case "sg":
        case "th":
        case "to":
        case "vi":
        case "wo":
        case "yo":
        case "zh":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => 0);
        case "be":
        case "bs":
        case "hr":
        case "ru":
        case "sh":
        case "sr":
        case "uk":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n % 10M == 1M && !(n % 100M == 11M))
              return 0;
            return !(n % 10M).Between(2M, 4M) || (n % 100M).Between(12M, 14M) ? 2 : 1;
          });
        case "br":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 0M)
              return 0;
            if (n == 1M)
              return 1;
            if (n == 2M)
              return 2;
            if (n == 3M)
              return 3;
            return !(n == 6M) ? 5 : 4;
          });
        case "cs":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            return !n.Between(2M, 4M) ? 2 : 1;
          });
        case "cy":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 0M)
              return 0;
            if (n == 1M)
              return 1;
            if (n == 2M)
              return 2;
            if (n == 3M)
              return 3;
            return !(n == 6M) ? 5 : 4;
          });
        case "ff":
        case "fr":
        case "kab":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => !(n >= 0M) || !(n < 2M) ? 1 : 0);
        case "ga":
        case "iu":
        case "ksh":
        case "kw":
        case "se":
        case "sma":
        case "smi":
        case "smj":
        case "smn":
        case "sms":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            return !(n == 2M) ? 2 : 1;
          });
        case "gv":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => !(n % 10M).Between(1M, 2M) && !(n % 20M == 0M) ? 1 : 0);
        case "lag":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 0M)
              return 0;
            return !(n > 0M) || !(n < 2M) ? 2 : 1;
          });
        case "lt":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n % 10M == 1M && !(n % 100M).Between(11M, 19M))
              return 0;
            return !(n % 10M).Between(2M, 9M) || (n % 100M).Between(11M, 19M) ? 2 : 1;
          });
        case "lv":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 0M)
              return 0;
            return !(n % 10M == 1M) || !(n % 100M != 11M) ? 2 : 1;
          });
        case "mb":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => !(n % 10M == 1M) || !(n != 11M) ? 1 : 0);
        case "mo":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            return !(n == 0M) && (!(n != 1M) || !(n % 100M).Between(1M, 19M)) ? 2 : 1;
          });
        case "mt":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            if (n == 0M || (n % 100M).Between(2M, 10M))
              return 1;
            return !(n % 100M).Between(11M, 19M) ? 3 : 2;
          });
        case "pl":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            if ((n % 10M).Between(2M, 4M) && !(n % 100M).Between(12M, 14M))
              return 1;
            return !(n % 10M).Between(0M, 1M) && !(n % 10M).Between(5M, 9M) && !(n % 100M).Between(12M, 14M) ? 3 : 2;
          });
        case "ro":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            return !(n == 0M) && !(n % 100M).Between(1M, 19M) ? 2 : 1;
          });
        case "shi":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n >= 0M && n <= 1M)
              return 0;
            return !n.Between(2M, 10M) ? 2 : 1;
          });
        case "sk":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n == 1M)
              return 0;
            return !n.Between(2M, 4M) ? 2 : 1;
          });
        case "sl":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) =>
          {
            if (n % 100M == 1M)
              return 0;
            if (n % 100M == 2M)
              return 1;
            return !(n % 100M).Between(3M, 4M) ? 3 : 2;
          });
        case "tzm":
          return (PluralFormatInfo.PluralRuleDelegate) ((n, c) => !n.Between(0M, 1M) && !n.Between(11M, 99M) ? 1 : 0);
        default:
          return (PluralFormatInfo.PluralRuleDelegate) null;
      }
    }

    private static bool Between(this Decimal value, Decimal min, Decimal max)
    {
      return value % 1M == 0M && value >= min && value <= max;
    }
  }
}
