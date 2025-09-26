// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Http.GoogleAuthenticationAgent
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VPN.Model.SocialNetworks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage.Streams;
using Windows.Web.Http;

#nullable disable
namespace VPN.ViewModel.Http
{
  public class GoogleAuthenticationAgent
  {
    public static readonly GoogleAuthenticationAgent Current = new GoogleAuthenticationAgent();
    public const string GoogleServiceProviderName = "Google";

    public async void LoginAsync()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("https://accounts.google.com/o/oauth2/auth?client_id=");
      stringBuilder.Append(Uri.EscapeDataString("922621893574-grqe7ehb5sq05b4ag38nf0ho19icq2lt.apps.googleusercontent.com"));
      stringBuilder.Append("&scope=openid%20email%20profile%20https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fplus.me");
      stringBuilder.Append("&redirect_uri=");
      stringBuilder.Append(Uri.EscapeDataString("http://localhost"));
      stringBuilder.Append("&state=foobar");
      stringBuilder.Append("&response_type=code");
      WebAuthenticationBroker.AuthenticateAndContinue(new Uri(stringBuilder.ToString()), new Uri("http://localhost"), (ValueSet) null, (WebAuthenticationOptions) 0);
    }

    private string GetCode(string webAuthResultResponseData)
    {
      return ((IEnumerable<string>) webAuthResultResponseData.Split('&')).FirstOrDefault<string>((Func<string, bool>) (value => value.Contains("code")));
    }

    public async Task<bool> GetSession(WebAuthenticationBrokerContinuationEventArgs args)
    {
      WebAuthenticationResult result = args.WebAuthenticationResult;
      if (result.ResponseStatus == WebAuthenticationStatus.Success)
      {
        string code = this.GetCode(result.ResponseData);
        CredentialsGoogle token = await GoogleAuthenticationAgent.GetToken(code);
        token.Code = code;
        token.ServiceProvider = "Google";
        AutoLoginAgent.Current.CredentialsGoogle = token;
        return true;
      }
      if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
        return false;
      throw new WebException();
    }

    private static async Task<CredentialsGoogle> GetToken(string code)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(code);
      stringBuilder.Append("&client_id=");
      stringBuilder.Append(Uri.EscapeDataString("922621893574-grqe7ehb5sq05b4ag38nf0ho19icq2lt.apps.googleusercontent.com"));
      stringBuilder.Append("&client_secret=");
      stringBuilder.Append(Uri.EscapeDataString("VqZR9tiTyrP6-UY9G48_EJTg"));
      stringBuilder.Append("&redirect_uri=");
      stringBuilder.Append(Uri.EscapeDataString("http://localhost"));
      stringBuilder.Append("&grant_type=authorization_code");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://accounts.google.com/o/oauth2/token", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) new HttpStringContent(stringBuilder.ToString(), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/x-www-form-urlencoded");
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      CredentialsGoogle oauthcredentialsGoogle = (CredentialsGoogle) new DataContractJsonSerializer(typeof (CredentialsGoogle)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      CredentialsGoogle credentialsGoogle = oauthcredentialsGoogle;
      string email = await GoogleAuthenticationAgent.GetEmail(oauthcredentialsGoogle.AccessToken);
      credentialsGoogle.Email = email;
      credentialsGoogle = (CredentialsGoogle) null;
      return oauthcredentialsGoogle;
    }

    private static async Task<string> GetEmail(string accessToken)
    {
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(new HttpRequestMessage(HttpMethod.Get, new Uri("https://www.googleapis.com/plus/v1/people/me?" + "access_token=" + accessToken, UriKind.Absolute)));
      httpResponseMessage.EnsureSuccessStatusCode();
      return ((ResponceOnGetUserProfileGoogle) new DataContractJsonSerializer(typeof (ResponceOnGetUserProfileGoogle)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream())).EmailsList[0].Email;
    }

    public static async Task<CredentialsGoogle> RefreshAccessToken(
      CredentialsGoogle savedCredentials)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("refresh_token=");
      stringBuilder.Append(Uri.EscapeDataString(savedCredentials.RefreshToken));
      stringBuilder.Append("&client_id=");
      stringBuilder.Append(Uri.EscapeDataString("922621893574-grqe7ehb5sq05b4ag38nf0ho19icq2lt.apps.googleusercontent.com"));
      stringBuilder.Append("&client_secret=");
      stringBuilder.Append(Uri.EscapeDataString("VqZR9tiTyrP6-UY9G48_EJTg"));
      stringBuilder.Append("&grant_type=refresh_token");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("https://www.googleapis.com/oauth2/v3/token", UriKind.Absolute));
      httpRequestMessage.Content = (IHttpContent) new HttpStringContent(stringBuilder.ToString(), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/x-www-form-urlencoded");
      HttpResponseMessage httpResponseMessage = await new HttpClient().SendRequestAsync(httpRequestMessage);
      httpResponseMessage.EnsureSuccessStatusCode();
      CredentialsGoogle credentialsGoogle = (CredentialsGoogle) new DataContractJsonSerializer(typeof (CredentialsGoogle)).ReadObject((await httpResponseMessage.Content.ReadAsBufferAsync()).AsStream());
      savedCredentials.AccessToken = credentialsGoogle.AccessToken;
      savedCredentials.ExpiresDate = credentialsGoogle.ExpiresDate;
      savedCredentials.TokenType = credentialsGoogle.TokenType;
      return savedCredentials;
    }
  }
}
