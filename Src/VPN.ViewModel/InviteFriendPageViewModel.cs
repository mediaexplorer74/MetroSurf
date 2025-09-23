// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.InviteFriendPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VPN.Localization;
using VPN.ViewModel.Http;

#nullable disable
namespace VPN.ViewModel
{
  [DataContract]
  public class InviteFriendPageViewModel : PageViewModel
  {
    public string InviteFriendsPageDescription
    {
      get => LocalizedResources.GetLocalizedString("S_TIP_INVITES");
    }

    public string InviteFriendsPageHeader
    {
      get => LocalizedResources.GetLocalizedString("S_GET_FREE").ToUpper();
    }

    public string InviteFriendsListContactsItemText
    {
      get => LocalizedResources.GetLocalizedString("WinPhoneInviteFriendsListContactsItem");
    }

    public string InviteFriendsListHeader
    {
      get => LocalizedResources.GetLocalizedString("S_INVITE_FRIENDS") + ":";
    }

    public async Task InviteFriendAsync(string friendemail)
    {
      if (!await this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await VPNServerAgent.Current.InviteFriendAsync(friendemail))))
        return;
      await BaseViewModel.ShowDialogAsync(LocalizedResources.GetLocalizedString("S_INVITE_SUCCESS"), dialogOkTitle: LocalizedResources.GetLocalizedString("S_OK"));
    }
  }
}
