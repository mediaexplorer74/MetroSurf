using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastImageAndText02
  {
    INotificationContentImage Image { get; }
    INotificationContentText TextHeading { get; }
    INotificationContentText TextBodyWrap { get; }
  }
}