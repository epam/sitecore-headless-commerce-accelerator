//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Commerce.Providers.Search
{
    using System.Linq;

    using Connect.Context.Catalog;

    using DependencyInjection;

    using Foundation.Search.Models.Common;
    using Foundation.Search.Providers;

    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchSettingsProvider))]
    public class SearchSettingsProvider : ISearchSettingsProvider
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly ICatalogContext catalogContext;

        public SearchSettingsProvider(ICatalogContext catalogContext)
        {
            Assert.ArgumentNotNull(catalogContext, nameof(catalogContext));
            this.catalogContext = catalogContext;

            this.commerceSearchManager = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }
        
        public SearchSettings GetSearchSettings(Item settingsItem)
        {
            var catalog = settingsItem ?? this.catalogContext.CatalogItem;

            return catalog != null
                ? new SearchSettings
                {
                    SortFieldNames =
                        this.commerceSearchManager.GetSortFieldsForItem(catalog)?.Select(field => field.Name),
                    ItemsPerPage = this.commerceSearchManager.GetItemsPerPageForItem(catalog),
                    Facets = this.commerceSearchManager.GetFacetFieldsForItem(catalog)
                        ?.Select(
                            facet => new Facet
                            {
                                Name = facet.Name,
                                DisplayName = facet.DisplayName,
                                Values = facet.Values
                            })
                } : null;
        }
    }
}