// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150Text11
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(4240206662, 52665, 22255, 65, 137, 154, 207, 223, 94, 129, 63)]
  [Version(16777216)]
  public interface ITileWide310x150Text11 : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentText TextPrefixColumn1Row1 { get; }

    INotificationContentText TextColumn2Row1 { get; }

    INotificationContentText TextPrefixColumn1Row2 { get; }

    INotificationContentText TextColumn2Row2 { get; }

    INotificationContentText TextPrefixColumn1Row3 { get; }

    INotificationContentText TextColumn2Row3 { get; }

    INotificationContentText TextPrefixColumn1Row4 { get; }

    INotificationContentText TextColumn2Row4 { get; }

    INotificationContentText TextPrefixColumn1Row5 { get; }

    INotificationContentText TextColumn2Row5 { get; }
  }
}
