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

namespace Wooli.Foundation.Commerce.Services.Analytics
{
    using Connect.Managers;

    using Context;

    using DependencyInjection;

    using Models.Catalog;

    using Sitecore.Commerce;

    [Service(typeof(ICommerceAnalyticsService), Lifetime = Lifetime.Singleton)]
    public class CommerceAnalyticsService : ICommerceAnalyticsService
    {
        private readonly IAnalyticsManager analyticsManager;

        private readonly IStorefrontContext storefrontContext;

        public CommerceAnalyticsService(IAnalyticsManager analyticsManager, IStorefrontContext storefrontContext)
        {
            Assert.ArgumentNotNull(analyticsManager, nameof(analyticsManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));

            this.analyticsManager = analyticsManager;
            this.storefrontContext = storefrontContext;
        }

        public void RaiseCategoryVisitedEvent(CategoryModel category)
        {
            Assert.ArgumentNotNull(category, nameof(category));

            this.analyticsManager.VisitedCategoryPage(
                this.storefrontContext.ShopName,
                category.SitecoreId,
                category.Name);
        }

        public void RaiseProductVisitedEvent(ProductModel product)
        {
            Assert.ArgumentNotNull(product, nameof(product));

            this.analyticsManager.VisitedProductDetailsPage(
                this.storefrontContext.ShopName,
                product.SitecoreId,
                product.DisplayName);
        }
    }
}