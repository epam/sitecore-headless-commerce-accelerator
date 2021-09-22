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

namespace HCA.Foundation.Commerce.Converters.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DependencyInjection;

    using Foundation.Search.Providers;

    using Mappers.Search;

    using Sitecore.Data;
    using Sitecore.Diagnostics;

    using Facet = Models.Entities.Search.Facet;
    using ProductSearchOptions = Models.Entities.Search.ProductSearchOptions;
    using Search = Foundation.Search.Models.Entities.Product;

    using SortDirection = Models.Entities.Search.SortDirection;

    /// <summary>
    /// Merges product search options with predefined search settings 
    /// </summary>
    [Service(typeof(ISearchOptionsConverter), Lifetime = Lifetime.Singleton)]
    public class SettingsSearchOptionsConverter : ISearchOptionsConverter
    {
        private readonly ISearchMapper searchMapper;
        private readonly ISearchSettingsProvider searchSettingsProvider;

        public SettingsSearchOptionsConverter(ISearchMapper searchMapper, ISearchSettingsProvider searchSettingsProvider)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchSettingsProvider, nameof(searchSettingsProvider));

            this.searchMapper = searchMapper;
            this.searchSettingsProvider = searchSettingsProvider;
        }
        
        public Search.ProductSearchOptions Convert(ProductSearchOptions searchOptions)
        {
            Assert.ArgumentNotNull(searchOptions, nameof(searchOptions));

            var settingsItem = searchOptions.CategoryId != Guid.Empty
                ? Sitecore.Context.Database.GetItem(new ID(searchOptions.CategoryId))
                : null;
            var searchSettings = this.searchSettingsProvider.GetSearchSettings(settingsItem);

            if (!string.IsNullOrWhiteSpace(searchOptions.SortField)
                && searchSettings != null && !searchSettings.SortFieldNames.Contains(
                    searchOptions.SortField,
                    StringComparer.InvariantCultureIgnoreCase))
            {
                throw new Exception("Sort field not found");
            }

            return new Search.ProductSearchOptions
            {
                SearchKeyword = searchOptions.SearchKeyword,
                Facets = this.GetFacetsIntersection(searchSettings?.Facets, searchOptions.Facets),
                StartPageIndex = searchOptions.PageNumber,
                NumberOfItemsToReturn =
                    searchOptions.PageSize > 0 ? searchOptions.PageSize : searchSettings?.ItemsPerPage ?? 1,
                CategoryId = searchOptions.CategoryId,
                SortField = !string.IsNullOrEmpty(searchOptions.SortField)
                    ? searchOptions.SortField
                    : searchSettings?.SortFieldNames?.FirstOrDefault(),
                SortDirection = searchOptions.SortDirection == SortDirection.Asc
                    ? Foundation.Search.Models.Common.SortDirection.Asc
                    : Foundation.Search.Models.Common.SortDirection.Desc
            };
        }

        private IEnumerable<Foundation.Search.Models.Common.Facet> GetFacetsIntersection(
            IEnumerable<Foundation.Search.Models.Common.Facet> searchSettingsFacets,
            IEnumerable<Facet> searchOptionsFacets)
        {
            if (searchOptionsFacets != null)
            {
                if (searchSettingsFacets == null)
                {
                    return this.searchMapper
                        .Map<IEnumerable<Facet>, IEnumerable<Foundation.Search.Models.Common.Facet>>(
                            searchOptionsFacets);
                }

                if (searchOptionsFacets == null || !searchOptionsFacets.Any())
                {
                    return searchSettingsFacets;
                }

                var facets = searchOptionsFacets.Where(
                    searchOptionsFacet =>
                    {
                        var searchSettingsFacet =
                            searchSettingsFacets.FirstOrDefault(facet => facet.Name == searchOptionsFacet.Name);
                        if (searchSettingsFacet == null)
                        {
                            return false;
                        }

                        searchOptionsFacet.DisplayName = searchSettingsFacet.DisplayName;
                        return true;
                    });

                return this.searchMapper.Map<IEnumerable<Facet>, IEnumerable<Foundation.Search.Models.Common.Facet>>(
                    facets);
            }

            return new List<Foundation.Search.Models.Common.Facet>();
        }
    }
}