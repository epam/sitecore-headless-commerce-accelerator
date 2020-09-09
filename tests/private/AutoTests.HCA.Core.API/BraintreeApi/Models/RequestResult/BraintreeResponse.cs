using System;
using System.Collections.Generic;
using System.Net;
using AutoTests.AutomationFramework.API.Models;

namespace AutoTests.HCA.Core.API.BraintreeApi.Models.RequestResult
{
    public class BraintreeResponse<TData> : IResponse<TData, object>
        where TData : class
    {
        public RequestInfo RequestInfo { get; set; }

        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        //TODO: Error Model

        public object Errors { get; set; }

        public TData OkResponseData { get; set; }

        public void CheckSuccessfulResponse()
        {
            throw new NotImplementedException();
        }

        public void CheckUnSuccessfulResponse()
        {
            throw new NotImplementedException();
        }

        public void VerifyErrors(string message, HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            throw new NotImplementedException();
        }

        public void VerifyErrors(IEnumerable<string> messages,
            HttpStatusCode code = HttpStatusCode.BadRequest)
        {
            throw new NotImplementedException();
        }

        public void VerifyOkResponseData()
        {
            throw new NotImplementedException();
        }

        public void VerifyResponseData()
        {
            throw new NotImplementedException();
        }
    }
}