// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150PeekImageCollection03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3351486811, 3933, 21600, 101, 131, 202, 53, 126, 218, 3, 23)]
  [Version(16777216)]
  public interface ITileWide310x150PeekImageCollection03 : 
    ITileWide310x150ImageCollection,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeadingWrap { get; }
  }
}
