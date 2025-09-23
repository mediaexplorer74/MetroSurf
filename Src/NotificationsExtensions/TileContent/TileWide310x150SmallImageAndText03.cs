// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileWide310x150SmallImageAndText03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileWide310x150SmallImageAndText03 : 
    TileWide310x150Base,
    ITileWide310x150SmallImageAndText03,
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileWide310x150SmallImageAndText03()
      : base(nameof (TileWide310x150SmallImageAndText03), "TileWideSmallImageAndText03", 1, 1)
    {
    }

    public INotificationContentImage Image => this.Images[0];

    public INotificationContentText TextBodyWrap => this.TextFields[0];
  }
}
