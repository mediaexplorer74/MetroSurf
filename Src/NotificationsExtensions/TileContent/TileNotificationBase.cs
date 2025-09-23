// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.TileNotificationBase
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  internal abstract class TileNotificationBase : NotificationBase
  {
    private TileBranding m_Branding = TileBranding.Logo;
    private string m_ContentId;

    public TileNotificationBase(
      string templateName,
      string fallbackName,
      int imageCount,
      int textCount)
      : base(templateName, fallbackName, imageCount, textCount)
    {
    }

    public TileBranding Branding
    {
      get => this.m_Branding;
      set
      {
        this.m_Branding = Enum.IsDefined(typeof(TileBranding), value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
      }
    }

    public string ContentId
    {
      get => this.m_ContentId;
      set => this.m_ContentId = value;
    }

    public TileNotification CreateNotification()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(this.GetContent());
      return new TileNotification(xmlDocument);
    }
  }
}
