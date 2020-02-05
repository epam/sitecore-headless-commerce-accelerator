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
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Entities.Shipping;
using Sitecore.Commerce.Services.Shipping;
using Wooli.Foundation.Connect.Models;

namespace Wooli.Foundation.Connect.Managers
{
    public interface IShippingManager
    {
        ManagerResponse<GetShippingMethodsResult, IReadOnlyCollection<ShippingMethod>> GetShippingMethods(
            string shopName, Cart cart, ShippingOptionType shippingOptionType, PartyEntity address,
            List<string> cartLineExternalIdList);

        ManagerResponse<GetShippingOptionsResult, List<ShippingOption>> GetShippingPreferences(Cart cart);
    }
}