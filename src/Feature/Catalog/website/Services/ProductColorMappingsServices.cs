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

namespace HCA.Feature.Catalog.Services
{
    using System.Collections.Generic;

    using Foundation.Base.Providers.SiteSettings;
    using Foundation.DependencyInjection;

    using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

    using Models;

    using Sitecore.Data;

    [Service(typeof(IProductColorMappingsServices), Lifetime = Lifetime.Singleton)]
    public class ProductColorMappingsServices : IProductColorMappingsServices
    {
        private readonly ISiteSettingsProvider siteSettingsProvider;

        public ProductColorMappingsServices(ISiteSettingsProvider siteSettingsProvider)
        {
            this.siteSettingsProvider = siteSettingsProvider;
        }

        public Dictionary<string, string> GetProductColorMappings()
        {
            var productColorMapping =
                this.siteSettingsProvider.GetSetting<IProductColorMappingFolder>(
                    new ID(ProductColorMappingFolder.TemplateId));

            var dictionary = new Dictionary<string, string>();

            productColorMapping?.Mappings?.ForEach(
                colorMapping => dictionary.Add(colorMapping.ColorName, colorMapping.ColorHEX));

            return dictionary;
        }
    }
}