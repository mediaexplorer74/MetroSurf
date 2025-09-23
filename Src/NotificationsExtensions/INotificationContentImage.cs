// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.INotificationContentImage
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions
{
  [Guid(48508130, 11873, 21395, 110, 73, 249, 203, 54, 124, 52, 213)]
  [Version(16777216)]
  public interface INotificationContentImage
  {
    string Src { get; [param: In] set; }

    string Alt { get; [param: In] set; }

    bool AddImageQuery { get; [param: In] set; }
  }
}
