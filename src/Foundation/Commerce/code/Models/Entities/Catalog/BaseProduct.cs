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

namespace HCA.Foundation.Commerce.Models.Entities.Catalog
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class BaseProduct
    {
        public string ProductId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> ImageUrls { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal? AdjustedPrice { get; set; }

        public string StockStatusName { get; set; }

        public decimal? CustomerAverageRating { get; set; }
    }
}