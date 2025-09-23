// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310Text01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(109569906, 6065, 21829, 88, 173, 117, 38, 45, 46, 128, 178)]
  [Version(16777216)]
  public interface ITileSquare310x310Text01 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }

    INotificationContentText TextBody5 { get; }

    INotificationContentText TextBody6 { get; }

    INotificationContentText TextBody7 { get; }

    INotificationContentText TextBody8 { get; }

    INotificationContentText TextBody9 { get; }
  }
}
