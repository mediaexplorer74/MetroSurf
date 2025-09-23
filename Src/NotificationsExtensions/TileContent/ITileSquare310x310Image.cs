// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileSquare310x310Image
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1146339762, 23055, 20584, 82, 24, 138, 191, 164, 145, 33, 135)]
  [Version(16777216)]
  public interface ITileSquare310x310Image : 
    ISquare310x310TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }
  }
}
