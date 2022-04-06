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

namespace HCA.Feature.Account.Models.Requests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class VariantRequest
    {
        [Required]
        public string VariantId { get; set; }

        public IDictionary<string, string> Properties { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
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