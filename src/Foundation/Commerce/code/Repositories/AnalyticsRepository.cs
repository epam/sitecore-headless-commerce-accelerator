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

using Wooli.Foundation.Commerce.Context;
using Wooli.Foundation.Commerce.ModelMappers;
using Wooli.Foundation.Commerce.Models.Catalog;
using Wooli.Foundation.Connect.Managers;
using Wooli.Foundation.DependencyInjection;

namespace Wooli.Foundation.Commerce.Repositories
{
    [Service(typeof(IAnalyticsRepository), Lifetime = Lifetime.Singleton)]
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly IAnalyticsManager analyticsManager;
        private readonly IEntityMapper entityMapper;
        private readonly IStorefrontContext storefrontContext;
        private readonly IVisitorContext visitorContext;

        public AnalyticsRepository(
            IAnalyticsManager analyticsManager,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
        {
            this.analyticsManager = analyticsManager;
            this.entityMapper = entityMapper;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
        }

        public void RaiseProductVisitedEvent(ProductModel product)
        {
            analyticsManager.VisitedProductDetailsPage(storefrontContext.ShopName, product.SitecoreId,
                product.DisplayName);
        }

        public void RaiseCategoryVisitedEvent(CategoryModel category)
        {
            analyticsManager.VisitedCategoryPage(storefrontContext.ShopName, category.SitecoreId, category.Name);
        }
    }
}