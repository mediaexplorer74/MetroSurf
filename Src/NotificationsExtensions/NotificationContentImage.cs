// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.NotificationContentImage
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

#nullable disable
namespace NotificationsExtensions
{
  internal sealed class NotificationContentImage : INotificationContentImage
  {
    private string m_Src;
    private string m_Alt;
    private bool? m_AddImageQueryNullable;

    internal NotificationContentImage()
    {
    }

    public string Src
    {
      get => this.m_Src;
      set => this.m_Src = value;
    }

    public string Alt
    {
      get => this.m_Alt;
      set => this.m_Alt = value;
    }

    public bool AddImageQuery
    {
      get => this.m_AddImageQueryNullable.HasValue && this.m_AddImageQueryNullable.Value;
      set => this.m_AddImageQueryNullable = new bool?(value);
    }

    public bool? AddImageQueryNullable
    {
      get => this.m_AddImageQueryNullable;
      set => this.m_AddImageQueryNullable = value;
    }
  }
}
