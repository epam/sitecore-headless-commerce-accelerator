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

namespace HCA.Foundation.Connect.Context.Storefront
{
    using DependencyInjection;

    using Glass.Mapper.Sc;

    using Models;

    using Providers;

    using Sitecore.Commerce.Multishop;
    using Sitecore.Commerce.Providers;
    using Sitecore.Diagnostics;

    [Service(typeof(IStorefrontContext), Lifetime = Lifetime.Transient)]
    public class StorefrontContext : IStorefrontContext
    {
        private readonly IConnectStorefrontContext connectStorefrontContext;
        private readonly IShopProvider shopProvider;
        private readonly ISitecoreService sitecoreService;

        public StorefrontContext(ISitecoreService sitecoreService, IConnectEntityProvider connectEntityProvider)
        {
            Assert.ArgumentNotNull(connectEntityProvider, nameof(connectEntityProvider));
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));

            this.sitecoreService = sitecoreService;
            this.shopProvider = connectEntityProvider.GetShopProvider();
            this.connectStorefrontContext = connectEntityProvider.GetConnectStorefrontContext();
        }

        public StorefrontModel StorefrontConfiguration
        {
            get
            {
                var storefrontConfigurationItem = this.connectStorefrontContext.StorefrontConfiguration;
                return this.sitecoreService.GetItem<StorefrontModel>(storefrontConfigurationItem);
            }
        }

        public string ShopName
        {
            get
            {
                var shop = this.shopProvider.GetShop();
                return shop.Name;
            }
        }
    }
}