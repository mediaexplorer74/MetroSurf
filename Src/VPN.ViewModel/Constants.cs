using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.System.UserProfile;
using Windows.UI;
using Windows.UI.Xaml;

#nullable disable
namespace VPN.ViewModel
{
    public static class Constants
    {
        public const string PrivacyPolicyUrlString = "https://www.vpnunlimitedapp.com/privacy/";
        public const string FAQUrlString = "https://www.vpnunlimitedapp.com/help";
        public const string FacebookAppString = "https://www.facebook.com/vpnunlimitedapp";
        public const string TwitterAppString = "https://twitter.com/vpnunlimited";
        public const string SupportEmail = "support@keepsolid.com";
        public const string GoogleClientId = "922621893574-grqe7ehb5sq05b4ag38nf0ho19icq2lt.apps.googleusercontent.com";
        public const string GoogleClientSecret = "VqZR9tiTyrP6-UY9G48_EJTg";
        public const string GoogleCallbackUrl = "http://localhost";
        public const string EncryptPassword = "L,2W~fKo|]*,VFiZ8&n";
        public static Color StatusOffColor = Color.FromArgb(byte.MaxValue, (byte)229, (byte)20, (byte)0);
        public static Color StatusOnColor = Color.FromArgb(byte.MaxValue, (byte)131, (byte)186, (byte)31);
        private static OSVersion _OSVersion;

        public static bool IsWin10()
        {
            if (Constants._OSVersion == OSVersion.NotInitialized)
                Constants.GetPlatformVersion();
            return Constants._OSVersion == OSVersion.Win10;
        }

        public static string GetAppVersion()
        {
            try
            {
                var appType = Application.Current?.GetType();
                if (appType == null)
                    return "0.0";
                var asm = appType.GetTypeInfo().Assembly;
                var version = asm.GetName().Version;
                if (version == null)
                    return "0.0";
                return string.Format("{0}.{1}", version.Major, version.Minor);
            }
            catch
            {
                return "0.0";
            }
        }

        public static string GetDeviceID()
        {
            IBuffer id = HardwareIdentification.GetPackageSpecificToken((IBuffer)null).Id;
            return CryptographicBuffer.EncodeToHexString(HashAlgorithmProvider.OpenAlgorithm("MD5").HashData(id));
        }

        public static string GetDeviceModel() => new EasClientDeviceInformation().SystemProductName;

        public static string GetPlatformVersion()
        {
            Type type1 = Type.GetType("Windows.System.Profile.AnalyticsInfo, Windows, ContentType=WindowsRuntime");
            Type type2 = Type.GetType("Windows.System.Profile.AnalyticsVersionInfo, Windows, ContentType=WindowsRuntime");
            if (type1 == null || type2 == null)
            {
                Constants._OSVersion = OSVersion.Win8;
                return "8.1";
            }
            object obj = type1.GetRuntimeProperty("VersionInfo").GetValue((object)null);
            long result;
            if (!long.TryParse(type2.GetRuntimeProperty("DeviceFamilyVersion").GetValue(obj).ToString(), out result))
            {
                Constants._OSVersion = OSVersion.Win8;
                return "8.1";
            }
            Version version = new Version((int)(ushort)(result >> 48), (int)(ushort)(result >> 32), (int)(ushort)(result >> 16), (int)(ushort)result);
            Constants._OSVersion = OSVersion.Win10;
            return version.ToString();
        }

        public static string GetCurrentLanguage(bool isTwoLetterName = true)
        {
            return !isTwoLetterName ? GlobalizationPreferences.Languages[0] : GlobalizationPreferences.Languages[0].Substring(0, 2);
        }

        public static string GetTimeZone()
        {
            return (TimeZoneInfo.Local.BaseUtcOffset < TimeSpan.Zero ? "-" : "+") + TimeZoneInfo.Local.BaseUtcOffset.ToString("hhmm");
        }

        public static string GetCounty() => RegionInfo.CurrentRegion.DisplayName;
    }
}
