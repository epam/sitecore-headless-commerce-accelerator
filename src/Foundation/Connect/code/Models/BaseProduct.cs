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

namespace Wooli.Foundation.Connect.Models
{
    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Data.Items;

    public class BaseProduct
    {
        public BaseProduct(Item item, string catalogName = null, decimal? customerAverageRating = null)
        {
            this.Item = item;
            this.Id = item.Name;
            this.CatalogName = catalogName;
            this.CustomerAverageRating = customerAverageRating;
        }

        public decimal? AdjustedPrice { get; set; }

        public string CatalogName { get; set; }

        public string CurrencyCode { get; set; }

        public decimal? CustomerAverageRating { get; set; }

        public string Id { get; protected set; }

        public Item Item { get; set; }

        public decimal? ListPrice { get; set; }

        //TODO: Create Connect.StockStatus, map and fix usages
        public StockStatus StockStatus { get; set; }

        public string StockStatusName { get; set; }
    }
}