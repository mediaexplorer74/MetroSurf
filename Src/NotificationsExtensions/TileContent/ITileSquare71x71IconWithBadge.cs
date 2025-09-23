// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare71x71IconWithBadge
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1802308205, 53138, 22309, 65, 116, 131, 183, 154, 175, 13, 158)]
  [Version(16777216)]
  public interface ITileSquare71x71IconWithBadge : 
    ISquare71x71TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage ImageIcon { get; }
  }
}
