using System.Net;
using AutoTests.AutomationFramework.API.Models;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult;

namespace AutoTests.HCA.Core.API.Models.Hca.RequestResult
{
    public class HcaResponse<T> : IResponse<HcaOkResult<T>, HcaErrorsResult>
        where T : class
    {
        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HcaOkResult<T> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }
    }
}