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

namespace HCA.Foundation.Connect.Builders.Search
{
    using System.Linq;

    using Base.Context;

    using DependencyInjection;

    using Loaders;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchQueryBuilder), Lifetime = Lifetime.Singleton)]
    public class SearchQueryBuilder : ISearchQueryBuilder
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly ISitecoreContext sitecoreContext;

        public SearchQueryBuilder(
            ISitecoreContext sitecoreContext,
            ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));

            this.sitecoreContext = sitecoreContext;

            this.commerceSearchManager = commerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }

        public IQueryable<CommerceSellableItemSearchResultItem> BuildProductsQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string searchKeyword,
            ID categoryId,
            CommerceSearchOptions searchOptions)
        {
            Assert.ArgumentNotNull(queryable, nameof(queryable));
            Assert.ArgumentNotNull(searchOptions, nameof(searchOptions));

            queryable = queryable
                .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
                .Where(item => item.Language == this.sitecoreContext.Language.Name);

            queryable = !ID.IsNullOrEmpty(categoryId)
                ? queryable.Where(item => item.Parent == categoryId)
                : queryable.Where(item => !item.ExcludeFromWebsiteSearchResults);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
            {
                queryable = queryable.Where(
                    item => item.Name.Contains(searchKeyword) || item["_displayname"].Contains(searchKeyword));
            }

            return this.commerceSearchManager.AddSearchOptionsToQuery(queryable, searchOptions);
        }

        public IQueryable<CommerceSellableItemSearchResultItem> BuildCategoryQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string categoryName)
        {
            Assert.ArgumentNotNull(queryable, nameof(queryable));
            Assert.ArgumentNotNull(categoryName, nameof(categoryName));

            return queryable
                .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Category)
                .Where(item => item.Language == this.sitecoreContext.Language.Name)
                .Where(item => item.Name == categoryName.ToLowerInvariant());
        }

        public IQueryable<CommerceSellableItemSearchResultItem> BuildProductQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            string productId)
        {
            Assert.ArgumentNotNull(queryable, nameof(queryable));
            Assert.ArgumentNotNull(productId, nameof(productId));

            return queryable
                .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
                .Where(item => item.Language == this.sitecoreContext.Language.Name)
                .Where(item => item.Name == productId.ToLowerInvariant());
        }
    }
}