using Api.AutomationFramework.Models.Common;
using Api.HCA.Core.Models.Hca.RequestResult.Results.ErrorResult;
using Api.HCA.Core.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HCA.Api.Core.Models.Hca.RequestResult
{
    public class HcaVoidResponse : IResponse<HcaOkResult<object>, HcaErrorsResult>
    {
        public bool IsSuccessful { get; set; }

        public HcaOkResult<object> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckError()
        {
            Assert.True(IsSuccessful, JsonConvert.SerializeObject(Errors));
        }
    }
}