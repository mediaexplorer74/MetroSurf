// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310SmallImagesAndTextList05
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1353501685, 59227, 23174, 108, 115, 41, 84, 162, 50, 229, 46)]
  [Version(16777216)]
  public interface ITileSquare310x310SmallImagesAndTextList05 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentImage Image1 { get; }

    INotificationContentText TextGroup1Field1 { get; }

    INotificationContentText TextGroup1Field2 { get; }

    INotificationContentImage Image2 { get; }

    INotificationContentText TextGroup2Field1 { get; }

    INotificationContentText TextGroup2Field2 { get; }

    INotificationContentImage Image3 { get; }

    INotificationContentText TextGroup3Field1 { get; }

    INotificationContentText TextGroup3Field2 { get; }
  }
}
