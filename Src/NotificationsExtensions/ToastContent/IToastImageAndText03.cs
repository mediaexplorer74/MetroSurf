// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastImageAndText03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(3765209549, 24847, 22698, 108, 205, 64, 140, 124, 101, 62, 156)]
  [Version(16777216)]
  public interface IToastImageAndText03 : IToastNotificationContent, INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextHeadingWrap { get; }

    INotificationContentText TextBody { get; }
  }
}
