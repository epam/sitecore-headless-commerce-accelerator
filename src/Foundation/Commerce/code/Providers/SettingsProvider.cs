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

namespace Wooli.Foundation.Commerce.Providers
{
    using Context;

    using DependencyInjection;

    using Models.Catalog;

    [Service(typeof(ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IStorefrontContext storefrontContext;

        public SettingsProvider(IStorefrontContext storefrontContext)
        {
            this.storefrontContext = storefrontContext;
        }

        public int GetDefaultItemsPerPage(int? pageSize, CategorySearchInformation searchInformation)
        {
            var defaultValue = this.storefrontContext.DefaultItemsPerPage;
            if (defaultValue <= 0)
            {
                defaultValue = searchInformation.ItemsPerPage;
            }

            if (defaultValue <= 0)
            {
                defaultValue = 12;
            }

            return pageSize.GetValueOrDefault(defaultValue);
        }
    }
}