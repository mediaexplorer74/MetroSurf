// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare71x71IconWithBadge
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare71x71IconWithBadge : 
    TileSquare71x71Base,
    ITileSquare71x71IconWithBadge,
    ISquare71x71TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare71x71IconWithBadge()
      : base(nameof (TileSquare71x71IconWithBadge), (string) null, 1, 0)
    {
    }

    public INotificationContentImage ImageIcon => this.Images[0];
  }
}
