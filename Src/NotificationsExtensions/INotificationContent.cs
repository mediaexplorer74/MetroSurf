using Windows.Data.Xml.Dom;

#nullable disable
namespace NotificationsExtensions
{
  public interface INotificationContent
  {
    string GetContent();
    Windows.Data.Xml.Dom.XmlDocument GetXml();
    string TemplateName { get; }
    string FallbackName { get; }
  }
}