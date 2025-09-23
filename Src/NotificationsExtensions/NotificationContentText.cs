// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.NotificationContentText
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions
{
  internal sealed class NotificationContentText : INotificationContentText
  {
    private string m_Text;
    private string m_Lang;

    internal NotificationContentText()
    {
    }

    public string Text
    {
      get => this.m_Text;
      set => this.m_Text = value;
    }

    public string Lang
    {
      get => this.m_Lang;
      set => this.m_Lang = value;
    }
  }
}
