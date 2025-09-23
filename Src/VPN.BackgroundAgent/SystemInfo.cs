// Decompiled with JetBrains decompiler
// Type: VPN.BackgroundAgent.SystemInfo
// Assembly: VPN.BackgroundAgent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: FF96E3FF-A49E-4455-8F7E-50F192B995C9
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.BackgroundAgent.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace VPN.BackgroundAgent
{
  [MarshalingBehavior]
  [Threading]
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  [Static(typeof (ISystemInfoStatic), 16777216)]
  public sealed class SystemInfo : ISystemInfoClass, IStringable
  {
    [MethodImpl]
    public extern SystemInfo();

    [MethodImpl]
    public static extern string GetAppVersion();

    [MethodImpl]
    public static extern string GetCurrentLanguageNameTwoLetter();

    [MethodImpl]
    public static extern string GetDeviceID();

    [MethodImpl]
    public static extern string GetDeviceModel();

    [MethodImpl]
    public static extern string GetPlatformVersion();

    [MethodImpl]
    public static extern string GetTimeZone();

    [MethodImpl]
    extern string IStringable.ToString();
  }
}
