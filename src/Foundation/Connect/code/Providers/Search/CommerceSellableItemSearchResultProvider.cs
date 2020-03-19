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

namespace Wooli.Foundation.Connect.Providers.Search
{
    using Context;

    using DependencyInjection;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.Commerce.Engine.Connect.Search;
    using Sitecore.ContentSearch;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchResultProvider<CommerceSellableItemSearchResultItem>), Lifetime = Lifetime.Singleton)]
    public sealed class
        CommerceSellableItemSearchResultProvider : SearchResultProvider<CommerceSellableItemSearchResultItem>
    {
        public CommerceSellableItemSearchResultProvider(
            ICommerceSearchManager commerceSearchManager,
            IStorefrontContext context)
        {
            Assert.ArgumentNotNull(commerceSearchManager, nameof(commerceSearchManager));
            Assert.ArgumentNotNull(context, nameof(context));

            this.SearchIndex = commerceSearchManager.GetIndex(context.CatalogName);
        }

        protected override ISearchIndex SearchIndex { get; set; }
    }
}