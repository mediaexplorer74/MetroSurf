// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ISquare310x310TileNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(810686969, 19945, 24434, 78, 78, 135, 124, 169, 206, 8, 74)]
  [Version(16777216)]
  public interface ISquare310x310TileNotificationContent : 
    ITileNotificationContent,
    INotificationContent
  {
    IWide310x150TileNotificationContent Wide310x150Content { get; [param: In] set; }

    bool RequireWide310x150Content { get; [param: In] set; }
  }
}
