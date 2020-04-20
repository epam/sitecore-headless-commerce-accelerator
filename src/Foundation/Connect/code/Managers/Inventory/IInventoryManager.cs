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

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Commerce.Services.Inventory;

    /// <summary>
    /// Executes InventoryServiceProvider methods
    /// </summary>
    public interface IInventoryManager
    {
        /// <summary>
        /// Gets stock information about inventory products
        /// </summary>
        /// <param name="shopName">Commerce shop name</param>
        /// <param name="inventoryProducts">Inventory products</param>
        /// <param name="detailsLevel">Information details level</param>
        /// <returns></returns>
        GetStockInformationResult GetStockInformation(
            string shopName,
            IEnumerable<CommerceInventoryProduct> inventoryProducts,
            StockDetailsLevel detailsLevel);
    }
}