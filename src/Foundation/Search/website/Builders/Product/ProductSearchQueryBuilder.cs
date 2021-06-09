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

namespace HCA.Foundation.Search.Builders.Product
{
    using System.Linq;

    using Base.Context;

    using DependencyInjection;

    using Loaders;

    using Models.Entities.Product;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data;
    using Sitecore.Diagnostics;

    [Service(typeof(IProductSearchQueryBuilder), Lifetime = Lifetime.Singleton)]
    public class ProductSearchQueryBuilder : IProductSearchQueryBuilder
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly ISitecoreContext sitecoreContext;

        public ProductSearchQueryBuilder(
            ISitecoreContext sitecoreContext,
            ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));

            this.sitecoreContext = sitecoreContext;

            this.commerceSearchManager = commerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }

        public IQueryable<CommerceSellableItemSearchResultItem> BuildProductQuery(
            IQueryable<CommerceSellableItemSearchResultItem> queryable,
            ProductSearchOptions productSearchOptions,
            CommerceSearchOptions commerceSearchOptions)
        {
            Assert.ArgumentNotNull(queryable, nameof(queryable));
            Assert.ArgumentNotNull(productSearchOptions, nameof(productSearchOptions));

            queryable = queryable
                .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
                .Where(item => item.Language == this.sitecoreContext.Language.Name);

            var categoryId = new ID(productSearchOptions.CategoryId);
            queryable = !ID.IsNullOrEmpty(categoryId)
                ? queryable.Where(item => item.Parent == categoryId)
                : queryable.Where(item => !item.ExcludeFromWebsiteSearchResults);

            if (!string.IsNullOrWhiteSpace(productSearchOptions.SearchKeyword))
            {
                queryable = queryable.Where(
                    item => item.Name.Contains(productSearchOptions.SearchKeyword) ||
                        item["_displayname"].Contains(productSearchOptions.SearchKeyword));
            }

            return commerceSearchOptions != null
                ? this.commerceSearchManager.AddSearchOptionsToQuery(queryable, commerceSearchOptions)
                : queryable;
        }

        //public IQueryable<CommerceSellableItemSearchResultItem> BuildProductQuery(
        //    IQueryable<CommerceSellableItemSearchResultItem> queryable,
        //    string productId)
        //{
        //    Assert.ArgumentNotNull(queryable, nameof(queryable));
        //    Assert.ArgumentNotNull(productId, nameof(productId));

        //    return queryable
        //        .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Product)
        //        .Where(item => item.Language == this.sitecoreContext.Language.Name)
        //        .Where(item => item.Name == productId.ToLowerInvariant());
        //}
    }
}