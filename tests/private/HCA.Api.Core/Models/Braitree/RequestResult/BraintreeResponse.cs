using HCA.Api.Core.Models.Common;

namespace HCA.Api.Core.Models.Braitree.RequestResult
{
    public class BraintreeResponse<TData> : IResponse<TData, object>
        where TData : class
    {
        public bool IsSuccessful { get; set; }

        //TODO: Error Model
        public object Errors { get; set; }

        public TData OkResponseData { get; set; }
    }
}
