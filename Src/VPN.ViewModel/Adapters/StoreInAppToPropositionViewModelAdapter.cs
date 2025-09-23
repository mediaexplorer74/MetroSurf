// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Adapters.StoreInAppToPropositionViewModelAdapter
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using VPN.Localization;
using VPN.ViewModel.Items;
using Windows.ApplicationModel.Store;

#nullable disable
namespace VPN.ViewModel.Adapters
{
  public class StoreInAppToPropositionViewModelAdapter : 
    BaseAdapter<ProductListing, PropositionViewModel>
  {
    public override void Init(ProductListing input, PropositionViewModel output)
    {
      output.Price = input.FormattedPrice.IndexOf('.') + 3 < input.FormattedPrice.Length ? input.FormattedPrice.Remove(input.FormattedPrice.IndexOf('.') + 3) : input.FormattedPrice;
      output.ID = input.ProductId;
      switch (input.ProductId)
      {
        case "com.KeepSolid.VPN.Infinity.WinPhone":
          output.Period = LocalizedResources.GetLocalizedString("S_INIFINITE_PLAN");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_INFINITY");
          output.Discount = 99;
          output.DaysCount = 10092;
          break;
        case "com.KeepSolid.VPN.3years.WinPhone":
          output.Period = LocalizedResources.GetLocalizedString("S_PURCHASE_SUBTITLE_3_YEAR");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_3_YEAR");
          output.Discount = 70;
          output.DaysCount = 1092;
          break;
        case "com.KeepSolid.VPN.1year.WinPhone.Consumable":
          output.Period = LocalizedResources.GetLocalizedString("S_PURCHASE_SUBTITLE_1_YEAR");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_1_YEAR");
          output.Discount = 67;
          output.DaysCount = 364;
          break;
        case "com.KeepSolid.VPN.3months.WinPhone.Consumable":
          output.Period = LocalizedResources.GetLocalizedString("S_PURCHASE_SUBTITLE_3_MONTH");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_3_MONTH");
          output.Discount = 56;
          output.DaysCount = 90;
          break;
        case "com.KeepSolid.VPN.1month.WinPhone.Consumable":
          output.Period = LocalizedResources.GetLocalizedString("S_PURCHASE_SUBTITLE_1_MONTH");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_1_MONTH");
          output.Discount = 33;
          output.DaysCount = 30;
          break;
        case "com.KeepSolid.VPN.10days.WinPhone":
          output.Period = LocalizedResources.GetLocalizedString("S_PURCHASE_SUBTITLE_10_DAYS");
          output.Name = LocalizedResources.GetLocalizedString("WinPhone_PURCHASE_TITLE_10_DAYS");
          output.Discount = 0;
          output.DaysCount = 10;
          break;
      }
    }
  }
}
