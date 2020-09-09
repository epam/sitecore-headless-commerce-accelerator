namespace AutoTests.HCA.Core.API.HcaApi.Models.RequestResult.Results.OkResult
{
    public class HcaOkResult<TData> : HcaResult
    {
        public TData Data { get; set; }
    }
}