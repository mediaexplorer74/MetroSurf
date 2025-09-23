// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310ImageAndTextOverlay03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3647369832, 3876, 23522, 112, 217, 209, 5, 164, 248, 222, 247)]
  [Version(16777216)]
  public interface ITileSquare310x310ImageAndTextOverlay03 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }

    INotificationContentText TextHeadingWrap { get; }

    INotificationContentText TextBody1 { get; }

    INotificationContentText TextBody2 { get; }

    INotificationContentText TextBody3 { get; }
  }
}
