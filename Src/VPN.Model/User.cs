// Decompiled with JetBrains decompiler
// Type: VPN.Model.User
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model
{
  [DataContract]
  public class User
  {
    [DataMember(Name = "real_ip_address")]
    public string RealIP { get; set; }

    [DataMember(Name = "ip_address")]
    public string CurrentIP { get; set; }

    [DataMember(Name = "timelast")]
    public double TimeLeft { get; set; }

    [DataMember(Name = "vpn_active")]
    public bool IsVPNActive { get; set; }

    [DataMember(Name = "vpn_region")]
    public string VPNRegion { get; set; }

    [DataMember(Name = "vpn_region_name")]
    public string VPNRegionName { get; set; }

    [DataMember(Name = "vpn_region_description")]
    public string VPNRegionDescription { get; set; }
  }
}
