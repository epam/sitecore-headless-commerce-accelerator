using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.AutomationFramework.API.Models;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AutoTests.HCA.Core.API.Models.Hca.RequestResult
{
    public class HcaResponse<T> : IResponse<HcaOkResult<T>, HcaErrorsResult>
        where T : class
    {
        public RequestInfo RequestInfo { get; set; }

        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public HcaOkResult<T> OkResponseData { get; set; }

        public HcaErrorsResult Errors { get; set; }

        public void CheckSuccessfulResponse()
        {
            Assert.True(IsSuccessful, $"{RequestInfo.Method} {RequestInfo.BaseUrl} isn't passed. " +
                                      $"Response:{JsonConvert.SerializeObject(Errors)}");
        }

        public void CheckUnSuccessfulResponse()
        {
            Assert.False(IsSuccessful, $"Bad Request {RequestInfo.Method} {RequestInfo.BaseUrl} is passed. " +
                                       $"Response:{JsonConvert.SerializeObject(Errors)}");
        }

        public void VerifyOkResponseData()
        {
            VerifyStatuses(OkResponseData, HttpStatusCode.OK, HcaStatus.Ok);
            ExtendedAssert.NotNull(OkResponseData.Data, nameof(OkResponseData.Data));
        }

        public void VerifyErrors(string message, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            VerifyErrors(new List<string> { message }, code);
        }

        public void VerifyErrors(IEnumerable<string> messages, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            VerifyStatuses(Errors, code, HcaStatus.Error);
            ExtendedAssert.NotNull(Errors.Errors, nameof(Errors.Errors));
            ExtendedAssert.AreEqual(messages.FirstOrDefault(), Errors.Error, nameof(Errors.ExceptionMessage));

            Assert.True(!messages.Except(Errors.Errors).Any(),
                $"{nameof(Errors.Errors)} contains an unexpected sequence of errors." +
                $"\r\nExpected: '{string.Join(';', messages)}'." +
                $"\r\nActual: {string.Join(';', Errors.Errors)}");
        }

        private void VerifyStatuses(HcaResult hcaResult, HttpStatusCode expHttpStatusCode, HcaStatus expHcaStatus)
        {
            ExtendedAssert.NotNull(hcaResult, "HcaResult");
            ExtendedAssert.AreEqual(expHttpStatusCode, StatusCode, nameof(StatusCode));
            ExtendedAssert.AreEqual(expHcaStatus, hcaResult?.Status, nameof(hcaResult.Status));
        }
    }
}