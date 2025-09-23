// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileWide310x150Image
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(2462071523, 30022, 22616, 84, 44, 196, 124, 76, 198, 166, 208)]
  [Version(16777216)]
  public interface ITileWide310x150Image : 
    IWide310x150TileNotificationContent,
    ITileNotificationContent,
    INotificationContent
  {
    INotificationContentImage Image { get; }
  }
}
