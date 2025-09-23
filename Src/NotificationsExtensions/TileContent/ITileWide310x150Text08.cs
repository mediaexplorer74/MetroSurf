// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150Text08
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3075966099, 5431, 24354, 109, 30, 180, 213, 65, 43, 242, 6)]
  [Version(16777216)]
  public interface ITileWide310x150Text08 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextShortColumn1Row1 { get; }

    INotificationContentText TextShortColumn1Row2 { get; }

    INotificationContentText TextShortColumn1Row3 { get; }

    INotificationContentText TextShortColumn1Row4 { get; }

    INotificationContentText TextShortColumn1Row5 { get; }

    INotificationContentText TextColumn2Row1 { get; }

    INotificationContentText TextColumn2Row2 { get; }

    INotificationContentText TextColumn2Row3 { get; }

    INotificationContentText TextColumn2Row4 { get; }

    INotificationContentText TextColumn2Row5 { get; }
  }
}
