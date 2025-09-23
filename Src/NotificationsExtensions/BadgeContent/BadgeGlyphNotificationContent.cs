// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.BadgeContent.BadgeGlyphNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.BadgeContent
{
  [MarshalingBehavior]
  [Threading]
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  [Activatable(typeof (IBadgeGlyphNotificationContentFactory), 16777216)]
  public sealed class BadgeGlyphNotificationContent : 
    IBadgeNotificationContent,
    INotificationContent,
    IBadgeGlyphNotificationContentClass,
    IStringable
  {
    [MethodImpl]
    public extern BadgeGlyphNotificationContent();

    [MethodImpl]
    public extern BadgeGlyphNotificationContent([In] GlyphValue glyph);

    [MethodImpl]
    public extern string GetContent();

    [MethodImpl]
    public extern XmlDocument GetXml();

    [MethodImpl]
    public extern BadgeNotification CreateNotification();

    public extern GlyphValue Glyph { [MethodImpl] get; [MethodImpl] [param: In] set; }

    [MethodImpl]
    public override sealed extern string ToString();

    [MethodImpl]
    extern string IStringable.ToString();
  }
}
