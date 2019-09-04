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

namespace Wooli.Foundation.Connect.Managers
{
    using System.Collections.Generic;
    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Services.Inventory;
    using Sitecore.Diagnostics;

    using Wooli.Foundation.Connect.Providers;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(IInventoryManager))]
    public class InventoryManager : IInventoryManager
    {
        private readonly InventoryServiceProvider inventoryServiceProvider;

        public InventoryManager(IConnectServiceProvider connectServiceProvider)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            this.inventoryServiceProvider = connectServiceProvider.GetInventoryServiceProvider();
        }

        public ManagerResponse<GetStockInformationResult, IEnumerable<StockInformation>> GetStockInformation(
            string shopName,
            IEnumerable<CommerceInventoryProduct> inventoryProducts,
            StockDetailsLevel detailsLevel)
        {
            Assert.ArgumentNotNull(inventoryProducts, nameof(inventoryProducts));

            var request = new GetStockInformationRequest(shopName, inventoryProducts, detailsLevel);
            request.Location = string.Empty;
           
            GetStockInformationResult stockInformation = this.inventoryServiceProvider.GetStockInformation(request);

            return new ManagerResponse<GetStockInformationResult, IEnumerable<StockInformation>>(stockInformation, stockInformation.StockInformation ?? new List<StockInformation>());
        }
    }
}
