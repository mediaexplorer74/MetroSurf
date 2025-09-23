// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Adapters.ServerToServerViewModelAdapter
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using VPN.Model;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel.Adapters
{
  public class ServerToServerViewModelAdapter : BaseAdapter<Server, ServerViewModel>
  {
    private static string PathToFlagsIcons = "https://39e5a1dabc4707021dd5-935414efe171f56dbde59246d9507091.ssl.cf1.rackcdn.com/winphone/{0}.png";
    private static string PathToOptimalIcon = "/Assets/Icons/optimal.jpg";

    public override void Init(Server input, ServerViewModel output)
    {
      if (input.CountryCode != null)
        output.FlagIcon = string.Format(ServerToServerViewModelAdapter.PathToFlagsIcons, (object) input.CountryCode);
      else
        output.FlagIcon = ServerToServerViewModelAdapter.PathToOptimalIcon;
      output.Country = input.Name;
      output.City = input.Description;
      output.Region = input.Region;
    }
  }
}
