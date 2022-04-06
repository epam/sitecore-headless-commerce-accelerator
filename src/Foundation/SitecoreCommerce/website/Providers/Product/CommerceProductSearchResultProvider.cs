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

namespace HCA.Foundation.SitecoreCommerce.Providers.Product
{
    using Foundation.Search.Providers;
    using Foundation.Search.Providers.Product;

    using Loaders;

    using Sitecore.Commerce.Engine.Connect.Interfaces;
    using Sitecore.ContentSearch;
    using Sitecore.Diagnostics;

    public sealed class
        CommerceProductSearchResultProvider : SearchResultProvider, IProductSearchResultProvider
    {
        private ISearchIndex searchIndex;
        private readonly ICommerceTypeLoader commerceTypeLoader;

        public CommerceProductSearchResultProvider(ICommerceTypeLoader commerceTypeLoader)
        {
            Assert.ArgumentNotNull(commerceTypeLoader, nameof(commerceTypeLoader));
            this.commerceTypeLoader = commerceTypeLoader;
        }

        protected override ISearchIndex SearchIndex {
            get
            {
                return searchIndex ?? (searchIndex = commerceTypeLoader.CreateInstance<ICommerceSearchManager>()?.GetIndex());
            }
            set {
                searchIndex = value;
            }
        }
    }
}