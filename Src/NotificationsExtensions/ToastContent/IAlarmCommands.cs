#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IAlarmCommands
  {
    bool ShowSnoozeCommand { get; set; }
    string SnoozeArgument { get; set; }
    bool ShowDismissCommand { get; set; }
    string DismissArgument { get; set; }
  }
}