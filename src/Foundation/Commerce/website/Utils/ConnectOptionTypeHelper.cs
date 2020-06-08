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

namespace HCA.Foundation.Commerce.Utils
{
    using System;
    using System.Globalization;

    using Sitecore.Commerce.Entities.Shipping;

    public static class ConnectOptionTypeHelper
    {
        public static ShippingOptionType ToShippingOptionType(string optionTypeName)
        {
            if (string.Equals(
                optionTypeName,
                ShippingOptionType.ShipToAddress.Value.ToString(CultureInfo.InvariantCulture),
                StringComparison.OrdinalIgnoreCase))
            {
                return ShippingOptionType.ShipToAddress;
            }

            if (string.Equals(
                optionTypeName,
                ShippingOptionType.PickupFromStore.Value.ToString(CultureInfo.InvariantCulture),
                StringComparison.OrdinalIgnoreCase))
            {
                return ShippingOptionType.PickupFromStore;
            }

            if (string.Equals(
                optionTypeName,
                ShippingOptionType.ElectronicDelivery.Value.ToString(CultureInfo.InvariantCulture),
                StringComparison.OrdinalIgnoreCase))
            {
                return ShippingOptionType.ElectronicDelivery;
            }

            if (string.Equals(
                optionTypeName,
                ShippingOptionType.DeliverItemsIndividually.Value.ToString(CultureInfo.InvariantCulture),
                StringComparison.OrdinalIgnoreCase))
            {
                return ShippingOptionType.DeliverItemsIndividually;
            }

            return ShippingOptionType.None;
        }
    }
}