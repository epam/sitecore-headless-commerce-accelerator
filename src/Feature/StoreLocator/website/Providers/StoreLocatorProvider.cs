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

namespace HCA.Feature.StoreLocator.Providers
{
    using Glass.Mapper.Sc;

    using HCA.Feature.StoreLocator.Models;
    using HCA.Foundation.Base.Providers.SiteDataSources;
    using HCA.Foundation.DependencyInjection;
    
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
 
    [Service(typeof(IStoreLocatorProvider), Lifetime = Lifetime.Singleton)]
    public class StoreLocatorProvider : IStoreLocatorProvider
    {
        private readonly ISitecoreService sitecoreService;
        private readonly IDataSourcesProvider datasourceProvider;

        public StoreLocatorProvider(IDataSourcesProvider dataSourceProvider, ISitecoreService sitecoreService)
        {
            Assert.ArgumentNotNull(dataSourceProvider, nameof(dataSourceProvider));
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));

            this.sitecoreService = sitecoreService;
            this.datasourceProvider = dataSourceProvider;
        }

        public IStoreLocator GetLocator()
        {
            var locatorItem = this.GetLocatorItem();

            return locatorItem != null 
                ? this.sitecoreService.GetItem<IStoreLocator>(locatorItem)
                : default(IStoreLocator);
        }

        private Item GetLocatorItem()
        {
            var datasourceRootFolder = this.datasourceProvider
                .GetDataSourcesRootFolder();

            var locatorFolder = this.datasourceProvider
                .GetFirstChildOrDefault(datasourceRootFolder, StoreLocatorFolder.TemplateId);

            var locatorItem = this.datasourceProvider
                .GetFirstChildOrDefault(locatorFolder, Models.StoreLocator.TemplateId);

            return locatorItem;
        }
    }
}