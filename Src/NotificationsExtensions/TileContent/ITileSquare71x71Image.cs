// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare71x71Image
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3905424282, 63975, 21034, 75, 62, 90, 228, 78, 105, 34, 73)]
  [Version(16777216)]
  public interface ITileSquare71x71Image : 
    ISquare71x71TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }
  }
}
