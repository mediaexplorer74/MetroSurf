// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150ImageCollection
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(4033866804, 63951, 24497, 117, 22, 58, 22, 223, 18, 205, 201)]
  [Version(16777216)]
  public interface ITileWide310x150ImageCollection : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage ImageMain { get; }

    INotificationContentImage ImageSmallColumn1Row1 { get; }

    INotificationContentImage ImageSmallColumn2Row1 { get; }

    INotificationContentImage ImageSmallColumn1Row2 { get; }

    INotificationContentImage ImageSmallColumn2Row2 { get; }
  }
}
