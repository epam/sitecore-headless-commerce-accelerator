//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace HCA.Foundation.ReactJss.Serialization.RenderJsonRendering
{
    using System.Collections.Generic;

    using Helpers;

    using Sitecore.Diagnostics;
    using Sitecore.LayoutService.Configuration;
    using Sitecore.LayoutService.Presentation.Pipelines.RenderJsonRendering;

    public class FormatRenderingParams : BaseRenderJsonRendering
    {
        public FormatRenderingParams(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override void SetResult(RenderJsonRenderingArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(args));
            Assert.IsNotNull(args.Result, "args.Result cannot be null");
            args.Result.RenderingParams = this.FormatRenderingParameters(args.Result.RenderingParams);
        }

        private IDictionary<string, string> FormatRenderingParameters(
            IDictionary<string, string> originalRenderingParams)
        {
            var renderingParams = new Dictionary<string, string>();

            foreach (var originalRenderingParam in originalRenderingParams)
            {
                var key = StringHelper.ConvertToCamelCase(originalRenderingParam.Key);
                var value = originalRenderingParam.Value;

                renderingParams[key] = value;
            }

            return renderingParams;
        }
    }
}