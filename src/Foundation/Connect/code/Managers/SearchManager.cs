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

using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Commerce.Engine.Connect;
using Sitecore.Commerce.Engine.Connect.Interfaces;
using Sitecore.Commerce.Engine.Connect.Search;
using Sitecore.Commerce.Engine.Connect.Search.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Wooli.Foundation.Connect.Models;
using Wooli.Foundation.DependencyInjection;

namespace Wooli.Foundation.Connect.Managers
{
    [Service(typeof(ISearchManager))]
    public class SearchManager : ISearchManager
    {
        public const string ProductCommerceSearchItemType = "SellableItem";
        public const string CategoryCommerceSearchItemType = "Category";

        public SearchResults GetProducts(
            string catalogName,
            ID categoryId,
            CommerceSearchOptions searchOptions,
            string searchKeyword)
        {
            Assert.ArgumentNotNull(catalogName, nameof(catalogName));

            var commerceSearchManager = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>();

            IQueryable<CommerceSellableItemSearchResultItem> queryable =
                GetBaseQueryable(catalogName, ProductCommerceSearchItemType);
            if (!ID.IsNullOrEmpty(categoryId))
                queryable = queryable.Where(x => x.CommerceAncestorIds.Contains(categoryId));

            if (!string.IsNullOrEmpty(searchKeyword))
                queryable = queryable.Where(item =>
                    item.Name.Contains(searchKeyword) || item["_displayname"].Contains(searchKeyword));

            queryable = commerceSearchManager.AddSearchOptionsToQuery(queryable, searchOptions);

            SearchResults<CommerceSellableItemSearchResultItem> results = queryable.GetResults();

            var searchResultsItems = SearchResponse.CreateFromSearchResultsItems(searchOptions, results);
            if (searchResultsItems != null)
                return new SearchResults(
                    searchResultsItems.ResponseItems,
                    searchResultsItems.TotalItemCount,
                    searchResultsItems.TotalPageCount,
                    searchOptions.StartPageIndex,
                    searchResultsItems.Facets.ToList());

            return new SearchResults();
        }

        public SearchResults SearchCatalogItemsByKeyword(
            string catalogName,
            string keyword,
            CommerceSearchOptions searchOptions)
        {
            // ToDo: implement or remove
            throw new NotImplementedException();
        }

        public Item GetProduct(string catalogName, string productId)
        {
            Assert.ArgumentNotNullOrEmpty(catalogName, nameof(catalogName));
            Assert.ArgumentNotNullOrEmpty(productId, nameof(productId));

            Item productItem = null;

            CommerceSellableItemSearchResultItem searchResultItem =
                GetBaseQueryable(catalogName, ProductCommerceSearchItemType)
                    .FirstOrDefault(item => string.Equals(item.Name, productId.ToLowerInvariant()));

            if (searchResultItem != null) productItem = searchResultItem.GetItem();

            return productItem;
        }

        public Item GetCategory(string catalogName, string categoryName)
        {
            Assert.ArgumentNotNullOrEmpty(catalogName, nameof(catalogName));
            Assert.ArgumentNotNullOrEmpty(categoryName, nameof(categoryName));

            Item productItem = null;

            CommerceSellableItemSearchResultItem searchResultItem =
                GetBaseQueryable(catalogName, CategoryCommerceSearchItemType)
                    .FirstOrDefault(item => string.Equals(item.Name, categoryName.ToLowerInvariant()));

            if (searchResultItem != null) productItem = searchResultItem.GetItem();

            return productItem;
        }

        public List<Item> GetNavigationCategories()
        {
            throw new NotImplementedException();
        }

        public List<Item> GetCategoryChildCategories(ID categoryId)
        {
            throw new NotImplementedException();
        }

        private IQueryable<CommerceSellableItemSearchResultItem> GetBaseQueryable(string catalogName,
            string searchItemType)
        {
            Assert.ArgumentNotNullOrEmpty(catalogName, nameof(catalogName));
            Assert.ArgumentNotNullOrEmpty(searchItemType, nameof(searchItemType));

            var commerceSearchManager = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            using (IProviderSearchContext searchContext =
                commerceSearchManager.GetIndex(catalogName).CreateSearchContext())
            {
                IQueryable<CommerceSellableItemSearchResultItem> searchResultItems = searchContext
                    .GetQueryable<CommerceSellableItemSearchResultItem>()
                    .Where(item => item.CommerceSearchItemType == searchItemType)
                    .Where(item => item.Language == Context.Language.Name);

                return searchResultItems;
            }
        }
    }
}