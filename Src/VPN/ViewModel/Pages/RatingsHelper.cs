// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Pages.RatingsHelper
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Threading.Tasks;
using VPN.Localization;
using VPN.ViewModel.Http;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Store;
using Windows.System;
using Windows.UI.Popups;

#nullable disable
namespace VPN.ViewModel.Pages
{
  public class RatingsHelper
  {
    private const string AppUri = "ms-windows-store:reviewapp?appid=69b97f20-7b3b-457b-9e15-3c6a3194ce71";
    private const string LaunchCount = "LAUNCH_COUNT";
    private const string Reviewed = "REVIEWED";
    private const string SupportMail = "mailto:{0}?subject={1}&body={2}";
    private const int FirstCount = 1;
    private const int SecondCount = 8;
    private int _launchCount;
    private bool _reviewed;
    private bool _isTrial;
    private string _message;
    private string _title;
    private string _yesText;
    private string _noText;
    private FeedbackState _state;
    private static ResourceLoader _resLoader;

    private void LoadState()
    {
      try
      {
        this._launchCount = SettingsStorageHelper.GetSetting<int>("LAUNCH_COUNT");
        this._reviewed = SettingsStorageHelper.GetSetting<bool>("REVIEWED");
        this._isTrial = CurrentApp.LicenseInformation.IsActive && CurrentApp.LicenseInformation.IsTrial;
        if (this._reviewed)
          return;
        ++this._launchCount;
        if (this._launchCount == 1)
          this._state = FeedbackState.FirstReview;
        else if (this._launchCount == 8)
          this._state = FeedbackState.SecondReview;
        this.StoreState();
      }
      catch (Exception ex)
      {
      }
    }

    private void StoreState()
    {
      try
      {
        SettingsStorageHelper.SetSetting("LAUNCH_COUNT", (object) this._launchCount);
        SettingsStorageHelper.SetSetting("REVIEWED", (object) this._reviewed);
      }
      catch (Exception ex)
      {
      }
    }

    public async Task InitialiseAsync()
    {
      this.LoadState();
      if (this._isTrial)
        return;
      if (this._state == FeedbackState.FirstReview)
      {
        this._title = LocalizedResources.GetLocalizedString("S_RATE_US_ON_STORE");
        this._message = LocalizedResources.GetLocalizedString("WinPhoneRatingMessage1");
        this._yesText = LocalizedResources.GetLocalizedString("WinPhoneRatingYes");
        this._noText = LocalizedResources.GetLocalizedString("S_NO_BTN");
        await this.ShowMessageAsync();
      }
      else
      {
        if (this._state != FeedbackState.SecondReview)
          return;
        this._title = LocalizedResources.GetLocalizedString("S_RATE_US_ON_STORE");
        this._message = LocalizedResources.GetLocalizedString(" WinPhoneRatingMessage2");
        this._yesText = LocalizedResources.GetLocalizedString("WinPhoneRatingYes");
        this._noText = LocalizedResources.GetLocalizedString("S_NO_BTN");
        await this.ShowMessageAsync();
      }
    }

    public async Task MakeReviewWithMessages(bool IsFirstMessage)
    {
      this._state = !IsFirstMessage ? FeedbackState.SecondReview : FeedbackState.FirstReview;
      if (this._state == FeedbackState.FirstReview)
      {
        this._title = LocalizedResources.GetLocalizedString("S_RATE_US_ON_STORE");
        this._message = LocalizedResources.GetLocalizedString("WinPhoneRatingMessage1");
        this._yesText = LocalizedResources.GetLocalizedString("WinPhoneRatingYes");
        this._noText = LocalizedResources.GetLocalizedString("S_NO_BTN");
        await this.ShowMessageAsync();
      }
      else
      {
        if (this._state != FeedbackState.SecondReview)
          return;
        this._title = LocalizedResources.GetLocalizedString("S_RATE_US_ON_STORE");
        this._message = LocalizedResources.GetLocalizedString("WinPhoneRatingMessage2");
        this._yesText = LocalizedResources.GetLocalizedString("WinPhoneRatingYes");
        this._noText = LocalizedResources.GetLocalizedString("S_NO_BTN");
        await this.ShowMessageAsync();
      }
    }

