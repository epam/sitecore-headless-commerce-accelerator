using System.Net;
using AutoTests.AutomationFramework.API.Models;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutoTests.HCA.Core.API.Models.Hca.RequestResult
{
    public class HcaResponse<T> : IResponse<HcaOkResult<T>, HcaErrorsResult>
        where T : class
    {
        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HcaOkResult<T> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckSuccessfulResponse()
        {
            Assert.True(IsSuccessful, $"The Request isn't passed. Response:{JsonConvert.SerializeObject(Errors)}");
        }

        public void VerifyResponseData()
        {
            ExtendedAssert.AreEqual(HttpStatusCode.OK, StatusCode, nameof(StatusCode));
            ExtendedAssert.NotNull(OkResponseData, nameof(OkResponseData));
            ExtendedAssert.AreEqual(HcaStatus.Ok, OkResponseData.Status, nameof(OkResponseData.Status));
            ExtendedAssert.NotNull(OkResponseData.Data, nameof(OkResponseData.Data));
        }
    }
}