#nullable disable
namespace NotificationsExtensions
{
  internal interface INotificationContentImage
  {
    string Src { get; set; }
    string Alt { get; set; }
    bool AddImageQuery { get; set; }
    bool? AddImageQueryNullable { get; set; }
  }
}