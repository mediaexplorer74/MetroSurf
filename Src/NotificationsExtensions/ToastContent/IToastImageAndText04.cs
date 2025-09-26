using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastImageAndText04
  {
    INotificationContentImage Image { get; }
    INotificationContentText TextHeading { get; }
    INotificationContentText TextBody1 { get; }
    INotificationContentText TextBody2 { get; }
  }
}