// Decompiled with JetBrains decompiler
// Type: VPN.Common.ContinuationManager
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using VPN.View;
using Windows.ApplicationModel.Activation;

#nullable disable
namespace VPN.Common
{
  public class ContinuationManager
  {
    internal void Continue(IContinuationActivatedEventArgs args)
    {
      var activated = args as IActivatedEventArgs;
      if (activated == null)
        return;
      if (activated.Kind != ActivationKind.WebAuthenticationBrokerContinuation)
        return;
      LoginPage.Current?.ContinueWebAuthentication(args as WebAuthenticationBrokerContinuationEventArgs);
    }
  }
}
