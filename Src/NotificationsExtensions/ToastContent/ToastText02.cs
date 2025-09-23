// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastText02 : 
    ToastNotificationBase,
    IToastText02,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastText02()
      : base(nameof (ToastText02), 0, 2)
    {
    }

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentText TextBodyWrap => this.TextFields[1];
  }
}
