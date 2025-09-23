// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310SmallImagesAndTextList04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(367768247, 36531, 21733, 125, 50, 238, 33, 102, 251, 19, 130)]
  [Version(16777216)]
  public interface ITileSquare310x310SmallImagesAndTextList04 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image1 { get; }

    INotificationContentText TextHeading1 { get; }

    INotificationContentText TextWrap1 { get; }

    INotificationContentImage Image2 { get; }

    INotificationContentText TextHeading2 { get; }

    INotificationContentText TextWrap2 { get; }

    INotificationContentImage Image3 { get; }

    INotificationContentText TextHeading3 { get; }

    INotificationContentText TextWrap3 { get; }
  }
}
