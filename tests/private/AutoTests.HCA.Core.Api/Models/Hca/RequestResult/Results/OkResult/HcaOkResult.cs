namespace AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult
{
    public class HcaOkResult<TData> : HcaResult
    {
        public TData Data { get; set; }
    }
}