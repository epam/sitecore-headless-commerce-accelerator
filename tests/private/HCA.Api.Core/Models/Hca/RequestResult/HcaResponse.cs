using HCA.Api.Core.Models.Common;
using HCA.Api.Core.Models.Hca.RequestResult.Results.ErrorResult;
using HCA.Api.Core.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HCA.Api.Core.Models.Hca.RequestResult
{
    public class HcaResponse<T> : IResponse<HcaOkResult<T>, HcaErrorsResult>
        where T : class
    {
        public bool IsSuccessful { get; set; }

        public HcaOkResult<T> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckError() =>
            Assert.True(IsSuccessful, JsonConvert.SerializeObject(Errors));
    }
}
