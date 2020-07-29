namespace Api.HCA.Core.Models.Hca.RequestResult.Results.ErrorResult
{
    public class HcaErrorResult : HcaResult
    {
        public string Error { get; set; }

        public string ExceptionMessage { get; set; }
    }
}