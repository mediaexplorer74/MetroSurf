// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastText03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastText03 : 
    ToastNotificationBase,
    IToastText03,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastText03()
      : base(nameof (ToastText03), 0, 2)
    {
    }

    public INotificationContentText TextHeadingWrap => this.TextFields[0];

    public INotificationContentText TextBody => this.TextFields[1];
  }
}
