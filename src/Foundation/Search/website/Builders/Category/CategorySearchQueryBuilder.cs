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

namespace HCA.Foundation.Search.Builders.Category
{
    using System.Linq;

    using Base.Context;

    using DependencyInjection;

    using Loaders;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Diagnostics;

    [Service(typeof(ICategorySearchQueryBuilder), Lifetime = Lifetime.Singleton)]
    public class CategorySearchQueryBuilder : ICategorySearchQueryBuilder
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly ISitecoreContext sitecoreContext;

        public CategorySearchQueryBuilder(
            ISitecoreContext sitecoreContext,
            ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));

            this.sitecoreContext = sitecoreContext;

            this.commerceSearchManager = commerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
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
    }
}