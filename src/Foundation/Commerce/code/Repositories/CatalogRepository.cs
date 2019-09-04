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
    using Glass.Mapper.Sc;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using Wooli.Foundation.Commerce.Context;
    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Commerce.Providers;
    using Wooli.Foundation.Connect.Managers;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(ICatalogRepository), Lifetime = Lifetime.Singleton)]
    public class CatalogRepository : BaseCatalogRepository, ICatalogRepository
    {
        private readonly ISearchManager searchManager;

        public CatalogRepository(ISiteContext siteContext, IStorefrontContext storefrontContext, IVisitorContext visitorContext, ICatalogManager catalogManager, ISitecoreContext sitecoreContext, ISearchManager searchManager, ICurrencyProvider currencyProvider)
            : base(currencyProvider, siteContext, storefrontContext, visitorContext, catalogManager, sitecoreContext)
        {
            this.searchManager = searchManager;
        }

        public ProductModel GetProduct(string productd)
        {
            Assert.ArgumentNotNull(productd, nameof(productd));

            Item productItem = this.searchManager.GetProduct(this.StorefrontContext.CatalogName, productd);
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

        public ProductModel GetCurrentProduct()
        {
            Item productItem = this.SiteContext.CurrentProductItem;

            return this.GetProductModel(this.VisitorContext, productItem);
        }

        public CategoryModel GetCurrentCategory()
        {
            Item categoryItem = this.SiteContext.CurrentCategoryItem;

            return this.GetCategoryModel(categoryItem);
        }
    }
}
