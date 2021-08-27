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

namespace HCA.Foundation.Search.Services.Category
{
    using System.Linq;

    using Base.Context;
    
    using DependencyInjection;

    using Loaders;

    using Mappers;

    using Models.Common;
    using Models.Entities.Category;
    
    using Providers.Product;
    
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.Diagnostics;
    
    public class CommerceCategorySearchService : ICategorySearchService
    {
        private readonly ISitecoreContext sitecoreContext;

        private readonly ISearchMapper searchMapper;

        private readonly IProductSearchResultProvider searchResultProvider;

        public CommerceCategorySearchService(
            ISearchMapper searchMapper,
            IProductSearchResultProvider searchResultProvider,
            ISitecoreContext sitecoreContext)
        {
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));
            Assert.ArgumentNotNull(searchResultProvider, nameof(searchResultProvider));
            Assert.ArgumentNotNull(sitecoreContext, nameof(sitecoreContext));
            
            this.searchMapper = searchMapper;
            this.searchResultProvider = searchResultProvider;
            this.sitecoreContext = sitecoreContext;
        }
        
        public SearchResults<CategorySearchResultItem> GetSearchResults(CategorySearchOptions options)
        {
            Assert.ArgumentNotNull(options, nameof(options));

            var results = this.searchResultProvider.GetSearchResults<CommerceSellableItemSearchResultItem>(
                queryable =>
                {
                    return queryable
                        .Where(item => item.CommerceSearchItemType == Constants.Search.ItemType.Category)
                        .Where(item => item.Language == this.sitecoreContext.Language.Name)
                        .Where(item => item.Name == options.CategoryName.ToLowerInvariant());
                });

            return this.searchMapper
                .Map<Sitecore.ContentSearch.Linq.SearchResults<CommerceSellableItemSearchResultItem>,
                    SearchResults<CategorySearchResultItem>>(results);
        }
    }
}