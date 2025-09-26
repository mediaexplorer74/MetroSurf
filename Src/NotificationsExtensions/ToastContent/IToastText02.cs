using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastText02
  {
    INotificationContentText TextHeading { get; }
    INotificationContentText TextBodyWrap { get; }
  }
}