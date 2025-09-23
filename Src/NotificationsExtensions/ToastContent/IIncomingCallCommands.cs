// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IIncomingCallCommands
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(3816849074, 48423, 21450, 94, 31, 145, 40, 218, 188, 125, 216)]
  [Version(16777216)]
  public interface IIncomingCallCommands
  {
    bool ShowVideoCommand { get; [param: In] set; }

    string VideoArgument { get; [param: In] set; }

    bool ShowVoiceCommand { get; [param: In] set; }

    string VoiceArgument { get; [param: In] set; }

    bool ShowDeclineCommand { get; [param: In] set; }

    string DeclineArgument { get; [param: In] set; }
  }
}
