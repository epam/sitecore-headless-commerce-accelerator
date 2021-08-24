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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines
{
    using System.Linq;

    using Base.Providers.SiteDefinitions;
    
    using Context;

    using DependencyInjection;

    using Providers;

    using Services.Search;
    using Services.Search.Commerce;

    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    
    [Service(typeof(ICatalogItemResolver))]
    public class CatalogItemResolver : ICatalogItemResolver
    {
        private readonly IPageTypeProvider pageTypeProvider;

        private readonly IProductSearchService productSearchService;

        private readonly ISiteContext siteContext;

        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        public CatalogItemResolver(
            IProductSearchService productSearchService,
            IPageTypeProvider pageTypeProvider,
            ISiteDefinitionsProvider siteDefinitionsProvider,
            ISiteContext siteContext)
        {
            this.productSearchService = productSearchService;
            this.pageTypeProvider = pageTypeProvider;
            this.siteDefinitionsProvider = siteDefinitionsProvider;
            this.siteContext = siteContext;
        }

        public void ProcessItemAndApplyContext(Item contextItem, string[] urlSegments)
        {
            Assert.ArgumentNotNull(contextItem, nameof(contextItem));

            if (urlSegments == null)
            {
                return;
            }

            var rootItem = this.siteDefinitionsProvider.GetCurrentSiteDefinition()?.RootItem;
            if (rootItem == null)
            {
                return;
            }

            if (this.siteContext.CurrentItem != null)
            {
                return;
            }

            var currentItem = Context.Item;
            while (currentItem != null && currentItem.ID != rootItem.ID)
            {
                var urlSegment = urlSegments.LastOrDefault()?.TrimEnd('/');

                this.ProcessItem(currentItem, urlSegment);

                if (urlSegments.Length > 0)
                {
                    urlSegments = urlSegments.Take(urlSegments.Length - 1).ToArray();
                }

                currentItem = currentItem.Parent;
            }
        }

        private void ProcessItem(Item item, string urlSegment)
        {
            var contextItemType = this.pageTypeProvider.ResolveByItem(item);
            if (contextItemType == Commerce.Constants.ItemType.Unknown)
            {
                return;
            }

            var itemName = item.Name != "*" ? item.Name : urlSegment;
            if (string.IsNullOrEmpty(itemName))
            {
                return;
            }

            Item currentItem;
            switch (contextItemType)
            {
                case Commerce.Constants.ItemType.Category:
                    currentItem = this.productSearchService.GetCategoryByName(itemName);
                    this.siteContext.CurrentCategoryItem = currentItem;
                    break;
                case Commerce.Constants.ItemType.Product:
                    currentItem = this.productSearchService.GetProductByName(itemName);
                    this.siteContext.CurrentProductItem = currentItem;
                    break;
                default:
                    return;
            }

            if (this.siteContext.CurrentItem == null)
            {
                this.siteContext.CurrentItem = currentItem;
            }
        }
    }
}