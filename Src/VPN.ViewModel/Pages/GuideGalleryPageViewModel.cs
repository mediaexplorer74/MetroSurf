// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.GuideGalleryPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VPN.Localization;
using VPN.Model;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel.Pages
{
  [DataContract]
  public class GuideGalleryPageViewModel : PageViewModel
  {
    [DataMember]
    public Config Config;
    private bool _isWin10;
    private List<GuidePageViewModel> _items;
    private int _selectedIndex;

    public GuideGalleryPageViewModel(Config config)
    {
      this.Config = config;
      this.IsWin10 = Constants.IsWin10();
    }

    [DataMember]
    public bool IsWin10
    {
      get => this._isWin10;
      set => this.SetProperty<bool>(ref this._isWin10, value, nameof (IsWin10));
    }

    [DataMember]
    public List<GuidePageViewModel> Items
    {
      get => this._items;
      set => this.SetProperty<List<GuidePageViewModel>>(ref this._items, value, nameof (Items));
    }

    [DataMember]
    public int SelectedIndex
    {
      get => this._selectedIndex;
      set => this.SetProperty<int>(ref this._selectedIndex, value, nameof (SelectedIndex));
    }

    public string ButtonNextText
    {
      get
      {
        string localizedString = LocalizedResources.GetLocalizedString("S_NEXT_BTN");
        return char.ToUpper(localizedString[0]).ToString() + localizedString.Substring(1) + " >";
      }
    }

    public string FinishButtonText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneFinishTutorialButton");
    }

    protected override async Task<bool> LoadAsync()
    {
      bool flag = true;
      try
      {
        if (Type.GetType("Windows.Phone.Notification.Management.AccessoryManager, Windows, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime", false) == null)
          flag = false;
      }
      catch (Exception ex)
      {
      }
      List<GuidePageViewModel> guidePageViewModelList = new List<GuidePageViewModel>();
      string str1 = this.IsWin10 ? "S_WINPHONE_VPN_TOUR_" : "WinPhoneTutorialPage";
      string format = this.IsWin10 ? "/Assets/VPNGuide10/00{0}.png" : "/Assets/VPNGuide/00{0}.png";
      string str2 = "1";
      guidePageViewModelList.Add(new GuidePageViewModel()
      {
        StepNumber = str2,
        StepDescription = LocalizedResources.GetLocalizedString(str1 + str2),
        ScreenPath = string.Format(format, (object) str2)
      });
      if (!this.IsWin10 && !flag)
      {
        string str3 = "2";
        guidePageViewModelList.Add(new GuidePageViewModel()
        {
          StepNumber = str3,
          StepDescription = LocalizedResources.GetLocalizedString("WinPhoneTutorialPage10"),
          ScreenPath = string.Format("/Assets/VPNGuide/010.png")
        });
        string str4 = "3";
        guidePageViewModelList.Add(new GuidePageViewModel()
        {
          StepNumber = str4,
          StepDescription = LocalizedResources.GetLocalizedString("WinPhoneTutorialPage11"),
          ScreenPath = string.Format("/Assets/VPNGuide/011.png")
        });
      }
      int index1 = flag ? 0 : 2;
      for (int index2 = 2; index2 <= 9; ++index2)
      {
        string str5 = index2.ToString();
        guidePageViewModelList.Add(new GuidePageViewModel()
        {
          StepNumber = (index1 + index2).ToString(),
          StepDescription = LocalizedResources.GetLocalizedString(str1 + str5),
          ScreenPath = string.Format(format, (object) str5)
        });
      }
      guidePageViewModelList[index1].IsFirstPage = true;
      guidePageViewModelList[index1 + 3].DataToCopy = this.Config.Addresses[0].IP;
      guidePageViewModelList[index1 + (this.IsWin10 ? 5 : 8)].DataToCopy = this.Config.PresharedKey;
      guidePageViewModelList[index1 + 6].DataToCopy = this.Config.UsernameXauth;
      guidePageViewModelList[index1 + 7].DataToCopy = this.Config.Password;
      if (this.IsWin10)
        guidePageViewModelList[index1 + 6].IsUserNamePage = true;
      guidePageViewModelList.Add(new GuidePageViewModel()
      {
        StepNumber = (index1 + 10).ToString(),
        StepDescription = LocalizedResources.GetLocalizedString("S_WINPHONE_VPN_TOUR_10"),
        ScreenPath = this.IsWin10 ? string.Format("/Assets/VPNGuide10/012.png") : string.Format("/Assets/VPNGuide/012.png")
      });
      guidePageViewModelList[index1 + 9].IsLastPage = true;
      this.Items = guidePageViewModelList;
      return await base.LoadAsync();
    }
  }
}
