// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.BadgeContent.IBadgeNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.BadgeContent
{
  [Guid(3308802654, 15353, 21232, 118, 87, 215, 193, 145, 12, 62, 68)]
  [Version(16777216)]
  public interface IBadgeNotificationContent : INotificationContent
  {
    BadgeNotification CreateNotification();
  }
}
