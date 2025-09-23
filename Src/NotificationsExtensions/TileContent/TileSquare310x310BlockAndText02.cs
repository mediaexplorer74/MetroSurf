// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310BlockAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310BlockAndText02 : 
    TileSquare310x310Base,
    ITileSquare310x310BlockAndText02,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310BlockAndText02()
      : base(nameof (TileSquare310x310BlockAndText02), (string) null, 1, 7)
    {
    }

    public INotificationContentImage Image => this.Images[0];

    public INotificationContentText TextBlock => this.TextFields[0];

    public INotificationContentText TextHeading1 => this.TextFields[1];

    public INotificationContentText TextHeading2 => this.TextFields[2];

    public INotificationContentText TextBody1 => this.TextFields[3];

    public INotificationContentText TextBody2 => this.TextFields[4];

    public INotificationContentText TextBody3 => this.TextFields[5];

    public INotificationContentText TextBody4 => this.TextFields[6];
  }
}