    private async Task ShowMessageAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RatingsHelper.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new RatingsHelper.\u003C\u003Ec__DisplayClass19_0();
      MessageDialog messageDialog = new MessageDialog(this._message, this._title);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass190.result = 0;
      // ISSUE: method pointer
      messageDialog.Commands.Add((IUICommand) new UICommand(this._yesText, new UICommandInvokedHandler((object) cDisplayClass190, __methodptr(\u003CShowMessageAsync\u003Eb__0))));
      // ISSUE: method pointer
      messageDialog.Commands.Add((IUICommand) new UICommand(this._noText, new UICommandInvokedHandler((object) cDisplayClass190, __methodptr(\u003CShowMessageAsync\u003Eb__1))));
      messageDialog.put_DefaultCommandIndex(0U);
      IUICommand iuiCommand = await messageDialog.ShowAsync();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass190.result == 1)
        await this.OnYesClickAsync();
      else
        await this.OnNoClickAsync();
    }

    private async Task OnNoClickAsync()
    {
      if (this._state != FeedbackState.FirstReview)
        return;
      this._title = LocalizedResources.GetLocalizedString("WinPhoneFeedbackTitle");
      this._message = string.Format(LocalizedResources.GetLocalizedString("WinPhoneFeedbackMessage1"), (object) LocalizedResources.GetLocalizedString("app_name"));
      this._yesText = LocalizedResources.GetLocalizedString("S_WRITE_FEEDBACK");
      this._noText = LocalizedResources.GetLocalizedString("S_NO_BTN");
      this._state = FeedbackState.Feedback;
      await this.ShowMessageAsync();
    }

    private async Task OnYesClickAsync()
    {
      if (this._state == FeedbackState.FirstReview || this._state == FeedbackState.SecondReview)
      {
        this._reviewed = true;
        this.StoreState();
        int num = await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=69b97f20-7b3b-457b-9e15-3c6a3194ce71", UriKind.Absolute)) ? 1 : 0;
      }
      else
      {
        if (this._state != FeedbackState.Feedback)
          return;
        await RatingsHelper.SendFeedback();
      }
    }

    public static async Task MakeReview()
    {
      SettingsStorageHelper.SetSetting("REVIEWED", (object) true);
      int num = await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=69b97f20-7b3b-457b-9e15-3c6a3194ce71", UriKind.Absolute)) ? 1 : 0;
    }

    public static async Task SendFeedback()
    {
      PackageVersion version = Package.Current.Id.Version;
      string str = string.Format("\n\n\nDevice: {1}\nWindows Phone version: " + (Constants.IsWin10() ? "10" : "8.1") + "\nVersion: {0}\nDevice language ISO: {2}\nCountry: {3}\nVirtual IP: {4}\nLocal IP: {5}\nKSID: {6}\n", (object) Constants.GetAppVersion(), (object) Constants.GetDeviceModel(), (object) Constants.GetCurrentLanguage(false), (object) Constants.GetCounty(), (object) VPNServerAgent.Current.AccountStatus.User.CurrentIP, (object) VPNServerAgent.Current.AccountStatus.User.RealIP, (object) AutoLoginAgent.Current.UserEmail);
      int num = await Launcher.LaunchUriAsync(new Uri(Uri.EscapeUriString(string.Format("mailto:{0}?subject={1}&body={2}", (object) "support@keepsolid.com", (object) LocalizedResources.GetLocalizedString("S_FEEDBACK_EMAIL"), (object) str)), UriKind.Absolute)) ? 1 : 0;
    }
  }
}
