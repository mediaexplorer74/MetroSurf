// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ServerSubstitution
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;

#nullable disable
namespace MetroLab.Common
{
  public class ServerSubstitution : IServerSubstitution
  {
    private readonly string _oldServerBasePath;
    private readonly string _newServerBasePath;

    public ServerSubstitution(string oldServerBasePath, string newServerBasePath)
    {
      if (string.IsNullOrEmpty(oldServerBasePath))
        throw new ArgumentException(nameof (oldServerBasePath));
      if (string.IsNullOrEmpty(newServerBasePath))
        throw new ArgumentException(nameof (newServerBasePath));
      this._oldServerBasePath = oldServerBasePath;
      this._newServerBasePath = newServerBasePath;
    }

    public string TransformUrl(string sourceUrl)
    {
      if (sourceUrl == null)
        return (string) null;
      return sourceUrl.StartsWith(this._oldServerBasePath) ? this._newServerBasePath + sourceUrl.Substring(this._oldServerBasePath.Length) : sourceUrl;
    }
  }
}
