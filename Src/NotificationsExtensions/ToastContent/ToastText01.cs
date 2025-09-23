// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastText01 : 
    ToastNotificationBase,
    IToastText01,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastText01()
      : base(nameof (ToastText01), 0, 1)
    {
    }

    public INotificationContentText TextBodyWrap => this.TextFields[0];
  }
}
