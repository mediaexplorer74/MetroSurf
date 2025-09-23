// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastImageAndText04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(2570209260, 35215, 23603, 65, 70, 219, 224, 61, 117, 204, 210)]
  [Version(16777216)]
  public interface IToastImageAndText04 : IToastNotificationContent, INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextHeading { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }
  }
}
