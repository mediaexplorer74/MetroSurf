// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.INotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Data.Xml.Dom;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions
{
  [Guid(739054458, 8136, 23990, 76, 154, 88, 2, 33, 238, 241, 244)]
  [Version(16777216)]
  public interface INotificationContent
  {
    string GetContent();

    XmlDocument GetXml();
  }
}
