using System.Net;
using AutoTests.AutomationFramework.API.Models;

namespace AutoTests.HCA.Core.API.Models.Braitree.RequestResult
{
    public class BraintreeResponse<TData> : IResponse<TData, object>
        where TData : class
    {
        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        //TODO: Error Model
        public object Errors { get; set; }

        public TData OkResponseData { get; set; }
    }
}