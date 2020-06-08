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

namespace HCA.Foundation.Commerce.Services.Analytics
{
    using System.Linq;

    using Base.Models.Result;

    using Connect.Context.Storefront;
    using Connect.Managers.Analytics;

    using DependencyInjection;

    using Models.Entities.Catalog;

    using Sitecore.Diagnostics;

    [Service(typeof(IAnalyticsService), Lifetime = Lifetime.Singleton)]
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsManager analyticsManager;

        private readonly IStorefrontContext storefrontContext;

        public AnalyticsService(IAnalyticsManager analyticsManager, IStorefrontContext storefrontContext)
        {
            Assert.ArgumentNotNull(analyticsManager, nameof(analyticsManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));

            this.analyticsManager = analyticsManager;
            this.storefrontContext = storefrontContext;
        }

        public Result<VoidResult> RaiseCategoryVisitedEvent(Category category)
        {
            Assert.ArgumentNotNull(category, nameof(category));

            var result = new Result<VoidResult>();

            var catalogResult = this.analyticsManager.VisitedCategoryPage(
                this.storefrontContext.ShopName,
                category.SitecoreId,
                category.Name);

            if (!catalogResult.Success)
            {
                result.SetErrors(catalogResult.SystemMessages.Select(m => m.Message).ToList());
            }

            return result;
        }

        public Result<VoidResult> RaiseProductVisitedEvent(Product product)
        {
            Assert.ArgumentNotNull(product, nameof(product));

            var result = new Result<VoidResult>();

            var catalogResult = this.analyticsManager.VisitedProductDetailsPage(
                this.storefrontContext.ShopName,
                product.SitecoreId,
                product.DisplayName);

            if (!catalogResult.Success)
            {
                result.SetErrors(catalogResult.SystemMessages.Select(m => m.Message).ToList());
            }

            return result;
        }

        public Result<VoidResult> RaiseSearchInitiatedEvent(string searchKeyword, int totalItemCount)
        {
            Assert.ArgumentNotNull(searchKeyword, nameof(searchKeyword));
            Assert.ArgumentNotNull(totalItemCount, nameof(totalItemCount));

            var result = new Result<VoidResult>();

            var catalogResult = this.analyticsManager.SearchInitiated(
                this.storefrontContext.ShopName,
                searchKeyword,
                totalItemCount);

            if (!catalogResult.Success)
            {
                result.SetErrors(catalogResult.SystemMessages.Select(m => m.Message).ToList());
            }

            return result;
        }
    }
}