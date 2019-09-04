//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Commerce.Context
{
    using Glass.Mapper.Sc;

    using Wooli.Foundation.Connect;
    using Wooli.Foundation.Connect.Models;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(IStorefrontContext), Lifetime = Lifetime.Transient)]
    public class StorefrontContext : IStorefrontContext
    {
        private readonly ISitecoreContext sitecoreContext;

        public StorefrontContext(ISitecoreContext sitecoreContext)
        {
            this.sitecoreContext = sitecoreContext;
        }

        // ToDo: implement logic for getting current catalog
        public string CatalogName => "Habitat_Master";

        // ToDo: implement logic for getting current shop
        public string ShopName => "Wooli";

        // ToDo: implement logic for getting current storefront settings
        public IStorefrontModel CurrentStorefront 
            => this.sitecoreContext.GetItem<IStorefrontModel>($"/sitecore/Commerce/Commerce Control Panel/Storefront Settings/Storefronts/{this.ShopName}");

        // ToDo: implement logic for getting current catalog item
        public ICommerceCatalogModel CurrentCatalog 
            => this.sitecoreContext.GetItem<ICommerceCatalogModel>($"/sitecore/Commerce/Catalog Management/Catalogs/{this.CatalogName}");

        public string SelectedCurrency => "USD";

        // ToDo: implement logic for getting current catalog item
        public int DefaultItemsPerPage => 0;
    }
}
