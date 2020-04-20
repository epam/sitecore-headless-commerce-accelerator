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
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities.Inventory;
    using Sitecore.Data.Items;

    public class Product
    {
        public Product(Item item, List<Variant> variants)
        {
            this.Item = item;
            this.Variants = variants;
            this.ProductId = item.Name;
        }

        public decimal? AdjustedPrice { get; set; }

        public string CatalogName { get; set; }

        public string CurrencyCode { get; set; }

        public decimal? CustomerAverageRating { get; set; }

        public Item Item { get; set; }

        public decimal? ListPrice { get; set; }

        public string ProductId { get; set; }

        public StockStatus StockStatus { get; set; }

        public string StockStatusName { get; set; }

        public List<Variant> Variants { get; protected set; }
    }
}