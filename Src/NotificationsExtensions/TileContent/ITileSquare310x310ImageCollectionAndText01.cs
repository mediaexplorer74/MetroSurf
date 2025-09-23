// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310ImageCollectionAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(2798916008, 63738, 24253, 73, 136, 253, 23, 107, 56, 12, 19)]
  [Version(16777216)]
  public interface ITileSquare310x310ImageCollectionAndText01 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage ImageMain { get; }

    INotificationContentImage ImageSmall1 { get; }

    INotificationContentImage ImageSmall2 { get; }

    INotificationContentImage ImageSmall3 { get; }

    INotificationContentImage ImageSmall4 { get; }

    INotificationContentText TextCaptionWrap { get; }
  }
}
