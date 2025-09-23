// Decompiled with JetBrains decompiler
// Type: VPN.Model.CredentialsFacebookVPNServer
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;
using VPN.Model.SocialNetworks;

#nullable disable
namespace VPN.Model
{
  [DataContract]
  public class CredentialsFacebookVPNServer
  {
    [DataMember(Name = "permissions")]
    public string[] Permissions { get; set; }

    [DataMember(Name = "accessToken")]
    public string AccessToken { get; set; }

    [DataMember(Name = "expirationDate")]
    public long ExpirationDate { get; set; }

    public CredentialsFacebookVPNServer(CredentialsFacebook credentialsGoogle)
    {
      this.AccessToken = credentialsGoogle.AccessToken;
      this.ExpirationDate = credentialsGoogle.ExpirationDate;
      this.Permissions = CredentialsFacebook.Permissions;
    }
  }
}
