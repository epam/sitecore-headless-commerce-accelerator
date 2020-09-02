using System.Collections.Generic;
using System.Net;

namespace AutoTests.AutomationFramework.API.Models
{
    public interface IResponse<TData, TErrors>
        where TData : class
        where TErrors : class
    {
        RequestInfo RequestInfo { get; set; }

        bool IsSuccessful { get; set; }

        HttpStatusCode StatusCode { get; set; }

        TData OkResponseData { get; set; }

        TErrors Errors { get; set; }

        void CheckSuccessfulResponse();

        void CheckUnSuccessfulResponse();

        void VerifyOkResponseData();

        void VerifyErrors(string message, HttpStatusCode code = HttpStatusCode.BadRequest);

        void VerifyErrors(IEnumerable<string> messages, HttpStatusCode code = HttpStatusCode.BadRequest);
    }
}