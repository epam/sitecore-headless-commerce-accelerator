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

namespace Wooli.Foundation.Commerce.Services.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models;

    using Connect.Managers.Search;
    using Connect.Models;

    using DependencyInjection;

    using Mappers.Search;

    using Models;
    using Models.Entities.Search;

    using Providers.Search;

    using Connect = Connect.Models.Search;

    [Service(typeof(IProductSearchService), Lifetime = Lifetime.Singleton)]
    public class ProductSearchService : IProductSearchService
    {
        private readonly ISearchManagerV2 searchManager;

        private readonly ISearchMapper searchMapper;
        private readonly ISearchSettingsProvider searchSettingsProvider;

        public ProductSearchService(
            ISearchSettingsProvider searchSettingsProvider,
            ISearchManagerV2 searchManager,
            ISearchMapper searchMapper)
        {
            this.searchSettingsProvider = searchSettingsProvider;
            this.searchManager = searchManager;
            this.searchMapper = searchMapper;
        }

        public Result<ProductSearchResults> GetProducts(ProductSearchOptions productSearchOptions)
        {
            var searchSettings = this.searchSettingsProvider.GetSearchSettings();

            //TODO: create a search options builder/mapper and refactor
            var searchOptions = this.ApplyDefaultSearchSettings(searchSettings, productSearchOptions);
            searchOptions = this.ApplySearchKeyword(searchOptions, productSearchOptions.SearchKeyword);
            searchOptions = this.ApplyFacets(searchOptions, productSearchOptions.Facets);
            searchOptions = this.ApplySorting(
                searchOptions,
                productSearchOptions.SortDirection,
                productSearchOptions.SortField);
            //

            var searchResults = this.searchManager.GetProducts(searchOptions);

            return new Result<ProductSearchResults>(
                this.searchMapper.Map<Connect.SearchResultsV2<Product>, ProductSearchResults>(searchResults));
        }

        private Connect.SearchOptions ApplyDefaultSearchSettings(
            SearchSettings searchSettings,
            ProductSearchOptions searchOptions)
        {
            var pageSize = searchOptions.PageSize > 0 ? searchOptions.PageSize : searchSettings.ItemsPerPage;
            var startPageIndex = (searchOptions.PageNumber - 1) * pageSize;

            return new Connect.SearchOptions
            {
                SortField = string.IsNullOrEmpty(searchOptions.SortField)
                    ? searchSettings.SortFieldNames?.FirstOrDefault()
                    : searchOptions.SortField,
                StartPageIndex = startPageIndex >= 0 ? startPageIndex : 0,
                NumberOfItemsToReturn = pageSize
            };
        }

        private Connect.SearchOptions ApplySearchKeyword(Connect.SearchOptions searchOptions, string searchKeyword)
        {
            searchOptions.SearchKeyword = searchKeyword;
            return searchOptions;
        }

        private Connect.SearchOptions ApplySorting(
            Connect.SearchOptions searchOptions,
            SortDirection sortDirection,
            string sortField)
        {
            searchOptions.SortDirection = sortDirection == SortDirection.Asc
                ? Connect.SortDirection.Asc
                : Connect.SortDirection.Desc;
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                searchOptions.SortField = sortField;
            }

            return searchOptions;
        }

        private Connect.SearchOptions ApplyFacets(Connect.SearchOptions searchOptions, IEnumerable<Facet> facets)
        {
            searchOptions.Facets = this.searchMapper.Map<IEnumerable<Facet>, IEnumerable<Connect.Facet>>(facets);
            return searchOptions;
        }
    }
}