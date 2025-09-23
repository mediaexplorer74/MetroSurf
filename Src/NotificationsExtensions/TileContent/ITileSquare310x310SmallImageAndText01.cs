// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310SmallImageAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3022286573, 13412, 20808, 94, 47, 98, 204, 174, 2, 111, 40)]
  [Version(16777216)]
  public interface ITileSquare310x310SmallImageAndText01 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextHeading { get; }

    INotificationContentText TextBodyWrap { get; }

    INotificationContentText TextBody { get; }
  }
}
