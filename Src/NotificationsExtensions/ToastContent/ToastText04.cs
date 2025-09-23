// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastText04
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastText04 : 
    ToastNotificationBase,
    IToastText04,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastText04()
      : base(nameof (ToastText04), 0, 3)
    {
    }

    public INotificationContentText TextHeading => this.TextFields[0];

    public INotificationContentText TextBody1 => this.TextFields[1];

    public INotificationContentText TextBody2 => this.TextFields[2];
  }
}
