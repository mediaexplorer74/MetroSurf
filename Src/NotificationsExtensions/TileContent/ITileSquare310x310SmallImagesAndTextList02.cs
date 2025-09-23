// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310SmallImagesAndTextList02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1544608252, 26405, 21302, 123, 37, 231, 254, 118, 207, 140, 117)]
  [Version(16777216)]
  public interface ITileSquare310x310SmallImagesAndTextList02 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image1 { get; }

    INotificationContentText TextWrap1 { get; }

    INotificationContentImage Image2 { get; }

    INotificationContentText TextWrap2 { get; }

    INotificationContentImage Image3 { get; }

    INotificationContentText TextWrap3 { get; }
  }
}
