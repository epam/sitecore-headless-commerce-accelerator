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

namespace HCA.Foundation.Commerce.Services.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Providers.SiteSettings;

    using HCA.Foundation.Base.Models.Result;
    using HCA.Foundation.Commerce.Models.Entities.Geolocation;
    using HCA.Foundation.Commerce.Repositories.Geolocation;
    using HCA.Foundation.Commerce.Utils;
    using HCA.Foundation.DependencyInjection;

    using Models;

    using Sitecore.Data;
    using Sitecore.Diagnostics;

    [Service(typeof(IGeolocationService), Lifetime = Lifetime.Singleton)]
    public class GeolocationService : IGeolocationService
    {
        private readonly IGeolocationRepository geolocationRepository;
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public GeolocationService(IGeolocationRepository geolocationRepository, ISiteSettingsProvider siteSettingsProvider)
        {
            Assert.ArgumentNotNull(geolocationRepository, nameof(geolocationRepository));
            Assert.ArgumentNotNull(siteSettingsProvider, nameof(siteSettingsProvider));

            this.geolocationRepository = geolocationRepository;
            this.siteSettingsProvider = siteSettingsProvider;
        }

        public Result<SearchByGeolocationResult> GetAll(
            IEnumerable<Guid> selectedLocations)
        {
            var result = new SearchByGeolocationResult
            {
                Locations = this.geolocationRepository.GetItems(selectedLocations)
            };

            return new Result<SearchByGeolocationResult>(result);
        }

        public Result<SearchByGeolocationResult> Find(
            double latitude,
            double longitude,
            double radius,
            string unitOfLength,
            IEnumerable<Guid> selectedLocations)
        {
            var result = new SearchByGeolocationResult();

            var unitSettings = this.siteSettingsProvider
                .GetSetting<IGeolocationSettings>(ID.Parse(GeolocationSettings.TemplateId));

            var activeUnit = unitSettings?.AvailableUnits
                .FirstOrDefault(unit => unit.UnitCode.Equals(unitOfLength, StringComparison.InvariantCultureIgnoreCase));

            if (activeUnit == null)
            {
                return new Result<SearchByGeolocationResult>(
                    new SearchByGeolocationResult(),
                    new List<string> { $"{unitOfLength} - unknown unit of length" });
            }

            var availableLocations = this.geolocationRepository.GetItems(selectedLocations);
            
            var earthRadius = Constants.Geolocation.EarthRadiusInKilometers * activeUnit.KilometerToUnitMultiplier;

            result.Locations = availableLocations
                .Where(location => location.InRadius(latitude, longitude, radius, earthRadius));

            return new Result<SearchByGeolocationResult>(result);
        }
    }
}