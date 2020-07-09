using System.Collections.Generic;

namespace HCA.Api.Core.Models.Hca.RequestResult.Results.ErrorResult
{
    public class HcaErrorsResult : HcaErrorResult
    {
        public IList<string> Errors { get; set; }
    }
}
