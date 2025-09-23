// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.VPNDataLoadViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using VPN.Localization;
using VPN.Model;
using VPN.Model.SocialNetworks.Exceptions;
using VPN.ViewModel.Http;

#nullable disable
namespace VPN.ViewModel
{
  [DataContract]
  public class VPNDataLoadViewModel : DataLoadViewModel
  {
    private ICommand _updateCommand;

    [IgnoreDataMember]
    public ICommand UpdateCommand
    {
      get
      {
        return BaseViewModel.GetCommand(ref this._updateCommand, (Action<object>) (o => this.UpdateAsync()));
      }
    }

    protected void LoadLocaliztion()
    {
      this.LoadingMessage = LocalizedResources.GetLocalizedString("WinPhoneLoading");
    }

    protected override async Task<bool> TryLoadAsyncInner(Func<Task<bool>> loadAction)
    {
      try
      {
        if (this._isContentActual)
          return true;
        this.IsWorkingOffline = false;
        this.IsWasProblemsWithConnection = false;
        return await loadAction();
      }
      catch (WebException ex)
      {
        this.IsWasProblemsWithConnection = true;
        if (ex.Response == null && ((byte) ex.Status == (byte) 1 || (byte) ex.Status == (byte) 2))
          this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INTERNET_PROBLEM"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
        else
          this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_REQUEST_FAILED"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"), isButtonTryAgainVisible: true);
        return false;
      }
      catch (VPNResponceException ex)
      {
        int result;
        int.TryParse(ex.Message, out result);
        string errorDescription;
        switch (result)
        {
          case 301:
            errorDescription = LocalizedResources.GetLocalizedString("S_ALREADY_CONFIRMED_SERV");
            break;
          case 302:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_USERNAME_PASSWORD_SERV");
            break;
          case 303:
            errorDescription = LocalizedResources.GetLocalizedString("S_LOGIN_ATTEMPTS_EXCEEDED_SERV").Replace("%s", AutoLoginAgent.Current.UserEmail ?? "");
            break;
          case 304:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_REFCODE_SERV");
            break;
          case 305:
            errorDescription = LocalizedResources.GetLocalizedString("S_ENCRYPTYON_FAILURE_SERV");
            break;
          case 306:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_URL_SERV");
            break;
          case 308:
            errorDescription = LocalizedResources.GetLocalizedString("S_TOO_MUCH_REGS");
            break;
          case 309:
            errorDescription = LocalizedResources.GetLocalizedString("S_TOO_MANY_DEVICES");
            break;
          case 310:
            errorDescription = LocalizedResources.GetLocalizedString("S_USER_ALREADY_EXIST");
            break;
          case 311:
            errorDescription = LocalizedResources.GetLocalizedString("S_NEED_CONFIRM_EMAIL");
            break;
          case 313:
            errorDescription = LocalizedResources.GetLocalizedString("S_USER_BANNED");
            break;
          case 314:
            errorDescription = LocalizedResources.GetLocalizedString("S_USER_DELETED");
            break;
          case 323:
            errorDescription = LocalizedResources.GetLocalizedString("S_ACCOUNT_CANT_BE_USED");
            break;
          case 327:
            errorDescription = LocalizedResources.GetLocalizedString("S_PROFILE_IS_INCORRECT");
            break;
          case 341:
            errorDescription = LocalizedResources.GetLocalizedString("S_PURCHASE_ALREADY_EXISTS_SERV");
            break;
          case 342:
            errorDescription = LocalizedResources.GetLocalizedString("S_APPLESERVER_NOT_RESPONSE_SERV");
            break;
          case 343:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_PURCHASE_SERV");
            break;
          case 361:
            errorDescription = LocalizedResources.GetLocalizedString("S_TOO_MUCH_MAIL");
            break;
          case 501:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_REQUEST_SERV");
            break;
          case 502:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_PARAMS_SERV");
            break;
          case 503:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_SESSION_SERV");
            break;
          case 504:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_USERNAME_SERV");
            break;
          case 505:
            errorDescription = LocalizedResources.GetLocalizedString("S_INVALID_DB_CONNECTION_SERV");
            break;
          default:
            errorDescription = LocalizedResources.GetLocalizedString("S_APPLESERVER_NOT_RESPONSE_SERV");
            break;
        }
        this.RunLoadFailed(errorDescription.Replace("%s", result.ToString()), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"), isButtonTryAgainVisible: true);
        return false;
      }
      catch (FacebookAppReturnInvalidDataException ex)
      {
        return false;
      }
      catch (FacebookOAuthException ex)
      {
        this.RunLoadFailed(ex.Message, alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"), isButtonTryAgainVisible: true);
        return false;
      }
      catch (HttpRequestException ex)
      {
        this.IsWasProblemsWithConnection = true;
        if (ex.InnerException is WebException innerException && innerException.Response == null && ((byte) innerException.Status == (byte) 1 || (byte) innerException.Status == (byte) 2))
          this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INTERNET_PROBLEM"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
        else
          this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_REQUEST_FAILED"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"), isButtonTryAgainVisible: true);
        return false;
      }
      catch (Exception ex)
      {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
          this.IsWasProblemsWithConnection = true;
          this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_INTERNET_PROBLEM"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"));
          return false;
        }
        if (ex.HResult != -2147012816 && ex.HResult != -2147012889 && ex.HResult != -2147012879 && ex.HResult != -2147012865)
        {
          int hresult = ex.HResult;
        }
        this.IsWasProblemsWithConnection = true;
        this.RunLoadFailed(LocalizedResources.GetLocalizedString("S_REQUEST_FAILED"), alertHeader: LocalizedResources.GetLocalizedString("S_ERROR"), isButtonTryAgainVisible: true);
        return false;
      }
    }
  }
}
