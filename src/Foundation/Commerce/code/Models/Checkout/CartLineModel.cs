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

namespace Wooli.Foundation.Commerce.Models.Checkout
{
    using System;
    using Catalog;
    using Entities;
    using TypeLite;

    [TsClass]
    [Obsolete("This model is obsolete. Use Commerce.Models.Entities.CartLine")]
    public class CartLineModel
    {
        public string Id { get; set; }

        public ProductModel Product { get; set; }

        public ProductVariantModel Variant { get; set; }

        public decimal Quantity { get; set; }

        public CartPriceModel Price { get; set; }

        [TsIgnore] public object Temp { get; set; }
    }
}