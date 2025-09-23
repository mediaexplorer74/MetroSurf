// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.PurchaseManager
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using Windows.ApplicationModel.Store;

#nullable disable
namespace VPN.ViewModel
{
  public class PurchaseManager
  {
    public static readonly PurchaseManager Current = new PurchaseManager();
    public LicenseInformation LicenseInformation;
  }
}
