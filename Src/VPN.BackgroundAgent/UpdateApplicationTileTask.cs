// Decompiled with JetBrains decompiler
// Type: VPN.BackgroundAgent.UpdateApplicationTileTask
// Assembly: VPN.BackgroundAgent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: FF96E3FF-A49E-4455-8F7E-50F192B995C9
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.BackgroundAgent.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Metadata;

#nullable disable
namespace VPN.BackgroundAgent
{
  [MarshalingBehavior]
  [Threading]
  [Version(16777216)]
  [CompilerGenerated]
  [Activatable(16777216)]
  [Static(typeof (IUpdateApplicationTileTaskStatic), 16777216)]
  public sealed class UpdateApplicationTileTask : IBackgroundTask, IStringable
  {
    [MethodImpl]
    public extern UpdateApplicationTileTask();

    [MethodImpl]
    public static extern string Decrypt(
      [In] string dataToDecrypt,
      [In] string openPartOfPassword,
      [In] string salt);

    [MethodImpl]
    public static extern void SetUpPushNotification([In] bool isVPNNotConnected);

    [MethodImpl]
    extern void IBackgroundTask.Run([In] IBackgroundTaskInstance taskInstance);

    [MethodImpl]
    extern string IStringable.ToString();
  }
}
