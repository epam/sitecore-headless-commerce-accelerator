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

namespace HCA.Feature.StoreLocator.Services
{
    using HCA.Feature.StoreLocator.Providers;
    using HCA.Foundation.Base.Models.Result;
    using HCA.Foundation.Commerce.Models.Entities.Geolocation;
    using HCA.Foundation.Commerce.Services.Geolocation;
    using HCA.Foundation.DependencyInjection;

    using Sitecore.Diagnostics;

    [Service(typeof(IStoreLocatorService), Lifetime = Lifetime.Singleton)]
    public class StoreLocatorService : IStoreLocatorService
    {
        private readonly IGeolocationService geolocationService;
        private readonly IStoreLocatorProvider storeLocatorProvider;

        public StoreLocatorService(
            IGeolocationService geolocationService,
            IStoreLocatorProvider storeLocatorProvider)
        {
            Assert.ArgumentNotNull(geolocationService, nameof(geolocationService));
            Assert.ArgumentNotNull(storeLocatorProvider, nameof(storeLocatorProvider));

            this.geolocationService = geolocationService;
            this.storeLocatorProvider = storeLocatorProvider;
        }

        public Result<SearchByGeolocationResult> FindStoresByGeolocation(
            double latitude,
            double longitude,
            double radius)
        {
            var storeLocator = this.storeLocatorProvider.GetLocator();

            var unitCode = storeLocator?.SelectedUnitOfLength?.UnitCode;

            return this.geolocationService
                .Find(
                    latitude,
                    longitude,
                    radius,
                    unitCode,
                    storeLocator.Stores);
        }

        public Result<SearchByGeolocationResult> GetStores()
        {
            var storeLocator = this.storeLocatorProvider.GetLocator();
            return this.geolocationService
                .GetAll(
                    storeLocator.Stores);
        }
    }
}