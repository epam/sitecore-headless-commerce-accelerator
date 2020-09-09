using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.HcaApi.Models.RequestResult.Results.ErrorResult
{
    public class HcaErrorsResult : HcaErrorResult
    {
        public IList<string> Errors { get; set; }
    }
}