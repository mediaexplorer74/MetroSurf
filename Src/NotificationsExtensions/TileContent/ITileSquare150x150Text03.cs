// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare150x150Text03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(2364431808, 19110, 20752, 102, 193, 201, 251, 160, 81, 95, 105)]
  [Version(16777216)]
  public interface ITileSquare150x150Text03 : 
    ISquare150x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }
  }
}
