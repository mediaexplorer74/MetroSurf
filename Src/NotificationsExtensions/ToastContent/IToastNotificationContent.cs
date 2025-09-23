// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(2595427304, 15498, 23945, 79, 116, 59, 130, 216, 139, 92, 0)]
  [Version(16777216)]
  public interface IToastNotificationContent : INotificationContent
  {
    bool StrictValidation { get; [param: In] set; }

    string Lang { get; [param: In] set; }

    string BaseUri { get; [param: In] set; }

    bool AddImageQuery { get; [param: In] set; }

    string Launch { get; [param: In] set; }

    IToastAudio Audio { get; }

    ToastDuration Duration { get; [param: In] set; }

    IIncomingCallCommands IncomingCallCommands { get; }

    IAlarmCommands AlarmCommands { get; }

    ToastNotification CreateNotification();
  }
}
