// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150ImageAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(2815664674, 14118, 20874, 89, 203, 124, 146, 252, 220, 89, 210)]
  [Version(16777216)]
  public interface ITileWide310x150ImageAndText02 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextCaption1 { get; }

    INotificationContentText TextCaption2 { get; }
  }
}
