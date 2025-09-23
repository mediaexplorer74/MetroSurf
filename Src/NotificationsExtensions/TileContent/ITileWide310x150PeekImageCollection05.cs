// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150PeekImageCollection05
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1587708638, 52491, 24190, 65, 44, 117, 188, 80, 2, 95, 182)]
  [Version(16777216)]
  public interface ITileWide310x150PeekImageCollection05 : 
    ITileWide310x150ImageCollection,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage ImageSecondary { get; }

    INotificationContentText TextHeading { get; }

    INotificationContentText TextBodyWrap { get; }
  }
}
