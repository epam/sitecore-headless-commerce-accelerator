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

namespace HCA.Foundation.Commerce.Infrastructure.ContentsResolvers
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Connect.Context.Storefront;
    using Connect.Models;

    using DependencyInjection;

    using Mappers.Region;

    using Models.Entities.Region;

    using Sitecore.LayoutService.Configuration;
    using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
    using Sitecore.Mvc.Presentation;

    // TODO: Remove redundant code, refactoring
    [Service(Lifetime = Lifetime.Transient)]
    public class StorefrontCountriesContentsResolver : IRenderingContentsResolver
    {
        private readonly IRegionMapper mapper;

        private readonly IStorefrontContext storefrontContext;

        public StorefrontCountriesContentsResolver(IStorefrontContext storefrontContext, IRegionMapper mapper)
        {
            this.storefrontContext = storefrontContext;
            this.mapper = mapper;
        }

        public bool IncludeServerUrlInMediaUrls { get; set; }

        public string ItemSelectorQuery { get; set; }

        public NameValueCollection Parameters { get; set; }

        public bool UseContextItem { get; set; }

        public object ResolveContents(Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            var settings = this.storefrontContext.StorefrontConfiguration.CountriesRegionsSettings.CountryRegionsValues;

            return new
            {
                Countries = this.mapper.Map<IEnumerable<CountryRegionModel>, IEnumerable<CountryRegion>>(settings)
            };
        }
    }
}