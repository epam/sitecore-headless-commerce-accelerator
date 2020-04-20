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

namespace HCA.Foundation.Commerce.Services.Delivery
{
    using System.Collections.Generic;

    using Base.Models.Result;

    using Models.Entities.Addresses;
    using Models.Entities.Delivery;
    using Models.Entities.Shipping;

    /// <summary>
    /// Performs main operations with delivery and shipping options
    /// </summary>
    public interface IDeliveryService
    {
        /// <summary>
        /// Gets delivery info
        /// </summary>
        /// <returns>Delivery info result</returns>
        Result<DeliveryInfo> GetDeliveryInfo();

        /// <summary>
        /// Gets shipping info
        /// </summary>
        /// <returns>Shipping info result</returns>
        Result<ShippingInfo> GetShippingInfo();

        /// <summary>
        /// Sets shipping options during checkout
        /// </summary>
        /// <param name="shippingPreferenceType">Preferred shipping type</param>
        /// <param name="shippingAddresses">List of shipping addresses</param>
        /// <param name="shippingMethods">Shipping methods</param>
        /// <returns></returns>
        Result<VoidResult> SetShippingOptions(
            string shippingPreferenceType,
            List<Address> shippingAddresses,
            List<ShippingMethod> shippingMethods);
    }
}