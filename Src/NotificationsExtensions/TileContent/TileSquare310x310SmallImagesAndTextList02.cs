// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310SmallImagesAndTextList02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310SmallImagesAndTextList02 : 
    TileSquare310x310Base,
    ITileSquare310x310SmallImagesAndTextList02,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310SmallImagesAndTextList02()
      : base(nameof (TileSquare310x310SmallImagesAndTextList02), (string) null, 3, 3)
    {
    }

    public INotificationContentImage Image1 => this.Images[0];

    public INotificationContentText TextWrap1 => this.TextFields[0];

    public INotificationContentImage Image2 => this.Images[1];

    public INotificationContentText TextWrap2 => this.TextFields[1];

    public INotificationContentImage Image3 => this.Images[2];

    public INotificationContentText TextWrap3 => this.TextFields[2];
  }
}
