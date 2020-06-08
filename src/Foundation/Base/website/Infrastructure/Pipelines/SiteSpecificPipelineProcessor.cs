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

namespace HCA.Foundation.Base.Infrastructure.Pipelines
{
    using System.Collections.Generic;

    using Sitecore;
    using Sitecore.Pipelines.HttpRequest;

    public abstract class SiteSpecificPipelineProcessor : HttpRequestProcessor
    {
        protected SiteSpecificPipelineProcessor()
        {
            this.Sites = new List<string>();
        }

        public List<string> Sites { get; set; }

        public override sealed void Process(HttpRequestArgs args)
        {
            if (!this.Sites.Contains(Context.Site?.Name))
            {
                return;
            }

            this.DoProcess(args);
        }

        protected abstract void DoProcess(HttpRequestArgs args);
    }
}