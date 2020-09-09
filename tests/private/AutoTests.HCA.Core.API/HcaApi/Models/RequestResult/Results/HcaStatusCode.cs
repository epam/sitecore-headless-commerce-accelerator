using System.Runtime.Serialization;

namespace AutoTests.HCA.Core.API.HcaApi.Models.RequestResult.Results
{
    public enum HcaStatusCode
    {
        NotSet,
        [DataMember(Name = "ok")] Ok,
        [DataMember(Name = "error")] Error
    }
}