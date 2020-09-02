using System.Runtime.Serialization;

namespace AutoTests.HCA.Core.API.Models.Hca
{
    public enum HcaStatus
    {
        NotSet,
        [DataMember(Name = "ok")] Ok,
        [DataMember(Name = "error")] Error
    }
}