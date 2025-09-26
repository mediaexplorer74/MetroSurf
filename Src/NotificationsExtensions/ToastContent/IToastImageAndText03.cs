using NotificationsExtensions;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastImageAndText03
  {
    INotificationContentImage Image { get; }
    INotificationContentText TextHeadingWrap { get; }
    INotificationContentText TextBody { get; }
  }
}