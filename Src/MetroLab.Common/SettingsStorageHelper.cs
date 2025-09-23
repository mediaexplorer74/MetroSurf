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
    public static T GetSetting<T>(string key, T defaultValue = null)
    {
      IPropertySet values = ApplicationData.Current.LocalSettings.Values;
      return !((IDictionary<string, object>) values).ContainsKey(key) || !(((IDictionary<string, object>) values)[key] is T) ? defaultValue : (T) ((IDictionary<string, object>) values)[key];
    }

    public static void SetSetting(string key, object value)
    {
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)[key] = value;
    }
  }
}
