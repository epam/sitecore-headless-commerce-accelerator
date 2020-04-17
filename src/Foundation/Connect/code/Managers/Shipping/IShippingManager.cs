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

namespace HCA.Foundation.Connect.Managers.Shipping
{
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Shipping;

    /// <summary>
    /// Executes ShippingServiceProvider methods
    /// </summary>
    public interface IShippingManager
    {
        /// <summary>
        /// Gets shipping methods
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="shippingOptionType">Shipping option type</param>
        /// <returns>Get shipping methods result</returns>
        GetShippingMethodsResult GetShippingMethods(
            Cart cart,
            ShippingOptionType shippingOptionType);

        /// <summary>
        /// Gets shipping options
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <returns>Get shipping options result</returns>
        GetShippingOptionsResult GetShippingOptions(Cart cart);
    }
}