// Decompiled with JetBrains decompiler
// Type: VPN.Model.SocialNetworks.CredentialsGoogle
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model.SocialNetworks
{
  [DataContract]
  public class CredentialsGoogle
  {
    [DataMember(Name = "access_token")]
    public string AccessToken { get; set; }

    [DataMember(Name = "token_type")]
    public string TokenType { get; set; }

    [DataMember(Name = "expires_in")]
    public long ExpiresDate { get; set; }

    [DataMember(Name = "id_token")]
    public string IdToken { get; set; }

    [DataMember(Name = "refresh_token")]
    public string RefreshToken { get; set; }

    [DataMember(Name = "email")]
    public string Email { get; set; }

    [DataMember(Name = "code")]
    public string Code { get; set; }

    [DataMember(Name = "serviceProvider")]
    public string ServiceProvider { get; set; }

    [DataMember(Name = "Key")]
    public string Key { get; set; }
  }
}
