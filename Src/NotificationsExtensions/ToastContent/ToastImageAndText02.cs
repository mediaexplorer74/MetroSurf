// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastImageAndText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastImageAndText02 : 
    ToastNotificationBase,
    IToastImageAndText02,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastImageAndText02()
      : base(nameof (ToastImageAndText02), 1, 2)
    {
    }

    public INotificationContentImage Image => this.Images[0];

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentText TextBodyWrap => this.TextFields[1];
  }
}
