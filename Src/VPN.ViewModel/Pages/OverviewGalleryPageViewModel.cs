// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.OverviewGalleryPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VPN.Localization;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class OverviewGalleryPageViewModel : MetroLab.Common.PageViewModel
  {
    private List<OverviewPageViewModel> _items;

    [DataMember]
    public List<OverviewPageViewModel> Items
    {
      get => this._items;
      set => this.SetProperty<List<OverviewPageViewModel>>(ref this._items, value, nameof (Items));
    }

    [DataMember]
    public bool IsLoadedFromSettings { get; set; }

    protected override async Task<bool> LoadAsync()
    {
      this.Items = new List<OverviewPageViewModel>()
      {
        new OverviewPageViewModel()
        {
          ImagePath = "/Assets/Overview/img-quick-tour-1.png",
          Title = LocalizedResources.GetLocalizedString("S_QTOUR_FIRST_TITLE"),
          Text1 = LocalizedResources.GetLocalizedString("S_QTOUR_FIRST_HEADER"),
          Text2 = LocalizedResources.GetLocalizedString("S_QTOUR_FIRST_FOOTER")
        },
        new OverviewPageViewModel()
        {
          ImagePath = "/Assets/Overview/img-quick-tour-2.png",
          Title = LocalizedResources.GetLocalizedString("S_QTOUR_SECOND_TITLE"),
          Text1 = LocalizedResources.GetLocalizedString("S_QTOUR_SECOND_HEADER"),
          Text2 = LocalizedResources.GetLocalizedString("S_QTOUR_SECOND_FOOTER_FIRST"),
          Text3 = LocalizedResources.GetLocalizedString("S_QTOUR_SECOND_FOOTER_SECOND"),
          Text4 = LocalizedResources.GetLocalizedString("S_QTOUR_SECOND_FOOTER_THIRD")
        },
        new OverviewPageViewModel()
        {
          ImagePath = "/Assets/Overview/img-quick-tour-3.png",
          Title = LocalizedResources.GetLocalizedString("S_QTOUR_THIRD_TITLE"),
          Text1 = LocalizedResources.GetLocalizedString("WinPhoneQuickTourThirdHeader"),
          Text2 = LocalizedResources.GetLocalizedString("WinPhoneQuickTourThirdFooterFirst"),
          IsLastPage = true
        }
      };
      return await base.LoadAsync();
    }

    public string ButtonNextText => LocalizedResources.GetLocalizedString("S_NEXT_BTN");
  }
}
