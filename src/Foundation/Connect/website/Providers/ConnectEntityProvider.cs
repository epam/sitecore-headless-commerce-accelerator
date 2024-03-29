﻿//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Connect.Providers
{
    using Base.Providers.Object;

    using DependencyInjection;

    using Sitecore.Commerce.Multishop;
    using Sitecore.Commerce.Providers;
    using Sitecore.Diagnostics;

    [Service(typeof(IConnectEntityProvider), Lifetime = Lifetime.Singleton)]
    public class ConnectEntityProvider : IConnectEntityProvider
    {
        private readonly IObjectProvider objectProvider;

        public ConnectEntityProvider(IObjectProvider objectProvider)
        {
            Assert.ArgumentNotNull(objectProvider, nameof(objectProvider));

            this.objectProvider = objectProvider;
        }

        public IShopProvider GetShopProvider()
        {
            return this.GetEntity<IShopProvider>("shopProvider");
        }

        public IConnectStorefrontContext GetConnectStorefrontContext()
        {
            return this.GetEntity<IConnectStorefrontContext>("connectStorefrontContext");
        }

        private TEntity GetEntity<TEntity>(string entityName)
            where TEntity : class
        {
            return this.objectProvider.GetObject<TEntity>(entityName);
        }
    }
}