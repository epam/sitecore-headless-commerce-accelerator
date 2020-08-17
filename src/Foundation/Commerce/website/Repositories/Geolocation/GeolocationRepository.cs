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

namespace HCA.Foundation.Commerce.Repositories.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc;

    using HCA.Foundation.Commerce.Mappers.Geolocation;
    using HCA.Foundation.Commerce.Models;
    using HCA.Foundation.Commerce.Models.Entities.Geolocation;
    using HCA.Foundation.DependencyInjection;

    using Sitecore.Diagnostics;

    [Service(typeof(IGeolocationRepository), Lifetime = Lifetime.Singleton)]
    public class GeolocationRepository : IGeolocationRepository
    {
        private readonly ISitecoreService sitecoreService;
        private readonly IGeolocationMapper geolocationMapper;

        public GeolocationRepository(ISitecoreService sitecoreService, IGeolocationMapper geolocationMapper)
        {
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));
            Assert.ArgumentNotNull(geolocationMapper, nameof(geolocationMapper));

            this.sitecoreService = sitecoreService;
            this.geolocationMapper = geolocationMapper;
        }

        public IEnumerable<GeolocationInfo> GetItems(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return Enumerable.Empty<GeolocationInfo>();
            }

            return ids.Select(id => this.sitecoreService.GetItem<GeolocationItem>(id))
                .Select(item => this.geolocationMapper.Map<GeolocationItem, GeolocationInfo>(item));
        }
    }
}