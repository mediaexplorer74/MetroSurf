// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.SettingsStorageHelper
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.Storage;

#nullable disable
namespace MetroLab.Common
{
  public static class SettingsStorageHelper
  {
    public static T GetSetting<T>(string key, T defaultValue)
    {
      IPropertySet values = ApplicationData.Current.LocalSettings.Values;
      if (!((IDictionary<string, object>)values).ContainsKey(key))
        return defaultValue;
      var obj = ((IDictionary<string, object>)values)[key];
      return obj is T t ? t : defaultValue;
    }

    public static T GetSetting<T>(string key)
    {
      return GetSetting<T>(key, default(T));
    }

    public static void SetSetting(string key, object value)
    {
      ((IDictionary<string, object>)ApplicationData.Current.LocalSettings.Values)[key] = value;
    }
  }
}
