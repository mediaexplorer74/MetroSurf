// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150BlockAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1204714112, 36992, 23482, 74, 16, 154, 237, 29, 189, 61, 239)]
  [Version(16777216)]
  public interface ITileWide310x150BlockAndText02 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextBodyWrap { get; }

    INotificationContentText TextBlock { get; }

    INotificationContentText TextSubBlock { get; }
  }
}
