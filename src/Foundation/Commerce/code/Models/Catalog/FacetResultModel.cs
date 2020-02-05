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

using System.Collections.Generic;
using Sitecore.Commerce.Engine.Connect.Search.Models;
using Sitecore.ContentSearch.Linq;
using Sitecore.Diagnostics;
using TypeLite;

namespace Wooli.Foundation.Commerce.Models.Catalog
{
    [TsClass]
    public class FacetResultModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IList<object> Values { get; set; }

        public IList<FacetValueResultModel> FoundValues { get; set; }

        public void Initialize(CommerceQueryFacet commerceQueryFacet)
        {
            Assert.ArgumentNotNull(commerceQueryFacet, nameof(commerceQueryFacet));

            Name = commerceQueryFacet.Name;
            DisplayName = commerceQueryFacet.DisplayName;
            Values = commerceQueryFacet.Values;

            var foundValues = new List<FacetValueResultModel>();
            foreach (FacetValue facetValue in commerceQueryFacet.FoundValues)
            {
                var facetValueResultModel = new FacetValueResultModel();
                facetValueResultModel.Initialize(facetValue);
                foundValues.Add(facetValueResultModel);
            }

            FoundValues = foundValues;
        }
    }
}