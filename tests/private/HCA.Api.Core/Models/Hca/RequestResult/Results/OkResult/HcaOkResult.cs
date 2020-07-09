namespace HCA.Api.Core.Models.Hca.RequestResult.Results.OkResult
{
    public class HcaOkResult<TData> : HcaResult
    {
        public TData Data { get; set; }
    }
}
