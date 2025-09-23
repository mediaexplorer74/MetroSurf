// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastNotificationBase
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal class ToastNotificationBase : NotificationBase, IToastNotificationContent, INotificationContent
  {
    private string m_Launch;
    private IToastAudio m_Audio = (IToastAudio) new ToastAudio();
    private IIncomingCallCommands m_IncomingCallCommands = (IIncomingCallCommands) new global::NotificationsExtensions.ToastContent.IncomingCallCommands();
    private IAlarmCommands m_AlarmCommands = (IAlarmCommands) new global::NotificationsExtensions.ToastContent.AlarmCommands();
    private ToastDuration m_Duration;

    // Constructor added to replace decompiled header
    public ToastNotificationBase(string templateName, int imageCount, int textCount)
      : base(templateName, null, imageCount, textCount)
    {
      this.m_Launch = null;
      this.m_Audio = new ToastAudio();
      this.m_IncomingCallCommands = new IncomingCallCommands();
      this.m_AlarmCommands = new AlarmCommands();
      this.m_Duration = ToastDuration.Short;
    }

    private bool AudioSrcIsLooping()
    {
      return this.Audio.Content == ToastAudioContent.LoopingAlarm || this.Audio.Content == ToastAudioContent.LoopingAlarm2 || this.Audio.Content == ToastAudioContent.LoopingAlarm3 || this.Audio.Content == ToastAudioContent.LoopingAlarm4 || this.Audio.Content == ToastAudioContent.LoopingAlarm5 || this.Audio.Content == ToastAudioContent.LoopingAlarm6 || this.Audio.Content == ToastAudioContent.LoopingAlarm7 || this.Audio.Content == ToastAudioContent.LoopingAlarm8 || this.Audio.Content == ToastAudioContent.LoopingAlarm9 || this.Audio.Content == ToastAudioContent.LoopingAlarm10 || this.Audio.Content == ToastAudioContent.LoopingCall || this.Audio.Content == ToastAudioContent.LoopingCall2 || this.Audio.Content == ToastAudioContent.LoopingCall3 || this.Audio.Content == ToastAudioContent.LoopingCall4 || this.Audio.Content == ToastAudioContent.LoopingCall5 || this.Audio.Content == ToastAudioContent.LoopingCall6 || this.Audio.Content == ToastAudioContent.LoopingCall7 || this.Audio.Content == ToastAudioContent.LoopingCall8 || this.Audio.Content == ToastAudioContent.LoopingCall9 || this.Audio.Content == ToastAudioContent.LoopingCall10;
    }

    private void ValidateAudio()
    {
      if (!this.StrictValidation)
        return;
      if (this.Audio.Loop && this.Duration != ToastDuration.Long)
        throw new NotificationContentValidationException("Looping audio is only available for long duration toasts.");
      if (this.Audio.Loop && !this.AudioSrcIsLooping())
        throw new NotificationContentValidationException("A looping audio src must be chosen if the looping audio property is set.");
      if (!this.Audio.Loop && this.AudioSrcIsLooping())
        throw new NotificationContentValidationException("The looping audio property needs to be set if a looping audio src is chosen.");
    }

    private void AppendAudioTag(StringBuilder builder)
    {
      if (this.Audio.Content == ToastAudioContent.Default)
        return;
      builder.Append("<audio");
      if (this.Audio.Content == ToastAudioContent.Silent)
      {
        builder.Append(" silent='true'/>");
      }
      else
      {
        if (this.Audio.Loop)
          builder.Append(" loop='true'");
        if (this.Audio.Content != ToastAudioContent.LoopingCall)
        {
          string str = (string) null;
          switch (this.Audio.Content)
          {
            case ToastAudioContent.Mail:
              str = "ms-winsoundevent:Notification.Mail";
              break;
            case ToastAudioContent.SMS:
              str = "ms-winsoundevent:Notification.SMS";
              break;
            case ToastAudioContent.IM:
              str = "ms-winsoundevent:Notification.IM";
              break;
            case ToastAudioContent.Reminder:
              str = "ms-winsoundevent:Notification.Reminder";
              break;
            case ToastAudioContent.LoopingCall:
              str = "ms-winsoundevent:Notification.Looping.Call";
              break;
            case ToastAudioContent.LoopingCall2:
              str = "ms-winsoundevent:Notification.Looping.Call2";
              break;
            case ToastAudioContent.LoopingCall3:
              str = "ms-winsoundevent:Notification.Looping.Call3";
              break;
            case ToastAudioContent.LoopingCall4:
              str = "ms-winsoundevent:Notification.Looping.Call4";
              break;
            case ToastAudioContent.LoopingCall5:
              str = "ms-winsoundevent:Notification.Looping.Call5";
              break;
            case ToastAudioContent.LoopingCall6:
              str = "ms-winsoundevent:Notification.Looping.Call6";
              break;
            case ToastAudioContent.LoopingCall7:
              str = "ms-winsoundevent:Notification.Looping.Call7";
              break;
            case ToastAudioContent.LoopingCall8:
              str = "ms-winsoundevent:Notification.Looping.Call8";
              break;
            case ToastAudioContent.LoopingCall9:
              str = "ms-winsoundevent:Notification.Looping.Call9";
              break;
            case ToastAudioContent.LoopingCall10:
              str = "ms-winsoundevent:Notification.Looping.Call10";
              break;
            case ToastAudioContent.LoopingAlarm:
              str = "ms-winsoundevent:Notification.Looping.Alarm";
              break;
            case ToastAudioContent.LoopingAlarm2:
              str = "ms-winsoundevent:Notification.Looping.Alarm2";
              break;
            case ToastAudioContent.LoopingAlarm3:
              str = "ms-winsoundevent:Notification.Looping.Alarm3";
              break;
            case ToastAudioContent.LoopingAlarm4:
              str = "ms-winsoundevent:Notification.Looping.Alarm4";
              break;
            case ToastAudioContent.LoopingAlarm5:
              str = "ms-winsoundevent:Notification.Looping.Alarm5";
              break;
            case ToastAudioContent.LoopingAlarm6:
              str = "ms-winsoundevent:Notification.Looping.Alarm6";
              break;
            case ToastAudioContent.LoopingAlarm7:
              str = "ms-winsoundevent:Notification.Looping.Alarm7";
              break;
            case ToastAudioContent.LoopingAlarm8:
              str = "ms-winsoundevent:Notification.Looping.Alarm8";
              break;
            case ToastAudioContent.LoopingAlarm9:
              str = "ms-winsoundevent:Notification.Looping.Alarm9";
              break;
            case ToastAudioContent.LoopingAlarm10:
              str = "ms-winsoundevent:Notification.Looping.Alarm10";
              break;
          }
          builder.AppendFormat(" src='{0}'", (object) str);
        }
      }
      builder.Append("/>");
    }

    private bool IsIncomingCallToast()
    {
      return this.IncomingCallCommands.ShowVideoCommand || this.IncomingCallCommands.ShowVoiceCommand || this.IncomingCallCommands.ShowDeclineCommand;
    }

    private bool IsAlarmToast()
    {
      return this.AlarmCommands.ShowSnoozeCommand || this.AlarmCommands.ShowDismissCommand;
    }

    private void ValidateCommands()
    {
      if (!this.StrictValidation)
        return;
      if (this.IsIncomingCallToast() && this.IsAlarmToast())
        throw new NotificationContentValidationException("IncomingCall and Alarm properties cannot be included on a single toast.");
      if (!this.IncomingCallCommands.ShowVideoCommand && !string.IsNullOrEmpty(this.IncomingCallCommands.VideoArgument))
        throw new NotificationContentValidationException("Video argument should not be set if the Video button is not shown.");
      if (!this.IncomingCallCommands.ShowVoiceCommand && !string.IsNullOrEmpty(this.IncomingCallCommands.VoiceArgument))
        throw new NotificationContentValidationException("Voice argument should not be set if the Voice button is not shown.");
      if (!this.IncomingCallCommands.ShowDeclineCommand && !string.IsNullOrEmpty(this.IncomingCallCommands.DeclineArgument))
        throw new NotificationContentValidationException("Decline argument should not be set if the Video button is not shown.");
      if (!this.AlarmCommands.ShowSnoozeCommand && !string.IsNullOrEmpty(this.AlarmCommands.SnoozeArgument))
        throw new NotificationContentValidationException("Snooze argument should not be set if the Snooze button is not shown.");
      if (!this.AlarmCommands.ShowDismissCommand && !string.IsNullOrEmpty(this.AlarmCommands.DismissArgument))
        throw new NotificationContentValidationException("Dismiss argument should not be set if the Dismiss button is not shown.");
    }

    private void AppendIncomingCallCommandsTag(StringBuilder builder)
    {
      builder.Append("<commands scenario='incomingcall'>");
      string format1 = "<command id='{0}'";
      string format2 = "arguments='{0}'";
      if (this.IncomingCallCommands.ShowVideoCommand)
      {
        builder.AppendFormat(format1, (object) "video");
        if (!string.IsNullOrEmpty(this.IncomingCallCommands.VideoArgument))
          builder.AppendFormat(format2, (object) Util.HttpEncode(this.IncomingCallCommands.VideoArgument));
        builder.Append("/>");
      }
      if (this.IncomingCallCommands.ShowVoiceCommand)
      {
        builder.AppendFormat(format1, (object) "voice");
        if (!string.IsNullOrEmpty(this.IncomingCallCommands.VoiceArgument))
          builder.AppendFormat(format2, (object) Util.HttpEncode(this.IncomingCallCommands.VoiceArgument));
        builder.Append("/>");
      }
      if (this.IncomingCallCommands.ShowDeclineCommand)
      {
        builder.AppendFormat(format1, (object) "decline");
        if (!string.IsNullOrEmpty(this.IncomingCallCommands.DeclineArgument))
          builder.AppendFormat(format2, (object) Util.HttpEncode(this.IncomingCallCommands.DeclineArgument));
        builder.Append("/>");
      }
      builder.Append("</commands>");
    }

    private void AppendAlarmCommandsTag(StringBuilder builder)
    {
      builder.Append("<commands scenario='alarm'>");
      string format1 = "<command id='{0}'";
      string format2 = "arguments='{0}'";
      if (this.AlarmCommands.ShowSnoozeCommand)
      {
        builder.AppendFormat(format1, (object) "snooze");
        if (!string.IsNullOrEmpty(this.AlarmCommands.SnoozeArgument))
          builder.AppendFormat(format2, (object) Util.HttpEncode(this.AlarmCommands.SnoozeArgument));
        builder.Append("/>");
      }
      if (this.AlarmCommands.ShowDismissCommand)
      {
        builder.AppendFormat(format1, (object) "dismiss");
        if (!string.IsNullOrEmpty(this.AlarmCommands.DismissArgument))
          builder.AppendFormat(format2, (object) Util.HttpEncode(this.AlarmCommands.DismissArgument));
        builder.Append("/>");
      }
      builder.Append("</commands>");
    }

    public override string GetContent()
    {
      this.ValidateAudio();
      this.ValidateCommands();
      StringBuilder builder = new StringBuilder("<toast");
      if (!string.IsNullOrEmpty(this.Launch))
        builder.AppendFormat(" launch='{0}'", (object) Util.HttpEncode(this.Launch));
      if (this.Duration != ToastDuration.Short)
        builder.AppendFormat(" duration='{0}'", (object) this.Duration.ToString().ToLowerInvariant());
      builder.Append(">");
      builder.AppendFormat("<visual version='{0}'", (object) 1);
      if (!string.IsNullOrWhiteSpace(this.Lang))
        builder.AppendFormat(" lang='{0}'", (object) Util.HttpEncode(this.Lang));
      if (!string.IsNullOrWhiteSpace(this.BaseUri))
        builder.AppendFormat(" baseUri='{0}'", (object) Util.HttpEncode(this.BaseUri));
      if (this.AddImageQuery)
        builder.AppendFormat(" addImageQuery='true'");
      builder.Append(">");
      builder.AppendFormat("<binding template='{0}'>{1}</binding>", (object) this.TemplateName, (object) this.SerializeProperties(this.Lang, this.BaseUri, this.AddImageQuery));
      builder.Append("</visual>");
      this.AppendAudioTag(builder);
      if (this.IsIncomingCallToast())
        this.AppendIncomingCallCommandsTag(builder);
      if (this.IsAlarmToast())
        this.AppendAlarmCommandsTag(builder);
      builder.Append("</toast>");
      return builder.ToString();
    }

    public ToastNotification CreateNotification()
    {
      if (this.StrictValidation && this.IsAlarmToast())
        throw new NotificationContentValidationException("The ToastNotification object should not be used for alarms.Use the results of GetContent() to construct a ScheduledToastNotification object.");
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(this.GetContent());
      return new ToastNotification(xmlDocument);
    }

    public IToastAudio Audio => this.m_Audio;

    public IIncomingCallCommands IncomingCallCommands => this.m_IncomingCallCommands;

    public IAlarmCommands AlarmCommands => this.m_AlarmCommands;

    public string Launch
    {
      get => this.m_Launch;
      set => this.m_Launch = value;
    }

    public ToastDuration Duration
    {
      get => this.m_Duration;
      set
      {
        this.m_Duration = Enum.IsDefined(typeof (ToastDuration), (object) value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }
    }
  }
}
