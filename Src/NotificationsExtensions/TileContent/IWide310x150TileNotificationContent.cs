// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.IWide310x150TileNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(3299572508, 47746, 23448, 99, 230, 225, 184, 84, 49, 171, 229)]
  [Version(16777216)]
  public interface IWide310x150TileNotificationContent : 
    ITileNotificationContent,
    INotificationContent
  {
    ISquare150x150TileNotificationContent Square150x150Content { get; [param: In] set; }

    bool RequireSquare150x150Content { get; [param: In] set; }
  }
}
