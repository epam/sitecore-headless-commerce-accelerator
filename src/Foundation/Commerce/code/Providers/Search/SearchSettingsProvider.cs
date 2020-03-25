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

namespace Wooli.Foundation.Commerce.Providers.Search
{
    using System.Linq;

    using Connect.Context;

    using DependencyInjection;

    using Models.Catalog;
    using Models.Entities.Search;

    using Sitecore.Commerce;
    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Data.Items;

    //TODO: move to Foundation.Connect
    [Service(typeof(ISearchSettingsProvider))]
    public class SearchSettingsProvider : ISearchSettingsProvider
    {
        private readonly ICommerceSearchManager commerceSearchManager;
        private readonly IStorefrontContext storefrontContext;

        public SearchSettingsProvider(IStorefrontContext storefrontContext)
        {
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            this.storefrontContext = storefrontContext;

            this.commerceSearchManager = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>();
            Assert.ArgumentNotNull(this.commerceSearchManager, nameof(this.commerceSearchManager));
        }

        public CategorySearchInformation GetCategorySearchInformation(Item categoryItem)
        {
            var commerceQueryFacets = this.commerceSearchManager.GetFacetFieldsForItem(categoryItem).ToList();
            var commerceQuerySorts = this.commerceSearchManager.GetSortFieldsForItem(categoryItem).ToList();
            var itemsPerPageForItem = this.commerceSearchManager.GetItemsPerPageForItem(categoryItem);

            var searchInformation = new CategorySearchInformation
            {
                ItemsPerPage = itemsPerPageForItem,
                RequiredFacets = commerceQueryFacets,
                SortFields = commerceQuerySorts
            };

            return searchInformation;
        }

        public SearchSettings GetSearchSettings()
        {
            var catalog = this.storefrontContext.CurrentCatalogItem;

            var searchPageSettings = new SearchSettings
            {
                SortFieldNames = this.commerceSearchManager.GetSortFieldsForItem(catalog)?.Select(field => field.Name),
                ItemsPerPage = this.commerceSearchManager.GetItemsPerPageForItem(catalog),
                Facets = this.commerceSearchManager.GetFacetFieldsForItem(catalog)?.Select(facet => new Facet
                {
                    Name = facet.Name,
                    DisplayName = facet.DisplayName,
                    Values = facet.Values
                })
            };

            return searchPageSettings;
        }
    }
}