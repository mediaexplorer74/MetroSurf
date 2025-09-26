using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace VPN.Model.SocialNetworks
{
  [DataContract]
  public class ResponceOnGetUserProfileGoogle
  {
    [DataMember(Name = "emails")]
    public List<GoogleEmailFromProfileEmailsList> EmailsList { get; set; }
  }
}
