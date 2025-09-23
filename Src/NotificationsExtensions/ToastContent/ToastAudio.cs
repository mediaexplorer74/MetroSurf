// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.ToastContent.ToastAudio
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
  internal sealed class ToastAudio : IToastAudio
  {
    private ToastAudioContent m_Content;
    private bool m_Loop;

    internal ToastAudio()
    {
    }

    public ToastAudioContent Content
    {
      get => this.m_Content;
      set
      {
        this.m_Content = Enum.IsDefined(typeof(ToastAudioContent), value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
      }
    }

    public bool Loop
    {
      get => this.m_Loop;
      set => this.m_Loop = value;
    }
  }
}
