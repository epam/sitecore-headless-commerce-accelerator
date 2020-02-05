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

using System.Collections.Generic;
using TypeLite;

namespace Wooli.Foundation.Commerce.Models.Checkout
{
    [TsClass]
    public class CartModel
    {
        public string Id { get; set; }

        public IList<CartLineModel> CartLines { get; set; }

        public CartPriceModel Price { get; set; }

        public IList<string> Adjustments { get; set; }

        public string Email { get; internal set; }

        public IList<AddressModel> Addresses { get; set; }

        public IList<FederatedPaymentModel> Payments { get; set; }

        public IList<ShippingMethodModel> Shippings { get; set; }

        [TsIgnore] public object Temp { get; set; }
    }
}