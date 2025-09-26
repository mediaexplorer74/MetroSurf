#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal interface IToastAudio
  {
    ToastAudioContent Content { get; set; }
    bool Loop { get; set; }
  }
}