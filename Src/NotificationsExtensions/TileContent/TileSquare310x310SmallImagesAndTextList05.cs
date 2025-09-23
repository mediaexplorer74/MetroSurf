// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileSquare310x310SmallImagesAndTextList05
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal class TileSquare310x310SmallImagesAndTextList05 : 
    TileSquare310x310Base,
    ITileSquare310x310SmallImagesAndTextList05,
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    public TileSquare310x310SmallImagesAndTextList05()
      : base(nameof (TileSquare310x310SmallImagesAndTextList05), (string) null, 3, 7)
    {
    }

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentImage Image1 => this.Images[0];

    public INotificationContentText TextGroup1Field1 => this.TextFields[1];

    public INotificationContentText TextGroup1Field2 => this.TextFields[2];

    public INotificationContentImage Image2 => this.Images[1];

    public INotificationContentText TextGroup2Field1 => this.TextFields[3];

    public INotificationContentText TextGroup2Field2 => this.TextFields[4];

    public INotificationContentImage Image3 => this.Images[2];

    public INotificationContentText TextGroup3Field1 => this.TextFields[5];

    public INotificationContentText TextGroup3Field2 => this.TextFields[6];
  }
}
