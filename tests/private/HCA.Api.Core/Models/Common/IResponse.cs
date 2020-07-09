namespace HCA.Api.Core.Models.Common
{
    public interface IResponse<TData, TErrors>
        where TData : class
        where TErrors : class
    {
        public bool IsSuccessful { get; set; }

        public TData OkResponseData { get; set; }

        public TErrors Errors { get; set; }
    }
}
