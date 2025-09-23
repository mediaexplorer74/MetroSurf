// Decompiled with JetBrains decompiler
// Type: VPN.Model.CredentialsGoogleVPNServer
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;
using VPN.Model.SocialNetworks;

#nullable disable
namespace VPN.Model
{
  [DataContract]
  public class CredentialsGoogleVPNServer
  {
    [DataMember(Name = "accessToken")]
    public string AccessToken { get; set; }

    [DataMember(Name = "token_type")]
    public string TokenType { get; set; }

    [DataMember(Name = "expirationDate")]
    public long ExpiresDate { get; set; }

    [DataMember(Name = "expires_in")]
    public string ExpiresDateString { get; set; }

    [DataMember(Name = "id_token")]
    public string IdToken { get; set; }

    [DataMember(Name = "refreshToken")]
    public string RefreshToken { get; set; }

    [DataMember(Name = "email")]
    public string Email { get; set; }

    [DataMember(Name = "code")]
    public string Code { get; set; }

    [DataMember(Name = "serviceProvider")]
    public string ServiceProvider { get; set; }

    public CredentialsGoogleVPNServer(CredentialsGoogle credentialsGoogle)
    {
      this.AccessToken = credentialsGoogle.AccessToken;
      this.Code = credentialsGoogle.Code;
      this.Email = credentialsGoogle.Email;
      this.ExpiresDate = credentialsGoogle.ExpiresDate;
      this.ExpiresDateString = this.ExpiresDate.ToString();
      this.IdToken = credentialsGoogle.IdToken;
      this.RefreshToken = credentialsGoogle.RefreshToken;
      this.ServiceProvider = credentialsGoogle.ServiceProvider;
      this.TokenType = credentialsGoogle.TokenType;
    }
  }
}
