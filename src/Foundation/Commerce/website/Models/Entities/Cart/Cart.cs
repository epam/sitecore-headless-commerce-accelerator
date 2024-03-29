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

namespace HCA.Foundation.Commerce.Models.Entities.Cart
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Addresses;
    using HCA.Foundation.Commerce.Models.Entities.Adjustments;
    using Payment;

    using Shipping;

    using TypeLite;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class Cart
    {
        public string Id { get; set; }

        public string Email { get; internal set; }

        public TotalPrice Price { get; set; }

        public IList<CartLine> CartLines { get; set; }

        public IList<Address> Addresses { get; set; }

        public IList<Adjustment> Adjustments { get; set; }

        public IList<ShippingMethod> Shipping { get; set; }

        public IList<FederatedPaymentInfo> Payment { get; set; }
    }
}