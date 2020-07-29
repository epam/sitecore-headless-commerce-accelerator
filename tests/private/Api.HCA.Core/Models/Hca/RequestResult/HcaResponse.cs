using Api.AutomationFramework.Models.Common;
using Api.HCA.Core.Models.Hca.RequestResult.Results.ErrorResult;
using Api.HCA.Core.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Api.HCA.Core.Models.Hca.RequestResult
{
    public class HcaResponse<T> : IResponse<HcaOkResult<T>, HcaErrorsResult>
        where T : class
    {
        public bool IsSuccessful { get; set; }

        public HcaOkResult<T> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckError()
        {
            Assert.True(IsSuccessful, JsonConvert.SerializeObject(Errors));
        }
    }
}