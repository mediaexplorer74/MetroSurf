// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310ImageCollectionAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(4108863186, 61774, 21569, 69, 224, 217, 152, 109, 179, 14, 36)]
  [Version(16777216)]
  public interface ITileSquare310x310ImageCollectionAndText02 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage ImageMain { get; }

    INotificationContentImage ImageSmall1 { get; }

    INotificationContentImage ImageSmall2 { get; }

    INotificationContentImage ImageSmall3 { get; }

    INotificationContentImage ImageSmall4 { get; }

    INotificationContentText TextCaption1 { get; }

    INotificationContentText TextCaption2 { get; }
  }
}
