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

namespace Wooli.Foundation.Connect.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using DependencyInjection;

    using ModelMappers;

    using Models;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Shipping;
    using Sitecore.Commerce.Services.Shipping.Generics;
    using Sitecore.Diagnostics;

    using GetShippingMethodsRequest = Sitecore.Commerce.Engine.Connect.Services.Shipping.GetShippingMethodsRequest;

    [Service(typeof(IShippingManager))]
    public class ShippingManager : IShippingManager
    {
        private readonly IConnectEntityMapper connectEntityMapper;

        private readonly ShippingServiceProvider shippingServiceProvider;

        public ShippingManager(IConnectServiceProvider connectServiceProvider, IConnectEntityMapper connectEntityMapper)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(connectEntityMapper, nameof(connectEntityMapper));
            this.connectEntityMapper = connectEntityMapper;
            this.shippingServiceProvider = connectServiceProvider.GetShippingServiceProvider();
        }

        public ManagerResponse<GetShippingMethodsResult, IReadOnlyCollection<ShippingMethod>> GetShippingMethods(
            string shopName,
            Cart cart,
            ShippingOptionType shippingOptionType,
            PartyEntity address,
            List<string> cartLineExternalIdList)
        {
            if ((cartLineExternalIdList != null) && cartLineExternalIdList.Any())
            {
            }

            CommerceParty commerceParty = null;
            if (address != null)
            {
                commerceParty = this.connectEntityMapper.MapToCommerceParty(address);
            }

            var shippingOption = new ShippingOption
            {
                ShippingOptionType = shippingOptionType
            };
            var request = new GetShippingMethodsRequest(shippingOption, commerceParty, cart as CommerceCart);
            var shippingMethods = this.shippingServiceProvider
                .GetShippingMethods<Sitecore.Commerce.Services.Shipping.GetShippingMethodsRequest, GetShippingMethodsResult>(
                    request);
            return new ManagerResponse<GetShippingMethodsResult, IReadOnlyCollection<ShippingMethod>>(
                shippingMethods,
                shippingMethods.ShippingMethods);
        }

        public virtual ManagerResponse<GetShippingOptionsResult, List<ShippingOption>> GetShippingPreferences(Cart cart)
        {
            var request = new GetShippingOptionsRequest(cart);
            var shippingOptions = this.shippingServiceProvider.GetShippingOptions(request);
            if (shippingOptions.Success && (shippingOptions.ShippingOptions != null))
            {
                return new ManagerResponse<GetShippingOptionsResult, List<ShippingOption>>(
                    shippingOptions,
                    shippingOptions.ShippingOptions.ToList());
            }

            return new ManagerResponse<GetShippingOptionsResult, List<ShippingOption>>(shippingOptions, null);
        }
    }
}