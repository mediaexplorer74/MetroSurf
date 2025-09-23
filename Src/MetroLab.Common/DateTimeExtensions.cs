// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.DateTimeExtensions
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#nullable disable
namespace MetroLab.Common
{
  public static class DateTimeExtensions
  {
    private static readonly string[] SupportedLanguages = new string[2]
    {
      "en",
      "ru"
    };

    private static string GetMostSuitableLangTwoLetters()
    {
      string lower = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLower();
      return !((IEnumerable<string>) DateTimeExtensions.SupportedLanguages).Contains<string>(lower) ? "en" : lower;
    }

    public static string ToUserFriendlyTimeAgoString(this DateTime d)
    {
      PluralFormatInfo.PluralRuleDelegate rule = CommonLanguageRules.GetPluralRule(DateTimeExtensions.GetMostSuitableLangTwoLetters());
      Func<int, string, string> func = (Func<int, string, string>) ((number, stringVariants) =>
      {
        try
        {
          string[] array = ((IEnumerable<string>) stringVariants.Split(';')).Select<string, string>((Func<string, string>) (x => x.Trim())).ToArray<string>();
          string str = array[rule((Decimal) number, array.Length)];
          return string.Format("{0} {1}", (object) number, (object) str);
        }
        catch (Exception ex)
        {
          if (Debugger.IsAttached)
            Debugger.Break();
          return number.ToString() + " ??? ";
        }
      });
      int totalDays = (int) DateTime.Now.Subtract(d).TotalDays;
      if (totalDays / 365 >= 1)
        return CommonLocalizedResources.GetLocalizedString("txt_datestring_overayearago");
      int num1 = totalDays / 31;
      if (num1 >= 1)
        return func(num1, CommonLocalizedResources.GetLocalizedString("txt_datestring_monthsago"));
      int num2 = totalDays / 7;
      if (num2 >= 1)
        return func(num2, CommonLocalizedResources.GetLocalizedString("txt_datestring_weeksago"));
      if (totalDays == 0)
        return CommonLocalizedResources.GetLocalizedString("txt_datestring_today");
      return totalDays == 1 ? CommonLocalizedResources.GetLocalizedString("txt_datestring_yesterday") : func(totalDays, CommonLocalizedResources.GetLocalizedString("txt_datestring_daysago"));
    }

    public static string ToUserFriendlyString(this TimeSpan timeSpan)
    {
      PluralFormatInfo.PluralRuleDelegate rule = CommonLanguageRules.GetPluralRule(DateTimeExtensions.GetMostSuitableLangTwoLetters());
      Func<int, string, string> func = (Func<int, string, string>) ((number, stringVariants) =>
      {
        try
        {
          string[] array = ((IEnumerable<string>) stringVariants.Split(';')).Select<string, string>((Func<string, string>) (x => x.Trim())).ToArray<string>();
          string str = array[rule((Decimal) number, array.Length)];
          return string.Format("{0} {1}", (object) number, (object) str);
        }
        catch (Exception ex)
        {
          if (Debugger.IsAttached)
            Debugger.Break();
          return "";
        }
      });
      string userFriendlyString = string.Empty;
      if (timeSpan.Days > 0)
        userFriendlyString = userFriendlyString + func(timeSpan.Days, CommonLocalizedResources.GetLocalizedString("days")) + " ";
      if (timeSpan.Hours > 0)
        userFriendlyString = userFriendlyString + func(timeSpan.Hours, CommonLocalizedResources.GetLocalizedString("hours")) + " ";
      if (timeSpan.Minutes > 0)
        userFriendlyString = userFriendlyString + func(timeSpan.Minutes, CommonLocalizedResources.GetLocalizedString("minutes")) + " ";
      if (timeSpan.Seconds > 0)
        userFriendlyString = userFriendlyString + func(timeSpan.Seconds, CommonLocalizedResources.GetLocalizedString("seconds")) + " ";
      return userFriendlyString;
    }
  }
}
