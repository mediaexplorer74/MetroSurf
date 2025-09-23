// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310BlockAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1527544783, 28192, 23060, 89, 214, 126, 86, 141, 115, 112, 196)]
  [Version(16777216)]
  public interface ITileSquare310x310BlockAndText01 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeadingWrap { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }

    INotificationContentText TextBody5 { get; }

    INotificationContentText TextBody6 { get; }

    INotificationContentText TextBlock { get; }

    INotificationContentText TextSubBlock { get; }
  }
}
