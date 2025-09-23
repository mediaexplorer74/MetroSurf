// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.IToastAudio
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  [Guid(643859137, 55823, 24560, 72, 113, 124, 53, 226, 14, 120, 135)]
  [Version(16777216)]
  public interface IToastAudio
  {
    ToastAudioContent Content { get; [param: In] set; }

    bool Loop { get; [param: In] set; }
  }
}
