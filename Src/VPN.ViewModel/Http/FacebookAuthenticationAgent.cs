// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Http.FacebookAuthenticationAgent
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using VPN.Model.SocialNetworks;
using VPN.Model.SocialNetworks.Exceptions;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.System;
using Windows.Web.Http;

#nullable disable
namespace VPN.ViewModel.Http
{
  internal class FacebookAuthenticationAgent
  {
    public static readonly FacebookAuthenticationAgent Current = new FacebookAuthenticationAgent();
    private const string AccessTokenKey = "access_token";
    private const string ErrorCodeKey = "error_code";
    private const string ErrorDescriptionKey = "error_description";
    private const string ErrorKey = "error";
    private const string ErrorReasonKey = "error_reason";
    private const string ExpiresInKey = "expires_in";
    private const string RedirectUri = "msft-69b97f207b3b457b9e153c6a3194ce71";
    private const string FacebookConnectUriTemplate = "fbconnect://authorize?client_id={0}&scope={1}&redirect_uri={2}";
    public const string FacebookClientId = "566235876818089";
    public const string FacebookСlientSecret = "c5436b2b98b3f480b2aca007a2e266d2";
    private const string FacebookCallbackUrl = "https://m.facebook.com/connect/login_success.html";

    public async Task<bool> AuthenticationFromAppReceivedAsync(string queryString)
    {
      string error = !string.IsNullOrWhiteSpace(queryString) ? FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "error") : throw new FacebookAppReturnInvalidDataException("");
      string errorDescription = FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "error_description");
      FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "error_reason");
      int result1 = 0;
      int.TryParse(FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "error_code"), out result1);
      if (string.IsNullOrEmpty(error) && result1 == 0)
      {
        CredentialsFacebook credentials = new CredentialsFacebook();
        credentials.AccessToken = FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "access_token");
        long result2;
        long.TryParse(FacebookAuthenticationAgent.GetQueryStringValueFromUri(queryString, "expires_in"), out result2);
        credentials.ExpirationDate = result2;
        credentials.RealExpirationDate = DateTime.UtcNow + TimeSpan.FromSeconds((double) result2);
        CredentialsFacebook credentialsFacebook = credentials;
        string email = await FacebookAuthenticationAgent.GetEmail(credentials.AccessToken);
        credentialsFacebook.Email = email;
        credentialsFacebook = (CredentialsFacebook) null;
        AutoLoginAgent.Current.CredentialsFacebook = credentials;
        return true;
      }
      throw new FacebookOAuthException(string.Format("{0}: {1}", (object) error, (object) errorDescription));
    }

    public static string GetFacebookUrl()
    {
      return "https://www.facebook.com/dialog/oauth?client_id=" + Uri.EscapeDataString("566235876818089") + "&redirect_uri=" + Uri.EscapeDataString("https://m.facebook.com/connect/login_success.html") + "&scope=" + CredentialsFacebook.Scope + "&display=popup&response_type=token";
    }

    public static string GetFacebookUrlForIE()
    {
      return "https://m.facebook.com/v2.7/dialog/oauth?client_id=" + Uri.EscapeDataString("566235876818089") + "&redirect_uri=msft-69b97f207b3b457b9e153c6a3194ce71%3A%2F%2Fauthorize&scope=" + CredentialsFacebook.Scope + "&display=touch&type=user_agent";
    }

    public static async Task<bool> AuthenticateWithApp()
    {
      bool result;
      try
      {
        LauncherOptions launcherOptions = new LauncherOptions();
        launcherOptions.FallbackUri = new Uri(FacebookAuthenticationAgent.GetFacebookUrlForIE());
        result = await Launcher.LaunchUriAsync(new Uri(string.Format("fbconnect://authorize?client_id={0}&scope={1}&redirect_uri={2}", "566235876818089", CredentialsFacebook.Scope, "msft-69b97f207b3b457b9e153c6a3194ce71://authorize")), launcherOptions);
      }
      catch
      {
        return false;
      }
      return result;
    }

    private static string GetQueryStringValueFromUri(string uri, string key)
    {
      int startIndex = 0;
      if (uri.Contains("?"))
        startIndex = uri.LastIndexOf("?", StringComparison.Ordinal);
      else if (uri.Contains("#"))
        startIndex = uri.LastIndexOf("#", StringComparison.Ordinal);
      if (startIndex == -1)
        startIndex = 0;
      string str1 = uri.Substring(startIndex, uri.Length - startIndex).Replace("#", string.Empty).Replace("?", string.Empty);
      char[] chArray1 = new char[1]{ '&' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str2.Split(chArray2);
        if (strArray[0].ToLowerInvariant() == key.ToLowerInvariant())
          return Uri.UnescapeDataString(strArray[1]);
      }
      return string.Empty;
    }

    public async Task<CredentialsFacebook> LoginAsync()
    {
      WebAuthenticationBroker.AuthenticateAndContinue(new Uri(FacebookAuthenticationAgent.GetFacebookUrl()), new Uri("https://m.facebook.com/connect/login_success.html"), (ValueSet) null, (WebAuthenticationOptions) 0);
      return (CredentialsFacebook) null;
    }

    private void GetKeyValues(
      string webAuthResultResponseData,
      out string accessToken,
      out string expiresIn)
    {
      string[] strArray1 = webAuthResultResponseData.Substring(webAuthResultResponseData.IndexOf("access_token", StringComparison.Ordinal)).Split('&');
      accessToken = (string) null;
      expiresIn = (string) null;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        string[] strArray2 = strArray1[index].Split('=');
        switch (strArray2[0])
        {
          case "access_token":
            accessToken = strArray2[1];
            break;
          case "expires_in":
            expiresIn = strArray2[1];
            break;
        }
      }
    }

    public async Task<CredentialsFacebook> Finalize(
      WebAuthenticationBrokerContinuationEventArgs args)
    {
      try
      {
        return await this.GetSession(args.WebAuthenticationResult);
      }
      catch (Exception ex)
      {
      }
      return (CredentialsFacebook) null;
    }

    private async Task<CredentialsFacebook> GetSession(WebAuthenticationResult result)
    {
      if (result.ResponseStatus == WebAuthenticationStatus.Success)
      {
        string accessToken;
        string expiresIn;
        this.GetKeyValues(result.ResponseData, out accessToken, out expiresIn);
        string email = await FacebookAuthenticationAgent.GetEmail(accessToken);
        return new CredentialsFacebook()
        {
          AccessToken = accessToken,
          ExpirationDate = long.Parse(expiresIn),
          Email = email
        };
      }
      if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
        throw new Exception("Error http");
      if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
        throw new Exception("User Canceled.");
      return (CredentialsFacebook) null;
    }

    private static async Task<string> GetEmail(string accessToken)
    {
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(new HttpRequestMessage(HttpMethod.Get, new Uri("https://graph.facebook.com/me?fields=email&" + "access_token=" + accessToken, UriKind.Absolute)));
      httpResponseMessage.EnsureSuccessStatusCode();
      return ((ResponceOnGetUserProfileFacebook) new DataContractJsonSerializer(typeof (ResponceOnGetUserProfileFacebook)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream())).Email;
    }
  }
}
