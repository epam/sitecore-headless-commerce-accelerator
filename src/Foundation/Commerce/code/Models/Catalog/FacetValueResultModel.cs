//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Commerce.Models
{
    using Sitecore.ContentSearch.Linq;
    using Sitecore.Diagnostics;

    using TypeLite;

    [TsClass]
    public class FacetValueResultModel
    {
        public string Name { get; protected set; }

        public int AggregateCount { get; protected set; }

        public void Initialize(FacetValue queryFacet)
        {
            Assert.ArgumentNotNull(queryFacet, nameof(queryFacet));

            this.Name = queryFacet.Name;
            this.AggregateCount = queryFacet.AggregateCount;
        }
    }
}
