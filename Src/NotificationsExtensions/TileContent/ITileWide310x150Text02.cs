// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150Text02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1860258592, 29595, 20752, 116, 100, 74, 36, 10, 246, 104, 21)]
  [Version(16777216)]
  public interface ITileWide310x150Text02 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextColumn1Row1 { get; }

    INotificationContentText TextColumn2Row1 { get; }

    INotificationContentText TextColumn1Row2 { get; }

    INotificationContentText TextColumn2Row2 { get; }

    INotificationContentText TextColumn1Row3 { get; }

    INotificationContentText TextColumn2Row3 { get; }

    INotificationContentText TextColumn1Row4 { get; }

    INotificationContentText TextColumn2Row4 { get; }
  }
}
