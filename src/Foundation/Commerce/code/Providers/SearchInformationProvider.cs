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
    using System.Linq;

    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Data.Items;

    using Wooli.Foundation.Commerce.Models.Catalog;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(ISearchInformationProvider))]
    public class SearchInformationProvider : ISearchInformationProvider
    {
        private readonly ICommerceSearchManager commerceSearchManager;

        public SearchInformationProvider()
        {
            this.commerceSearchManager = CommerceTypeLoader.CreateInstance<ICommerceSearchManager>();
        }

        // ToDo: update this logic if required
        public CategorySearchInformation GetCategorySearchInformation(Item categoryItem)
        {
            var commerceQueryFacets = this.commerceSearchManager.GetFacetFieldsForItem(categoryItem).ToList();
            var commerceQuerySorts = this.commerceSearchManager.GetSortFieldsForItem(categoryItem).ToList();
            int itemsPerPageForItem = this.commerceSearchManager.GetItemsPerPageForItem(categoryItem);

            var searchInformation = new CategorySearchInformation
                                    {
                                        ItemsPerPage = itemsPerPageForItem,
                                        RequiredFacets = commerceQueryFacets,
                                        SortFields = commerceQuerySorts
                                    };

            return searchInformation;
        }
    }
}