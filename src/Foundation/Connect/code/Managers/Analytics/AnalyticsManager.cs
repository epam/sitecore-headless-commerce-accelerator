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

namespace HCA.Foundation.Connect.Managers.Analytics
{
    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Services.Catalog;
    using Sitecore.Diagnostics;

    [Service(typeof(IAnalyticsManager))]
    public class AnalyticsManager : BaseManager, IAnalyticsManager
    {
        private readonly CatalogServiceProvider catalogServiceProvider;

        public AnalyticsManager(
            ILogService<CommonLog> logService,
            IConnectServiceProvider connectServiceProvider) : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            this.catalogServiceProvider = connectServiceProvider.GetCatalogServiceProvider();
        }

        public CatalogResult VisitedCategoryPage(string shopName, string categoryId, string categoryName)
        {
            Assert.IsNotNullOrEmpty(categoryId, nameof(categoryId));
            Assert.IsNotNullOrEmpty(categoryName, nameof(categoryName));

            return this.Execute(
                new VisitedCategoryPageRequest(shopName, categoryId, categoryName),
                this.catalogServiceProvider.VisitedCategoryPage);
        }

        public CatalogResult VisitedProductDetailsPage(string shopName, string productId, string productName)
        {
            Assert.IsNotNullOrEmpty(productId, nameof(productId));
            Assert.IsNotNullOrEmpty(productName, nameof(productName));

            return this.Execute(
                new VisitedProductDetailsPageRequest(shopName, productId, productName, null, null),
                this.catalogServiceProvider.VisitedProductDetailsPage);
        }

        public CatalogResult SearchInitiated(
            string shopName,
            string searchKeyword,
            int totalItemsCount)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, shopName);
            Assert.ArgumentNotNullOrEmpty(searchKeyword, nameof(searchKeyword));

            return this.Execute(
                new SearchInitiatedRequest(shopName, searchKeyword, totalItemsCount),
                this.catalogServiceProvider.SearchInitiated);
        }
    }
}