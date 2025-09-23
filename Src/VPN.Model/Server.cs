// Decompiled with JetBrains decompiler
// Type: VPN.Model.Server
// Assembly: VPN.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EFE1621-05FE-4101-912F-307D264A394F
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.Model.dll

using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model
{
  [DataContract]
  public class Server
  {
    [DataMember(Name = "region")]
    public string Region { get; set; }

    [DataMember(Name = "domain")]
    public string Domain { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "description")]
    public string Description { get; set; }

    [DataMember(Name = "priority")]
    public int Priority { get; set; }

    [DataMember(Name = "country_code")]
    public string CountryCode { get; set; }

    [DataMember(Name = "vps")]
    public bool VPS { get; set; }

    [DataMember(Name = "flag_http_2x")]
    public string FlagUrl { get; set; }

    public static int Compare(Server x, Server y)
    {
      return x.Priority != y.Priority ? y.Priority.CompareTo(x.Priority) : x.Name.CompareTo(y.Name);
    }
  }
}
