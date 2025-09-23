// Decompiled with JetBrains decompiler
// Type: VPN.Model.SocialNetworks.CredentialsFacebook
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model.SocialNetworks
{
  [DataContract]
  public class CredentialsFacebook
  {
    public static readonly string[] Permissions = new string[2]
    {
      "email",
      "public_profile"
    };

    [DataMember(Name = "accessToken")]
    public string AccessToken { get; set; }

    [DataMember(Name = "expirationDate")]
    public long ExpirationDate { get; set; }

    [DataMember(Name = "realExpirationDate")]
    public DateTime RealExpirationDate { get; set; }

    [DataMember(Name = "email")]
    public string Email { get; set; }

    public static string Scope
    {
      get
      {
        string scope = "";
        for (int index = 0; index < CredentialsFacebook.Permissions.Length; ++index)
        {
          scope += CredentialsFacebook.Permissions[index];
          if (index != CredentialsFacebook.Permissions.Length - 1)
            scope += ",";
        }
        return scope;
      }
    }

    [DataMember(Name = "Key")]
    public string Key { get; set; }
  }
}
