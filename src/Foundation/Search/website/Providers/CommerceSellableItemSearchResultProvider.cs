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

namespace HCA.Foundation.Search.Providers
{
    using DependencyInjection;

    using Loaders;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.ContentSearch;
    using Sitecore.Diagnostics;

    [Service(typeof(ISearchResultProvider), Lifetime = Lifetime.Singleton)]
    public sealed class
        CommerceSellableItemSearchResultProvider : SearchResultProvider
    {
        public CommerceSellableItemSearchResultProvider(ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));

            this.SearchIndex = commerceTypeLoader.CreateInstance<ICommerceSearchManager>()
                ?.GetIndex();
        }

        protected override ISearchIndex SearchIndex { get; set; }
    }
}