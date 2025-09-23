// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ISquare150x150TileNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(343877222, 33070, 23304, 80, 165, 176, 182, 204, 190, 31, 196)]
  [Version(16777216)]
  public interface ISquare150x150TileNotificationContent : 
    ITileNotificationContent,
    INotificationContent
  {
    ISquare71x71TileNotificationContent Square71x71Content { get; [param: In] set; }

    bool RequireSquare71x71Content { get; [param: In] set; }
  }
}
