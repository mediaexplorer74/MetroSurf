// Decompiled with JetBrains decompiler
// Type: VPN.BackgroundAgent.SystemInfo
// Assembly: VPN.BackgroundAgent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: FF96E3FF-A49E-4455-8F7E-50F192B995C9
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.BackgroundAgent.winmd

using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace VPN.BackgroundAgent
{
    public sealed class SystemInfo
    {
        public SystemInfo() { }

        public static string GetAppVersion() => string.Empty;
        public static string GetCurrentLanguageNameTwoLetter() => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        public static string GetDeviceID() => string.Empty;
        public static string GetDeviceModel() => string.Empty;
        public static string GetPlatformVersion() => string.Empty;
        public static string GetTimeZone() => TimeZoneInfo.Local.StandardName;

        public override string ToString() => "SystemInfo";
    }
}
