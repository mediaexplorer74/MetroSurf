// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastText03
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(133853472, 53651, 24500, 86, 208, 190, 242, 23, 249, 70, 255)]
  [Version(16777216)]
  public interface IToastText03 : IToastNotificationContent, INotificationContent
  {
    INotificationContentText TextHeadingWrap { get; }

    INotificationContentText TextBody { get; }
  }
}
