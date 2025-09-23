// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IAlarmCommands
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(557698227, 48267, 20773, 97, 192, 109, 13, 136, 7, 134, 206)]
  [Version(16777216)]
  public interface IAlarmCommands
  {
    bool ShowSnoozeCommand { get; [param: In] set; }

    string SnoozeArgument { get; [param: In] set; }

    bool ShowDismissCommand { get; [param: In] set; }

    string DismissArgument { get; [param: In] set; }
  }
}
