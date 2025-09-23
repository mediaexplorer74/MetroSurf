// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastText04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(1179466295, 4972, 23560, 68, 197, 24, 39, 163, 138, 167, 123)]
  [Version(16777216)]
  public interface IToastText04 : IToastNotificationContent, INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }
  }
}
