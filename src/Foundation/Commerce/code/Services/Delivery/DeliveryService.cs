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

namespace Wooli.Foundation.Commerce.Services.Delivery
{
    using System;
    using System.Collections.Generic;

    using DependencyInjection;

    using Models;
    using Models.Entities;
    using Models.Entities.Addresses;
    using Models.Entities.Delivery;
    using Models.Entities.Shipping;

    [Service(typeof(IDeliveryService), Lifetime = Lifetime.Singleton)]
    public class DeliveryService : IDeliveryService
    {
        public Result<DeliveryInfo> GetDeliveryOptions()
        {
            throw new NotImplementedException();
        }

        public Result<ShippingInfo> GetShippingOptions()
        {
            throw new NotImplementedException();
        }

        public Result<VoidResult> SetShippingOptions(
            string shippingPreferenceType,
            List<Address> shippingAddresses,
            List<ShippingMethod> shippingMethods)
        {
            throw new NotImplementedException();
        }
    }
}