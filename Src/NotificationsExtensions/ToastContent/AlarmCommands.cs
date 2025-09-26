// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.AlarmCommands
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal sealed class AlarmCommands : IAlarmCommands
  {
    private bool m_Snooze;
    private bool m_Dismiss;
    private string m_SnoozeArgument = string.Empty;
    private string m_DismissArgument = string.Empty;

    internal AlarmCommands()
    {
    }

    public bool ShowSnoozeCommand
    {
      get => this.m_Snooze;
      set => this.m_Snooze = value;
    }

    public string SnoozeArgument
    {
      get => this.m_SnoozeArgument;
      set => this.m_SnoozeArgument = value;
    }

    public bool ShowDismissCommand
    {
      get => this.m_Dismiss;
      set => this.m_Dismiss = value;
    }

    public string DismissArgument
    {
      get => this.m_DismissArgument;
      set => this.m_DismissArgument = value;
    }
  }

  // Added IncomingCallCommands implementation here so it's compiled without modifying the csproj
  internal sealed class IncomingCallCommands : IIncomingCallCommands
  {
    private bool m_Video;
    private string m_VideoArgument = string.Empty;
    private bool m_Voice;
    private string m_VoiceArgument = string.Empty;
    private bool m_Decline;
    private string m_DeclineArgument = string.Empty;

    internal IncomingCallCommands()
    {
    }

    public bool ShowVideoCommand
    {
      get => this.m_Video;
      set => this.m_Video = value;
    }

    public string VideoArgument
    {
      get => this.m_VideoArgument;
      set => this.m_VideoArgument = value ?? string.Empty;
    }

    public bool ShowVoiceCommand
    {
      get => this.m_Voice;
      set => this.m_Voice = value;
    }

    public string VoiceArgument
    {
      get => this.m_VoiceArgument;
      set => this.m_VoiceArgument = value ?? string.Empty;
    }

    public bool ShowDeclineCommand
    {
      get => this.m_Decline;
      set => this.m_Decline = value;
    }

    public string DeclineArgument
    {
      get => this.m_DeclineArgument;
      set => this.m_DeclineArgument = value ?? string.Empty;
    }
  }
}
