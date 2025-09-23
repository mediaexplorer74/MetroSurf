// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.TileContent.ITileNotificationContent
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

#nullable disable
namespace NotificationsExtensions.TileContent
{
  [Guid(1331327541, 27570, 21821, 64, 0, 74, 88, 86, 43, 112, 114)]
  [Version(16777216)]
  public interface ITileNotificationContent : INotificationContent
  {
    bool StrictValidation { get; [param: In] set; }

    string Lang { get; [param: In] set; }

    string BaseUri { get; [param: In] set; }

    TileBranding Branding { get; [param: In] set; }

    bool AddImageQuery { get; [param: In] set; }

    string ContentId { get; [param: In] set; }

    TileNotification CreateNotification();
  }
}
