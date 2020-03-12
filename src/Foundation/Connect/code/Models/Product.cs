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
    using System;
    using System.Collections.Generic;

    using Sitecore.Data.Items;

    public class Product : BaseProduct
    {
        public Product(Item item, List<Variant> variants, string catalogName = null, decimal? customerAverageRating = null) 
            : base(item, catalogName, customerAverageRating)
        {
            this.Variants = variants;
        }

        [Obsolete("Use Id property")]
        public string ProductId => this.Id;

        public List<Variant> Variants { get; protected set; }
    }
}