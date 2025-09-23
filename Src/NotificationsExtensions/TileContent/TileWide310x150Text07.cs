// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileWide310x150Text07
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileWide310x150Text07 : 
    TileWide310x150Base,
    ITileWide310x150Text07,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileWide310x150Text07()
      : base(nameof (TileWide310x150Text07), "TileWideText07", 0, 9)
    {
    }

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentText TextShortColumn1Row1 => this.TextFields[1];

    public INotificationContentText TextColumn2Row1 => this.TextFields[2];

    public INotificationContentText TextShortColumn1Row2 => this.TextFields[3];

    public INotificationContentText TextColumn2Row2 => this.TextFields[4];

    public INotificationContentText TextShortColumn1Row3 => this.TextFields[5];

    public INotificationContentText TextColumn2Row3 => this.TextFields[6];

    public INotificationContentText TextShortColumn1Row4 => this.TextFields[7];

    public INotificationContentText TextColumn2Row4 => this.TextFields[8];
  }
}
