// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310ImageAndTextOverlay02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310ImageAndTextOverlay02 : 
    TileSquare310x310Base,
    ITileSquare310x310ImageAndTextOverlay02,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310ImageAndTextOverlay02()
      : base(nameof (TileSquare310x310ImageAndTextOverlay02), (string) null, 1, 2)
    {
    }

    public INotificationContentImage Image => this.Images[0];

    public INotificationContentText TextHeadingWrap => this.TextFields[0];

    public INotificationContentText TextBodyWrap => this.TextFields[1];
  }
}
