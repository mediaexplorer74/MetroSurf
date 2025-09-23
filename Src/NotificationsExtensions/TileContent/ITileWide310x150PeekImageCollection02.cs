// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150PeekImageCollection02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(135185970, 44433, 22466, 99, 226, 16, 99, 231, 12, 146, 217)]
  [Version(16777216)]
  public interface ITileWide310x150PeekImageCollection02 : 
    ITileWide310x150ImageCollection,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }
  }
}
