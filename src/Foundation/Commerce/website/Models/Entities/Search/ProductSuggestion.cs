﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.Commerce.Models.Entities.Search
{
    using System.Diagnostics.CodeAnalysis;

    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class ProductSuggestion
    {
        public string ProductId { get; set; }

        public string DisplayName { get; set; }

        public string Brand { get; set; }

        public string ImageUrl { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? Price { get; set; }
    }
}