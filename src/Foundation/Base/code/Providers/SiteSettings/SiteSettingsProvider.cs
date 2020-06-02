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

namespace HCA.Foundation.Base.Providers.SiteSettings
{
    using System.Linq;

    using DependencyInjection;

    using Extensions;

    using Glass.Mapper.Sc;

    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using SiteDefinitions;

    [Service(typeof(ISiteSettingsProvider), Lifetime = Lifetime.Singleton)]
    public class SiteSettingsProvider : ISiteSettingsProvider
    {
        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        private readonly ISitecoreService sitecoreService;

        public SiteSettingsProvider(ISiteDefinitionsProvider siteDefinitionsProvider, ISitecoreService sitecoreService)
        {
            Assert.ArgumentNotNull(siteDefinitionsProvider, nameof(siteDefinitionsProvider));
            Assert.ArgumentNotNull(sitecoreService, nameof(sitecoreService));

            this.siteDefinitionsProvider = siteDefinitionsProvider;
            this.sitecoreService = sitecoreService;
        }

        public T GetSetting<T>(ID settingTemplateId) where T : class
        {
            Assert.ArgumentNotNull(settingTemplateId, nameof(settingTemplateId));

            var settingItem = this.GetSetting(settingTemplateId);

            return this.sitecoreService.GetItem<T>(settingItem);
        }

        private Item GetSetting(ID settingTemplateId)
        {
            var settingsFolder = this.GetSettingsFolder();
            Item targetSettingItem = null;

            if (settingsFolder != null)
            {
                targetSettingItem = settingsFolder.IsDerived(settingTemplateId)
                    ? settingsFolder
                    : settingsFolder.Children.FirstOrDefault(i => i.IsDerived(settingTemplateId));
            }

            return targetSettingItem;
        }

        private Item GetSettingsFolder()
        {
            var siteDefinition = this.siteDefinitionsProvider.GetCurrentSiteDefinition();

            if (siteDefinition == null)
            {
                return null;
            }

            var definitionItem = siteDefinition.RootItem;

            var result = definitionItem.Children.FirstOrDefault(i => i.IsDerived(new ID(Models.SiteSettings.TemplateId)));

            return result;
        }
    }
}