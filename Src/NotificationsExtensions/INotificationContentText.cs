// Decompiled with JetBrains decompiler
// Type: NotificationsExtensions.INotificationContentText
// Assembly: NotificationsExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: 45D5A015-032A-4E9A-A1F7-E4E00D40AE62
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\NotificationsExtensions.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

#nullable disable
namespace NotificationsExtensions
{
  [Guid(2064201256, 29995, 23866, 66, 192, 171, 231, 45, 66, 211, 35)]
  [Version(16777216)]
  public interface INotificationContentText
  {
    string Text { get; [param: In] set; }

    string Lang { get; [param: In] set; }
  }
}
