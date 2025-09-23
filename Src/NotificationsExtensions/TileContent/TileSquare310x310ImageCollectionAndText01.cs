// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310ImageCollectionAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310ImageCollectionAndText01 : 
    TileSquare310x310Base,
    ITileSquare310x310ImageCollectionAndText01,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310ImageCollectionAndText01()
      : base(nameof (TileSquare310x310ImageCollectionAndText01), (string) null, 5, 1)
    {
    }

    public INotificationContentImage ImageMain => this.Images[0];

    public INotificationContentImage ImageSmall1 => this.Images[1];

    public INotificationContentImage ImageSmall2 => this.Images[2];

    public INotificationContentImage ImageSmall3 => this.Images[3];

    public INotificationContentImage ImageSmall4 => this.Images[4];

    public INotificationContentText TextCaptionWrap => this.TextFields[0];
  }
}
