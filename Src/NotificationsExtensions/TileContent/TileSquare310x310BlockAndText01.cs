// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310BlockAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310BlockAndText01 : 
    TileSquare310x310Base,
    ITileSquare310x310BlockAndText01,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310BlockAndText01()
      : base(nameof (TileSquare310x310BlockAndText01), (string) null, 0, 9)
    {
    }

    public INotificationContentText TextHeadingWrap => this.TextFields[0];

    public INotificationContentText TextBody1 => this.TextFields[1];

    public INotificationContentText TextBody2 => this.TextFields[2];

    public INotificationContentText TextBody3 => this.TextFields[3];

    public INotificationContentText TextBody4 => this.TextFields[4];

    public INotificationContentText TextBody5 => this.TextFields[5];

    public INotificationContentText TextBody6 => this.TextFields[6];

    public INotificationContentText TextBlock => this.TextFields[7];

    public INotificationContentText TextSubBlock => this.TextFields[8];
  }
}
