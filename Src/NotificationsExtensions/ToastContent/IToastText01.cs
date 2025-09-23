// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastText01
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(2736594515, 41449, 21061, 127, 73, 134, 2, 0, 161, 234, 108)]
  [Version(16777216)]
  public interface IToastText01 : IToastNotificationContent, INotificationContent
  {
    INotificationContentText TextBodyWrap { get; }
  }
}
