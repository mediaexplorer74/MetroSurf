// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare150x150PeekImageAndText03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1287263363, 4443, 20777, 110, 200, 222, 34, 190, 95, 251, 109)]
  [Version(16777216)]
  public interface ITileSquare150x150PeekImageAndText03 : 
    ISquare150x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }

    INotificationContentText TextBody4 { get; }
  }
}
