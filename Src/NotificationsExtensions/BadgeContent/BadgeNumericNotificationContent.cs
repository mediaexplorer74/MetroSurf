// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.BadgeContent.BadgeNumericNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.BadgeContent
{
    public sealed class BadgeNumericNotificationContent : NotificationsExtensions.INotificationContent
    {
        public BadgeNumericNotificationContent()
        {
            Number = 0;
        }

        public BadgeNumericNotificationContent(uint number)
        {
            Number = number;
        }

        public uint Number { get; set; }

        public string GetContent()
        {
            var xml = GetXml();
            return xml?.GetXml();
        }

        public XmlDocument GetXml()
        {
            var doc = new XmlDocument();
            var xml = $"<badge value=\"{Number}\"/>";
            doc.LoadXml(xml);
            return doc;
        }

        public BadgeNotification CreateNotification()
        {
            var xml = GetXml();
            return new BadgeNotification(xml);
        }

        public override string ToString()
        {
            return GetContent();
        }

        // Simple defaults for MVP
        public string TemplateName => "badgeNumeric";
        public string FallbackName => "badgeNumeric";
    }
}
