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

namespace Wooli.Foundation.Commerce.Models.Entities.Search
{
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ProductsSearchOptions
    {
        public string SearchKeyword { get; set; }

        public int? PageNumber { get; set; }

        public NameValueCollection FacetValues { get; set; }

        public string SortField { get; set; }

        public int? PageSize { get; set; }

        public SortDirection? SortDirection { get; set; }

        public string CurrentCatalogItemId { get; set; }

        public string CurrentItemId { get; set; }
    }
}