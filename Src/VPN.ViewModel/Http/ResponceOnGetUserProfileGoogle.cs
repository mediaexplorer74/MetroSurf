using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace VPN.ViewModel.Http
{
  [DataContract]
  public class ResponceOnGetUserProfileGoogle
  {
    [DataMember(Name = "emails")]
    public List<GoogleEmail> EmailsList { get; set; }
  }

  [DataContract]
  public class GoogleEmail
  {
    [DataMember(Name = "value")]
    public string Email { get; set; }
  }
}
