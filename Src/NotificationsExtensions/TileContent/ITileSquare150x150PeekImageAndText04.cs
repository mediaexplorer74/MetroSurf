// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare150x150PeekImageAndText04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1959605568, 61850, 23093, 68, 82, 130, 225, 4, 17, 196, 23)]
  [Version(16777216)]
  public interface ITileSquare150x150PeekImageAndText04 : 
    ISquare150x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextBodyWrap { get; }
  }
}
