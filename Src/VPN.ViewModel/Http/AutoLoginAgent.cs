// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.Http.AutoLoginAgent
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VPN.Model.SocialNetworks;
using Windows.Storage;

#nullable disable
namespace VPN.ViewModel.Http
{
  [DataContract]
  public class AutoLoginAgent
  {
    public static AutoLoginAgent Current = new AutoLoginAgent();
    private CredentialsGoogle _сredentialsGoogle;
    private CredentialsFacebook _сredentialsFacebook;
    private CredentialsKeepSolidID _сredentialsKeepSolidID;

    [DataMember]
    public string UserEmail { get; set; }

    [DataMember]
    public string Session { get; set; }

    [DataMember]
    public string CabinetUrl { get; set; }

    [IgnoreDataMember]
    public CredentialsGoogle CredentialsGoogle
    {
      get
      {
        if (this._сredentialsGoogle == null)
        {
          this._сredentialsGoogle = CacheAgent.LoadFromLocalSettings<CredentialsGoogle>("google");
          if (this._сredentialsGoogle != null)
            this._сredentialsGoogle.RefreshToken = CacheAgent.Decrypt(this._сredentialsGoogle.RefreshToken, this._сredentialsGoogle.Email, this._сredentialsGoogle.Key);
        }
        return this._сredentialsGoogle;
      }
      set
      {
        this._сredentialsGoogle = value;
        if (value == null)
          return;
        string accessToken = value.AccessToken;
        string key;
        string str = CacheAgent.Encrypt(value.RefreshToken, value.Email, out key);
        value.RefreshToken = str;
        value.Key = key;
        CacheAgent.SaveToLocalSettings<CredentialsGoogle>("google", value);
        value.AccessToken = accessToken;
      }
    }

    [IgnoreDataMember]
    public CredentialsFacebook CredentialsFacebook
    {
      get
      {
        if (this._сredentialsFacebook == null)
        {
          this._сredentialsFacebook = CacheAgent.LoadFromLocalSettings<CredentialsFacebook>("facebook");
          if (this._сredentialsFacebook != null)
            this._сredentialsFacebook.AccessToken = CacheAgent.Decrypt(this._сredentialsFacebook.AccessToken, this._сredentialsFacebook.Email, this._сredentialsFacebook.Key);
        }
        return this._сredentialsFacebook;
      }
      set
      {
        this._сredentialsFacebook = value;
        if (value == null)
          return;
        string accessToken = value.AccessToken;
        string key;
        string str = CacheAgent.Encrypt(value.AccessToken, value.Email, out key);
        value.AccessToken = str;
        value.Key = key;
        CacheAgent.SaveToLocalSettings<CredentialsFacebook>("facebook", value);
        value.AccessToken = accessToken;
      }
    }

    [IgnoreDataMember]
    public CredentialsKeepSolidID CredentialsKeepSolidID
    {
      get
      {
        if (this._сredentialsKeepSolidID == null)
        {
          this._сredentialsKeepSolidID = CacheAgent.LoadFromLocalSettings<CredentialsKeepSolidID>("keepsolid");
          if (this._сredentialsKeepSolidID != null)
            this._сredentialsKeepSolidID.Password = CacheAgent.Decrypt(this._сredentialsKeepSolidID.Password, this._сredentialsKeepSolidID.Email, this._сredentialsKeepSolidID.Key);
        }
        return this._сredentialsKeepSolidID;
      }
      set
      {
        this._сredentialsKeepSolidID = value;
        if (value == null)
          return;
        string password = value.Password;
        string key;
        string str = CacheAgent.Encrypt(value.Password, value.Email, out key);
        value.Password = str;
        value.Key = key;
        CacheAgent.SaveToLocalSettings<CredentialsKeepSolidID>("keepsolid", value);
        value.Password = password;
      }
    }

    public static async Task<bool> AutoLogin()
    {
      CredentialsGoogle credentialsGoogle1 = AutoLoginAgent.Current.CredentialsGoogle;
      if (credentialsGoogle1 != null)
      {
        CredentialsGoogle credentialsGoogle2 = await GoogleAuthenticationAgent.RefreshAccessToken(credentialsGoogle1);
        int num = await VPNServerAgent.Current.LoginGoogleAsync(credentialsGoogle2) ? 1 : 0;
        return true;
      }
      CredentialsFacebook credentialsFacebook = AutoLoginAgent.Current.CredentialsFacebook;
      if (credentialsFacebook != null && credentialsFacebook.RealExpirationDate > DateTime.UtcNow)
      {
        int num = await VPNServerAgent.Current.LoginFacebookAsync(credentialsFacebook) ? 1 : 0;
        return true;
      }
      CredentialsKeepSolidID credentialsKeepSolidId = AutoLoginAgent.Current.CredentialsKeepSolidID;
      if (credentialsKeepSolidId == null)
        return false;
      int num1 = await VPNServerAgent.Current.LoginAsync(credentialsKeepSolidId.Email, credentialsKeepSolidId.Password, true) ? 1 : 0;
      return true;
    }

    public async Task Logout()
    {
      this.CredentialsFacebook = (CredentialsFacebook) null;
      this.CredentialsGoogle = (CredentialsGoogle) null;
      this.CredentialsKeepSolidID = (CredentialsKeepSolidID) null;
      CacheAgent.DeleteLoginCache();
      int num = await VPNServerAgent.Current.LogoutAsync() ? 1 : 0;
      this.CabinetUrl = (string) null;
      this.Session = (string) null;
    }

    public void SaveKeepSolidEmail(string email)
    {
      this.UserEmail = email;
      ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["KeepSolidEmail"] = (object) email;
    }

    public string GetKeepSolidEmail()
    {
      return (string) ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["KeepSolidEmail"];
    }
  }
}
