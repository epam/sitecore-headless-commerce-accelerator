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

namespace Wooli.Foundation.Commerce.Repositories
{
    using Connect.Context;
    using Connect.Managers;

    using Context;

    using DependencyInjection;

    using Glass.Mapper.Sc;

    using Models.Catalog;

    using Providers;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ICatalogRepository), Lifetime = Lifetime.Singleton)]
    public class CatalogRepository : BaseCatalogRepository, ICatalogRepository
    {
        private readonly ISearchManager searchManager;

        public CatalogRepository(
            ISiteContext siteContext,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            ISitecoreService sitecoreService,
            ISearchManager searchManager,
            ICurrencyProvider currencyProvider)
            : base(currencyProvider, siteContext, storefrontContext, visitorContext, sitecoreService)
        {
            this.searchManager = searchManager;
        }

        public CategoryModel GetCurrentCategory()
        {
            var categoryItem = this.SiteContext.CurrentCategoryItem;

            return this.GetCategoryModel(categoryItem);
        }
        
        public ProductModel GetProduct(string productId)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));

            var productItem = this.searchManager.GetProduct(this.StorefrontContext.CatalogName, productId);
            if (productItem != null)
            {
                return this.GetProductModel(this.VisitorContext, productItem);
            }

            return null;
        }

        public ProductModel GetProduct(Item productItem)
        {
            Assert.ArgumentNotNull(productItem, nameof(productItem));

            return this.GetProductModel(this.VisitorContext, productItem);
        }
    }
}