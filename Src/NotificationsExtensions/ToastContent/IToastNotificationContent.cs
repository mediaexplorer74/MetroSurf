using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastNotificationContent
  {
    IToastAudio Audio { get; }
    IIncomingCallCommands IncomingCallCommands { get; }
    IAlarmCommands AlarmCommands { get; }
    string Launch { get; set; }
    ToastDuration Duration { get; set; }
    ToastNotification CreateNotification();
  }
}