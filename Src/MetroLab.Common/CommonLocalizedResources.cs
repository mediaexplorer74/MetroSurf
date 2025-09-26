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

        private static ResourceLoader Loader
            => _resourceLoader ??= CreateLoader();

        private static ResourceLoader CreateLoader()
        {
            try
            {
                // try component-specific map first
                return ResourceLoader.GetForViewIndependentUse("MetroLab.Common/Resources");
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                // fallback to app-level resources
                return ResourceLoader.GetForViewIndependentUse();
            }
        }

        public static string GetLocalizedString(string resourceName) =>
            Loader.GetString(resourceName);
    }
}
