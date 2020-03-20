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

namespace Wooli.Foundation.Connect.Managers.Search
{
    using System.Linq;

    using Base.Context;

    using DependencyInjection;

    using Loaders;

    using Mappers.Search;

    using Models;
    using Models.Search;

    using Providers.Search;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchManagerV2))]
    public class SearchManagerV2 : ISearchManagerV2
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly ISearchMapper searchMapper;
        private readonly ISearchResponseProvider searchResponseProvider;
        private readonly ISearchResultProvider<CommerceSellableItemSearchResultItem> searchResultProvider;
        private readonly ISitecoreContext sitecoreContext;

        public SearchManagerV2(
            ISearchMapper searchMapper,
            ISitecoreContext sitecoreContext,
            ICommerceTypeLoader commerceTypeLoader,
            ISearchResponseProvider searchResponseProvider,
            ISearchResultProvider<CommerceSellableItemSearchResultItem> searchResultProvider)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(searchResultProvider, nameof(searchResultProvider));

            this.searchMapper = searchMapper;
            this.sitecoreContext = sitecoreContext;
            this.searchResponseProvider = searchResponseProvider;
            this.searchResultProvider = searchResultProvider;

            this.commerceSearchManager = commerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }

        public SearchResultsV2 GetProducts(
            string searchKeyword,
            SearchOptions searchOptions)
        {
            Assert.ArgumentNotNull(searchKeyword, nameof(searchKeyword));
            Assert.ArgumentNotNull(searchOptions, nameof(searchOptions));

            var commerceSearchOptions = this.searchMapper.Map<SearchOptions, CommerceSearchOptions>(searchOptions);

            var results = this.searchResultProvider.GetSearchResults(
                queryable =>
                {
                    queryable = queryable
                        .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
                        .Where(item => item.Language == this.sitecoreContext.Language.Name);

                    if (!string.IsNullOrWhiteSpace(searchKeyword))
                    {
                        queryable = queryable.Where(
                            item => item.Name.Contains(searchKeyword) || item.DisplayName.Contains(searchKeyword));
                    }

                    return this.commerceSearchManager.AddSearchOptionsToQuery(queryable, commerceSearchOptions);
                });

            var searchResponse =
                this.searchResponseProvider.CreateFromSearchResultsItems(commerceSearchOptions, results);

            return this.searchMapper.Map<SearchResponse, SearchResultsV2>(searchResponse);
        }

        public Item GetCategoryItem(string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            var result = this.searchResultProvider.GetSearchResult(
                queryable =>
                {
                    return queryable
                        .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Category)
                        .Where(item => item.Language == this.sitecoreContext.Language.Name)
                        .Where(item => item.Name == categoryName.ToLowerInvariant());
                });

            return result?.GetItem();
        }

        public Item GetProductItem(string productId)
        {
            Assert.ArgumentNotNullOrEmpty(productId, nameof(productId));

            var result = this.searchResultProvider.GetSearchResult(
                queryable =>
                {
                    return queryable
                        .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
                        .Where(item => item.Language == this.sitecoreContext.Language.Name)
                        .Where(item => item.Name == productId.ToLowerInvariant());
                });

            return result?.GetItem();
        }
    }
}