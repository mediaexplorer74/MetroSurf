// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileWide310x150Text01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileWide310x150Text01 : 
    TileWide310x150Base,
    ITileWide310x150Text01,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileWide310x150Text01()
      : base(nameof (TileWide310x150Text01), "TileWideText01", 0, 5)
    {
    }

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentText TextBody1 => this.TextFields[1];

    public INotificationContentText TextBody2 => this.TextFields[2];

    public INotificationContentText TextBody3 => this.TextFields[3];

    public INotificationContentText TextBody4 => this.TextFields[4];
  }
}
