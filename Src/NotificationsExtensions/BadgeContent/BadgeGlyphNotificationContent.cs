using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.BadgeContent
{
    public sealed class BadgeGlyphNotificationContent : NotificationsExtensions.INotificationContent
    {
        public BadgeGlyphNotificationContent()
        {
            Glyph = GlyphValue.None;
        }

        public BadgeGlyphNotificationContent(GlyphValue glyph)
        {
            Glyph = glyph;
        }

        public GlyphValue Glyph { get; set; }

        // Returns the XML payload as string
        public string GetContent()
        {
            var xml = GetXml();
            return xml?.GetXml();
        }

        // Build the XmlDocument according to badge schema for glyphs
        public XmlDocument GetXml()
        {
            var doc = new XmlDocument();
            // The badge value for glyphs is the string name of the glyph in lower-case
            var value = GlyphToString(Glyph);
            // Create minimal badge XML
            var xml = $"<badge value=\"{value}\"/>";
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

        // INotificationContent members (simple defaults for MVP)
        public string TemplateName => "badgeGlyph";
        public string FallbackName => "badgeGlyph";

        private static string GlyphToString(GlyphValue gv)
        {
            // Map enum names to badge glyph strings used by the system
            switch (gv)
            {
                case GlyphValue.Activity: return "activity";
                case GlyphValue.Alert: return "alert";
                case GlyphValue.Available: return "available";
                case GlyphValue.Away: return "away";
                case GlyphValue.Busy: return "busy";
                case GlyphValue.NewMessage: return "newMessage";
                case GlyphValue.Paused: return "paused";
                case GlyphValue.Playing: return "playing";
                case GlyphValue.Unavailable: return "unavailable";
                case GlyphValue.Error: return "error";
                case GlyphValue.Attention: return "attention";
                case GlyphValue.Alarm: return "alarm";
                case GlyphValue.None:
                default:
                    return "none";
            }
        }
    }
}
