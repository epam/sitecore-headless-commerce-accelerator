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

namespace HCA.Foundation.Connect.Managers.Inventory
{
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Diagnostics;

    [Service(typeof(IInventoryManager), Lifetime = Lifetime.Singleton)]
    public class InventoryManager : BaseManager, IInventoryManager
    {
        private readonly InventoryServiceProvider inventoryServiceProvider;

        public InventoryManager(IConnectServiceProvider connectServiceProvider, ILogService<CommonLog> logService)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.inventoryServiceProvider = connectServiceProvider.GetInventoryServiceProvider();
        }

        public GetStockInformationResult GetStockInformation(
            string shopName,
            IEnumerable<CommerceInventoryProduct> inventoryProducts,
            StockDetailsLevel detailsLevel)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));
            Assert.ArgumentNotNull(inventoryProducts, nameof(inventoryProducts));
            Assert.ArgumentNotNull(detailsLevel, nameof(detailsLevel));

            var request = new GetStockInformationRequest(shopName, inventoryProducts, detailsLevel);

            return this.Execute(request, this.inventoryServiceProvider.GetStockInformation);
        }
    }
}