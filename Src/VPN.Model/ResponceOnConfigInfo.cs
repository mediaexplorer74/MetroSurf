// Decompiled with JetBrains decompiler
// Type: VPN.Model.ResponceOnConfigInfo
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model
{
  [DataContract]
  public class ResponceOnConfigInfo
  {
    [DataMember(Name = "response")]
    public int Response { get; set; }

    [DataMember(Name = "config")]
    public Config Config { get; set; }
  }
}
