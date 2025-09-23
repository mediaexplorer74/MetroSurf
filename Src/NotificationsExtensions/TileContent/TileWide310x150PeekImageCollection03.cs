// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileWide310x150PeekImageCollection03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileWide310x150PeekImageCollection03 : 
    TileWide310x150Base,
    ITileWide310x150PeekImageCollection03,
    ITileWide310x150ImageCollection,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileWide310x150PeekImageCollection03()
      : base(nameof (TileWide310x150PeekImageCollection03), "TileWidePeekImageCollection03", 5, 1)
    {
    }

    public INotificationContentImage ImageMain => this.Images[0];

    public INotificationContentImage ImageSmallColumn1Row1 => this.Images[1];

    public INotificationContentImage ImageSmallColumn2Row1 => this.Images[2];

    public INotificationContentImage ImageSmallColumn1Row2 => this.Images[3];

    public INotificationContentImage ImageSmallColumn2Row2 => this.Images[4];

    public INotificationContentText TextHeadingWrap => this.TextFields[0];
  }
}
