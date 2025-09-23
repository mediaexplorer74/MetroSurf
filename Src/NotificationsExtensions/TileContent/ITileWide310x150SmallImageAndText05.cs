// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150SmallImageAndText05
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(2807616614, 17507, 23269, 82, 85, 143, 122, 107, 49, 220, 67)]
  [Version(16777216)]
  public interface ITileWide310x150SmallImageAndText05 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextHeading { get; }

    INotificationContentText TextBodyWrap { get; }
  }
}
