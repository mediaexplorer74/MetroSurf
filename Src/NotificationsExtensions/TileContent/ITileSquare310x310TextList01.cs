// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310TextList01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3186536765, 12715, 22427, 83, 109, 94, 2, 166, 187, 53, 174)]
  [Version(16777216)]
  public interface ITileSquare310x310TextList01 : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextHeading1 { get; }

    INotificationContentText TextBodyGroup1Field1 { get; }

    INotificationContentText TextBodyGroup1Field2 { get; }

    INotificationContentText TextHeading2 { get; }

    INotificationContentText TextBodyGroup2Field1 { get; }

    INotificationContentText TextBodyGroup2Field2 { get; }

    INotificationContentText TextHeading3 { get; }

    INotificationContentText TextBodyGroup3Field1 { get; }

    INotificationContentText TextBodyGroup3Field2 { get; }
  }
}
