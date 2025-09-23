// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare150x150Text04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare150x150Text04 : 
    TileSquare150x150Base,
    ITileSquare150x150Text04,
    ISquare150x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare150x150Text04()
      : base(nameof (TileSquare150x150Text04), "TileSquareText04", 0, 1)
    {
    }

    public INotificationContentText TextBodyWrap => this.TextFields[0];
  }
}
