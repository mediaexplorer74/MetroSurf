// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastText02
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(4144737054, 9799, 22789, 108, 24, 162, 28, 215, 156, 235, 129)]
  [Version(16777216)]
  public interface IToastText02 : IToastNotificationContent, INotificationContent
  {
    INotificationContentText TextHeading { get; }

    INotificationContentText TextBodyWrap { get; }
  }
}
