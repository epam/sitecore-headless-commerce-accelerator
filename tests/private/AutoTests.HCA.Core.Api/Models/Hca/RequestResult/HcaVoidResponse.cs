using System.Net;
using AutoTests.AutomationFramework.API.Models;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutoTests.HCA.Core.API.Models.Hca.RequestResult
{
    public class HcaVoidResponse : IResponse<HcaOkResult<object>, HcaErrorsResult>
    {
        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HcaOkResult<object> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckError()
        {
            Assert.True(IsSuccessful, JsonConvert.SerializeObject(Errors));
        }
    }
}