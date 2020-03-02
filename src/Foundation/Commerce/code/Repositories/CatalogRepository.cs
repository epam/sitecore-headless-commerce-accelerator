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

namespace Wooli.Foundation.Commerce.Repositories
{
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

        public CatalogRepository(ISiteContext siteContext, IStorefrontContext storefrontContext,
            IVisitorContext visitorContext, ICatalogManager catalogManager, ISitecoreService sitecoreService,
            ISearchManager searchManager, ICurrencyProvider currencyProvider)
            : base(currencyProvider, siteContext, storefrontContext, visitorContext, catalogManager, sitecoreService)
        {
            this.searchManager = searchManager;
        }

        public ProductModel GetProduct(string produced)
        {
            Assert.ArgumentNotNull(produced, nameof(produced));

            var productItem = searchManager.GetProduct(StorefrontContext.CatalogName, produced);
            if (productItem != null) return GetProductModel(VisitorContext, productItem);

            return null;
        }

        public ProductModel GetProduct(Item productItem)
        {
            Assert.ArgumentNotNull(productItem, nameof(productItem));

            return GetProductModel(VisitorContext, productItem);
        }

        public ProductModel GetCurrentProduct()
        {
            var productItem = SiteContext.CurrentProductItem;

            return GetProductModel(VisitorContext, productItem);
        }

        public CategoryModel GetCurrentCategory()
        {
            var categoryItem = SiteContext.CurrentCategoryItem;

            return GetCategoryModel(categoryItem);
        }
    }
}