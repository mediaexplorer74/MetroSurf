#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IIncomingCallCommands
  {
    bool ShowVideoCommand { get; set; }
    string VideoArgument { get; set; }
    bool ShowVoiceCommand { get; set; }
    string VoiceArgument { get; set; }
    bool ShowDeclineCommand { get; set; }
    string DeclineArgument { get; set; }
  }
}