using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastImageAndText01
  {
    INotificationContentImage Image { get; }
    INotificationContentText TextBodyWrap { get; }
  }
}