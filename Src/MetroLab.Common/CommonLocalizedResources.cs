// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.CommonLocalizedResources
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using Windows.ApplicationModel.Resources;

#nullable disable
namespace MetroLab.Common
{
  public static class CommonLocalizedResources
  {
    private static ResourceLoader _resourceLoader;

    public static string GetLocalizedString(string resourceName)
    {
      if (CommonLocalizedResources._resourceLoader == null)
        CommonLocalizedResources._resourceLoader = ResourceLoader.GetForViewIndependentUse("MetroLab.Common/Resources");
      return CommonLocalizedResources._resourceLoader.GetString(resourceName);
    }
  }
}
