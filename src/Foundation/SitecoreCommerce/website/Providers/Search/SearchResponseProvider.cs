﻿//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.SitecoreCommerce.Providers.Search
{
    using System.Diagnostics.CodeAnalysis;

    using DependencyInjection;

    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ISearchResponseProvider), Lifetime = Lifetime.Singleton)]
    public class SearchResponseProvider : ISearchResponseProvider
    {
        public SearchResponse CreateFromSearchResultsItems<T>(
            CommerceSearchOptions searchOptions,
            SearchResults<T> sitecoreSearchResults) where T : SearchResultItem
        {
            return SearchResponse.CreateFromSearchResultsItems(searchOptions, sitecoreSearchResults);
        }
    }
}