//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.SitecoreCommerce.Services.Product
{
    using Base.Context;
    using DependencyInjection;
    using Foundation.Search.Models.Common;
    using Foundation.Search.Models.Entities.Product;
    using Foundation.Search.Providers.Product;
    using Loaders;
    using Mappers.Search;
    using Providers.Search;
    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System.Linq;

    [Service(typeof(ICommerceProductSearchService), Lifetime = Lifetime.Singleton)]
    public class CommerceProductSearchService : ICommerceProductSearchService
    {
        private readonly ICommerceSearchManager commerceSearchManager;

        private readonly ISitecoreContext sitecoreContext;

        private readonly ISearchMapper searchMapper;

        private readonly ISearchResponseProvider searchResponseProvider;

        private readonly IProductSearchResultProvider searchResultProvider;

        public CommerceProductSearchService(
            ISearchMapper searchMapper,
            ISearchResponseProvider searchResponseProvider,
            IProductSearchResultProvider searchResultProvider,
            ISitecoreContext sitecoreContext,
            ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(searchResponseProvider, nameof(searchResponseProvider));
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));

            this.searchMapper = searchMapper;
            this.searchResponseProvider = searchResponseProvider;
            this.searchResultProvider = searchResultProvider;
            this.sitecoreContext = sitecoreContext;

            this.commerceSearchManager = commerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }

        public SearchResults<Item> GetSearchResults(ProductSearchOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            var commerceSearchOptions = this.searchMapper.Map<ProductSearchOptions, CommerceSearchOptions>(options);

            var results =
                this.searchResultProvider.GetSearchResults<CommerceSellableItemSearchResultItem>(
                    queryable =>
                    {
                        queryable = queryable
                            .Where(item => item.CommerceSearchItemType == CommerceSearchItemType.SellableItem)
                            .Where(item => item.Language == this.sitecoreContext.Language.Name);

                        var categoryId = new ID(options.CategoryId);
                        queryable = !ID.IsNullOrEmpty(categoryId)
                            ? queryable.Where(item => item.Parent == categoryId)
                            : queryable.Where(item => !item.ExcludeFromWebsiteSearchResults);

                        if (!string.IsNullOrWhiteSpace(options.SearchKeyword))
                        {
                            queryable = queryable.Where(
                                item => item.Name.Contains(options.SearchKeyword) ||
                                    item["_displayname"].Contains(options.SearchKeyword) || 
                                    item.ProductId == options.SearchKeyword);
                        }

                        if (options.ProductIds?.Any() == true)
                        {
                            queryable = queryable.Where(
                                item => options.ProductIds.Contains(item.ProductId));
                        }

                        return commerceSearchOptions != null
                            ? this.commerceSearchManager.AddSearchOptionsToQuery(queryable, commerceSearchOptions)
                            : queryable;
                    });

            var searchResponse =
                this.searchResponseProvider.CreateFromSearchResultsItems(commerceSearchOptions, results);
            var searchResults =
                this.searchMapper.Map<SearchResponse, SearchResults<Item>>(searchResponse);

            return searchResults;
        }
    }
}