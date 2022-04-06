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

namespace HCA.Foundation.Base.Providers.SiteDataSources
{
    using System.Linq;

    using DependencyInjection;

    using Extensions;

    using HCA.Foundation.Base.Models;

    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using SiteDefinitions;

    [Service(typeof(IDataSourcesProvider), Lifetime = Lifetime.Singleton)]
    public class DataSourcesProvider : IDataSourcesProvider
    {
        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        public DataSourcesProvider(ISiteDefinitionsProvider siteDefinitionsProvider)
        {
            Assert.ArgumentNotNull(siteDefinitionsProvider, nameof(siteDefinitionsProvider));

            this.siteDefinitionsProvider = siteDefinitionsProvider;
        }

        public Item GetDataSourcesRootFolder()
        {
            var siteDefinition = this.siteDefinitionsProvider.GetCurrentSiteDefinition();

            if (siteDefinition == null)
            {
                return default(Item);
            }

            var definitionItem = siteDefinition.RootItem;

            var result = this.GetFirstChildOrDefault(definitionItem, SiteDataSources.TemplateId);  

            return result;
        }

        public Item GetFirstChildOrDefault(Item ancestor, string childTemplateId)
        {
            return ancestor
                ?.Children
                ?.FirstOrDefault(item => item.IsDerived(new ID(childTemplateId)));
        }
    }
}