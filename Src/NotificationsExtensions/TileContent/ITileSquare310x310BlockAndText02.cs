// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310BlockAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(558372205, 1701, 22327, 125, 181, 96, 169, 240, 214, 21, 74)]
  [Version(16777216)]
  public interface ITileSquare310x310BlockAndText02 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextBlock { get; }

    INotificationContentText TextHeading1 { get; }

    INotificationContentText TextHeading2 { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }
  }
}
