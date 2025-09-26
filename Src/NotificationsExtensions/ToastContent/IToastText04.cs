using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastText04
  {
    INotificationContentText TextHeading { get; }
    INotificationContentText TextBody1 { get; }
    INotificationContentText TextBody2 { get; }
  }
}