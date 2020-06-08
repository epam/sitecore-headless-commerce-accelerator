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

namespace HCA.Foundation.Connect.Context.Catalog
{
    using System.Linq;

    using DependencyInjection;

    using Glass.Mapper.Sc;

    using Models;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ICatalogContext), Lifetime = Lifetime.Transient)]
    public class CatalogContext : ICatalogContext
    {
        private const string CatalogFolderPath = "/sitecore/Commerce/Catalog Management/Catalogs";

        private readonly ISitecoreService sitecoreService;

        public CatalogContext(ISitecoreService sitecoreService)
        {
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));

            this.sitecoreService = sitecoreService;
        }

        // It is assumed that we have only one selected catalog
        public string CatalogName => this.GetCatalogFolder()?.SelectedCatalogs.FirstOrDefault();

        // It is assumed that we have only one selected catalog
        public Item CatalogItem
        {
            get
            {
                var catalogFolder = this.GetCatalogFolder();
                var catalogName = catalogFolder?.SelectedCatalogs.FirstOrDefault();

                return !string.IsNullOrEmpty(catalogName)
                    ? catalogFolder.Item.Children.FirstOrDefault(child => child.Name == catalogName)
                    : null;
            }
        }

        private CommerceCatalogFolderModel GetCatalogFolder()
        {
            return this.sitecoreService.GetItem<CommerceCatalogFolderModel>(CatalogFolderPath);
        }
    }
}