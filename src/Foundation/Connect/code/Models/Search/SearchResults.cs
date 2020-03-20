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

namespace Wooli.Foundation.Connect.Models.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class SearchResults
    {
        private IEnumerable<CommerceQueryFacet> facets;

        private IList<Item> searchResultItems;

        public SearchResults()
            : this(null, 0, 0, 0, null)
        {
        }

        public SearchResults(
            List<Item> searchResultItems,
            int totalItemCount,
            int totalPageCount,
            int currentPageNumber,
            IEnumerable<CommerceQueryFacet> facets)
        {
            this.SearchResultItems = searchResultItems ?? new List<Item>();
            this.TotalPageCount = totalPageCount;
            this.TotalItemCount = totalItemCount;
            this.Facets = facets ?? Enumerable.Empty<CommerceQueryFacet>();
            this.CurrentPageNumber = currentPageNumber;
        }

        public int CurrentPageNumber { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<CommerceQueryFacet> Facets
        {
            get => this.facets;
            set
            {
                Assert.ArgumentNotNull(value, nameof(value));
                this.facets = value;
            }
        }

        public Item NamedSearchItem { get; set; }

        public IList<Item> SearchResultItems
        {
            get => this.searchResultItems;
            set
            {
                Assert.ArgumentNotNull(value, nameof(value));
                this.searchResultItems = value;
            }
        }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get; set; }
    }
}