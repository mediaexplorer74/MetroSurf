// Decompiled with JetBrains decompiler
// Type: VPN.Localization.LocalizedResources
// Assembly: VPN.Localization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E9A7C50B-A679-4D24-8E43-2BA4BE115C30
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Localization.dll

using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel;
using Windows.Storage;
using System.Collections.Generic;
using System;
using System.IO;
using Windows.Data.Json;

#nullable disable
namespace VPN.Localization
{
  public static class LocalizedResources
  {
    private static ResourceLoader _resourceLoader;
    private static ResourceLoader _defaultResourceLoader;
    private static Dictionary<string, string> _fallbackStrings;

    public static string GetLocalizedString(string resourceName)
    {
      // Primary: explicit namespaced resource file
      try
      {
        if (LocalizedResources._resourceLoader == null)
          LocalizedResources._resourceLoader = ResourceLoader.GetForViewIndependentUse("VPN.Localization/Resources");
        string val = LocalizedResources._resourceLoader.GetString(resourceName);
        if (!string.IsNullOrEmpty(val))
          return val;
      }
      catch
      {
        // ignore and try fallback
      }

      // Secondary: default resource loader for app
      try
      {
        if (LocalizedResources._defaultResourceLoader == null)
          LocalizedResources._defaultResourceLoader = ResourceLoader.GetForViewIndependentUse();
        string val2 = LocalizedResources._defaultResourceLoader.GetString(resourceName);
        if (!string.IsNullOrEmpty(val2))
          return val2;
      }
      catch
      {
      }

      // Tertiary: load fallback JSON from app package (ms-appx:///Assets/Localization/ResourcesFallback.json)
      try
      {
        EnsureFallbackLoaded();
        if (LocalizedResources._fallbackStrings != null && LocalizedResources._fallbackStrings.TryGetValue(resourceName, out string v))
          return v;
      }
      catch
      {
      }

      // Final fallback: return the resource key itself so UI remains usable
      return resourceName;
    }

    private static void EnsureFallbackLoaded()
    {
      if (LocalizedResources._fallbackStrings != null)
        return;
      try
      {
        // Attempt synchronous load of fallback JSON from package
        var uri = new Uri("ms-appx:///Assets/Localization/ResourcesFallback.json");
        StorageFile file = StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().GetAwaiter().GetResult();
        if (file != null)
        {
          string text = FileIO.ReadTextAsync(file).AsTask().GetAwaiter().GetResult();
          if (!string.IsNullOrWhiteSpace(text))
          {
            // Parse using Windows.Data.Json for UWP compatibility
            try
            {
              JsonObject jobj = JsonObject.Parse(text);
              var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
              foreach (var pair in jobj)
              {
                string key = pair.Key;
                IJsonValue jval = pair.Value;
                string sval = string.Empty;
                if (jval.ValueType == JsonValueType.String)
                  sval = jval.GetString();
                else
                  sval = jval.Stringify();
                dict[key] = sval;
              }
              LocalizedResources._fallbackStrings = dict;
              return;
            }
            catch
            {
              // fall through to empty dictionary
            }
          }
        }
      }
      catch
      {
        // ignore any errors reading fallback
      }

      LocalizedResources._fallbackStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
  }
}
