// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150Text10
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3655563342, 46886, 23150, 78, 75, 15, 111, 159, 73, 183, 92)]
  [Version(16777216)]
  public interface ITileWide310x150Text10 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextPrefixColumn1Row1 { get; }

    INotificationContentText TextColumn2Row1 { get; }

    INotificationContentText TextPrefixColumn1Row2 { get; }

    INotificationContentText TextColumn2Row2 { get; }

    INotificationContentText TextPrefixColumn1Row3 { get; }

    INotificationContentText TextColumn2Row3 { get; }

    INotificationContentText TextPrefixColumn1Row4 { get; }

    INotificationContentText TextColumn2Row4 { get; }
  }
}
