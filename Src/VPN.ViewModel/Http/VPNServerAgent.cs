// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Http.VPNServerAgent
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VPN.Localization;
using VPN.Model;
using VPN.Model.SocialNetworks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;

#nullable disable
namespace VPN.ViewModel.Http
{
  public class VPNServerAgent
  {
    public static readonly VPNServerAgent Current = new VPNServerAgent();
    public const string VPNservice = "com.simplexsolutionsinc.vpnguard";
    public const string ApiLoginUrlString = "https://auth.simplexsolutionsinc.com/";
    public const string ApiUrlString = "https://api.vpnunlimitedapp.com/";
    public const string GoogleOauthServiceName = "googleplus";
    public const string FacebookOauthServiceName = "facebook";
    public ResponceOnAccountStatus AccountStatus;
    public ResponceOnServersList ServersList;
    public Config VPNConnectionConfig;

    public static string ToBase64(string str)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
    }

    public async Task<bool> LoginAsync(string _email, string _password, bool isAutoLogin)
    {
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("login")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_email)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_password)), "password");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponseOnLogin result = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response != 200)
      {
        if (!isAutoLogin)
          throw new VPNResponceException(result.Response.ToString());
        await this.CheckIfLogoutIsNeededAsync(result.Response);
      }
      AutoLoginAgent.Current.Session = result.Session;
      AutoLoginAgent.Current.CabinetUrl = result.UserInfo.CabinetUrl.Replace("https://my.keepsolid.com/auth/", "https://my.keepsolid.com/account/");
      AutoLoginAgent.Current.SaveKeepSolidEmail(result.UserInfo.Username ?? _email);
      if (((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["ShowVPNNotConnected"] == null)
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["FirstLoginTime"] = (object) DateTime.Now.Ticks;
      return true;
    }

    public async Task<bool> RegisterAsync(string _email, string _password)
    {
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("register")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_email)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_password)), "password");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("win_phone_purch")), "purch_type");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponseOnLogin responseOnLogin = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (responseOnLogin.Response != 200)
        throw new VPNResponceException(responseOnLogin.Response.ToString());
      return await this.LoginAsync(_email, _password, false);
    }

    public async Task<bool> GetVPNServersListAsync()
    {
      if (AutoLoginAgent.Current.Session == null)
      {
        if (!await AutoLoginAgent.AutoLogin())
          return false;
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("vpn_servers")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.vpnunlimitedapp.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponceOnServersList result = (ResponceOnServersList) new DataContractJsonSerializer(typeof (ResponceOnServersList)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response != 200)
      {
        if (result.Response != 503)
          throw new VPNResponceException(result.Response.ToString());
        int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
        return await this.GetVPNServersListAsync();
      }
      this.ServersList = result;
      return true;
    }

    public async Task<bool> GetAccountAsync()
    {
      if (AutoLoginAgent.Current.Session == null)
      {
        if (!await AutoLoginAgent.AutoLogin())
          return false;
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("account_status")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.vpnunlimitedapp.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponceOnAccountStatus result = (ResponceOnAccountStatus) new DataContractJsonSerializer(typeof (ResponceOnAccountStatus)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response != 200)
      {
        if (result.Response != 503)
          throw new VPNResponceException(result.Response.ToString());
        int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
        return await this.GetAccountAsync();
      }
      this.AccountStatus = result;
      return true;
    }

    public async Task<bool> GetConnectionCredentialsAsync(string region)
    {
      if (AutoLoginAgent.Current.Session == null)
      {
        if (!await AutoLoginAgent.AutoLogin())
          return false;
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("config_info")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("ipsec-psk")), "protocol");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(region)), nameof (region));
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.vpnunlimitedapp.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponceOnConfigInfo result = (ResponceOnConfigInfo) new DataContractJsonSerializer(typeof (ResponceOnConfigInfo)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response != 200)
      {
        if (result.Response != 503)
          throw new VPNResponceException(result.Response.ToString());
        int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
        return await this.GetConnectionCredentialsAsync(region);
      }
      this.VPNConnectionConfig = result.Config;
      return true;
    }

    public async Task<bool> LoginFacebookAsync(CredentialsFacebook credentialsFacebook)
    {
      string str;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (CredentialsFacebookVPNServer)).WriteObject((Stream) memoryStream, (object) new CredentialsFacebookVPNServer(credentialsFacebook));
        byte[] array = memoryStream.ToArray();
        str = Encoding.UTF8.GetString(array, 0, ((IEnumerable<byte>) array).Count<byte>());
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("loginsocial")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("2")), "social_login_version");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(credentialsFacebook.Email)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("facebook")), "oauthservice");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(str)), "oauthcredentials");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      ResponseOnLogin responseOnLogin = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await (await new HttpClient().SendRequestAsync(httpRequestMessage)).Content.ReadAsBufferAsync()).AsStream());
      AutoLoginAgent.Current.Session = responseOnLogin.Response == 200 ? responseOnLogin.Session : throw new VPNResponceException(responseOnLogin.Response.ToString());
      AutoLoginAgent.Current.UserEmail = responseOnLogin.UserInfo.Username ?? credentialsFacebook.Email;
      AutoLoginAgent.Current.CabinetUrl = responseOnLogin.UserInfo.CabinetUrl.Replace("https://my.keepsolid.com/auth/", "https://my.keepsolid.com/account/");
      if (((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["ShowVPNNotConnected"] == null)
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["FirstLoginTime"] = (object) DateTime.Now.Ticks;
      return true;
    }

    public async Task<bool> LoginGoogleAsync(CredentialsGoogle credentialsGoogle)
    {
      string str;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (CredentialsGoogleVPNServer)).WriteObject((Stream) memoryStream, (object) new CredentialsGoogleVPNServer(credentialsGoogle));
        byte[] array = memoryStream.ToArray();
        str = Encoding.UTF8.GetString(array, 0, ((IEnumerable<byte>) array).Count<byte>());
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("loginsocial")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("2")), "social_login_version");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(credentialsGoogle.Email)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("googleplus")), "oauthservice");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(str)), "oauthcredentials");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      ResponseOnLogin responseOnLogin = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await (await new HttpClient().SendRequestAsync(httpRequestMessage)).Content.ReadAsBufferAsync()).AsStream());
      AutoLoginAgent.Current.Session = responseOnLogin.Response == 200 ? responseOnLogin.Session : throw new VPNResponceException(responseOnLogin.Response.ToString());
      AutoLoginAgent.Current.UserEmail = responseOnLogin.UserInfo.Username ?? credentialsGoogle.Email;
      AutoLoginAgent.Current.CabinetUrl = responseOnLogin.UserInfo.CabinetUrl.Replace("https://my.keepsolid.com/auth/", "https://my.keepsolid.com/account/");
      if (((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["ShowVPNNotConnected"] == null)
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["FirstLoginTime"] = (object) DateTime.Now.Ticks;
      return true;
    }

    private void AddParametersForRequestToVPN(
      HttpMultipartFormDataContent multipartFormDataContent)
    {
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetDeviceModel())), "device");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetDeviceID())), "deviceid");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("WinPhone")), "platform");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetPlatformVersion())), "platformversion");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetAppVersion())), "appversion");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetCurrentLanguage())), "locale");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Constants.GetTimeZone())), "timezone");
    }

    public async Task<bool> LogoutAsync()
    {
      if (AutoLoginAgent.Current.Session == null)
        return true;
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("logout")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.vpnunlimitedapp.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      ResponseOnLogin responseOnLogin = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await (await new HttpClient().SendRequestAsync(httpRequestMessage)).Content.ReadAsBufferAsync()).AsStream());
      return true;
    }

    public async Task<bool> InviteFriendAsync(string friendemail)
    {
      if (AutoLoginAgent.Current.Session == null)
      {
        if (!await AutoLoginAgent.AutoLogin())
          return false;
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("simplexinvite")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(friendemail)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("email")), "type");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      ResponceOnFriendInvitation result = (ResponceOnFriendInvitation) new DataContractJsonSerializer(typeof (ResponceOnFriendInvitation)).ReadObject((await (await new HttpClient().SendRequestAsync(httpRequestMessage)).Content.ReadAsBufferAsync()).AsStream());
      if (result.Response == 200)
        return true;
      if (result.Response != 503)
        throw new VPNResponceException(result.Response.ToString());
      int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
      return await this.InviteFriendAsync(friendemail);
    }

    public async Task<bool> ChangePassword(string _password, string _newPassword)
    {
      if (AutoLoginAgent.Current.Session == null)
      {
        if (!await AutoLoginAgent.AutoLogin())
          return false;
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("changeaccountpassword")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_password)), "password");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_newPassword)), "newpassword");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("win_phone_purch")), "purch_type");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponseOnLogin result = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response == 200)
        return true;
      if (result.Response != 503)
        throw new VPNResponceException(result.Response.ToString());
      int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
      return await this.ChangePassword(_password, _newPassword);
    }

    public async Task<bool> RemindPassword(string _email)
    {
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("recoveryaccountpasswordmailsend")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(_email)), "login");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("com.simplexsolutionsinc.vpnguard")), "service");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://auth.simplexsolutionsinc.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponseOnLogin responseOnLogin = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (responseOnLogin.Response != 200)
        throw new VPNResponceException(responseOnLogin.Response.ToString());
      return true;
    }

    public async Task<bool> PurchaseAsync(string receiptXML, string transactionID)
    {
      ProductPurchaceReceiptVPNServer graph = new ProductPurchaceReceiptVPNServer()
      {
        Receipt = receiptXML
      };
      string str;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (ProductPurchaceReceiptVPNServer)).WriteObject((Stream) memoryStream, (object) graph);
        byte[] array = memoryStream.ToArray();
        str = Encoding.UTF8.GetString(array, 0, ((IEnumerable<byte>) array).Count<byte>());
      }
      HttpMultipartFormDataContent multipartFormDataContent = new HttpMultipartFormDataContent();
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("apppurchase")), "action");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(Guid.NewGuid().ToString())), "purchaseid");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(transactionID)), "transaction_id");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(str)), "receipt");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64(AutoLoginAgent.Current.Session)), "session");
      multipartFormDataContent.Add((IHttpContent) new HttpStringContent(VPNServerAgent.ToBase64("win_phone_purch")), "purch_type");
      this.AddParametersForRequestToVPN(multipartFormDataContent);
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://api.vpnunlimitedapp.com/", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) multipartFormDataContent;
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      ResponseOnLogin result = (ResponseOnLogin) new DataContractJsonSerializer(typeof (ResponseOnLogin)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      if (result.Response == 200)
        return true;
      if (result.Response != 503)
        throw new VPNResponceException(result.Response.ToString());
      int num = await AutoLoginAgent.AutoLogin() ? 1 : 0;
      return await this.PurchaseAsync(receiptXML, transactionID);
    }

    public async Task CheckIfLogoutIsNeededAsync(int vpnErrorCode)
    {
      if (vpnErrorCode != 302 && vpnErrorCode != 308 && vpnErrorCode != 309 && vpnErrorCode != 311 && vpnErrorCode != 303 && vpnErrorCode != 313 && vpnErrorCode != 323 && vpnErrorCode != 314 && vpnErrorCode != 327 || AutoLoginAgent.Current.Session == null)
        return;
      await AutoLoginAgent.Current.Logout();
      AppViewModel.Current.ClearNavigationStack();
      AppViewModel.Current.GoHome();
    }

    public async Task ShowErrorAsync(int vpnErrorCode)
    {
      string errorDescription;
      switch (vpnErrorCode)
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
      MessageDialog messageDialog = new MessageDialog(errorDescription.Replace("%s", vpnErrorCode.ToString()), LocalizedResources.GetLocalizedString("S_ERROR"));
      messageDialog.Commands.Add((IUICommand) new UICommand(LocalizedResources.GetLocalizedString("S_OK"), (UICommandInvokedHandler) null));
      IUICommand iuiCommand = await messageDialog.ShowAsync();
    }
  }
}
