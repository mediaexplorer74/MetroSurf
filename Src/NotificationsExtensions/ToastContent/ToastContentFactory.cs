// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastContentFactory
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [MarshalingBehavior]
  [Threading]
  [Version(16777216)]
  [CompilerGenerated]
  [Static(typeof (IToastContentFactoryStatic), 16777216)]
  public sealed class ToastContentFactory : IToastContentFactoryClass, IStringable
  {
    [MethodImpl]
    public static extern IToastImageAndText01 CreateToastImageAndText01();

    [MethodImpl]
    public static extern IToastImageAndText02 CreateToastImageAndText02();

    [MethodImpl]
    public static extern IToastImageAndText03 CreateToastImageAndText03();

    [MethodImpl]
    public static extern IToastImageAndText04 CreateToastImageAndText04();

    [MethodImpl]
    public static extern IToastText01 CreateToastText01();

    [MethodImpl]
    public static extern IToastText02 CreateToastText02();

    [MethodImpl]
    public static extern IToastText03 CreateToastText03();

    [MethodImpl]
    public static extern IToastText04 CreateToastText04();

    [MethodImpl]
    extern string IStringable.ToString();
  }
}
