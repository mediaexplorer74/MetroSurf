// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastImageAndText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastImageAndText01 : 
    ToastNotificationBase,
    IToastImageAndText01,
    IToastNotificationContent,
    INotificationContent
  {
    public ToastImageAndText01()
      : base(nameof (ToastImageAndText01), 1, 1)
    {
    }

    public INotificationContentImage Image => this.Images[0];

    public INotificationContentText TextBodyWrap => this.TextFields[0];
  }
}
