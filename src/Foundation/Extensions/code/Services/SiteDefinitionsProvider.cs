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

namespace Wooli.Foundation.Extensions.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Sites;
    using Sitecore.Web;

    public class SiteDefinitionsProvider : ISiteDefinitionsProvider
    {
        private readonly IEnumerable<SiteInfo> sites;

        private IEnumerable<SiteDefinition> siteDefinitions;

        public SiteDefinitionsProvider() : this(SiteContextFactory.Sites)
        {
        }

        public SiteDefinitionsProvider(IEnumerable<SiteInfo> sites)
        {
            this.sites = sites;
        }

        public IEnumerable<SiteDefinition> SiteDefinitions => siteDefinitions ?? (siteDefinitions =
                                                                  sites.Where(IsValidSite).Select(Create)
                                                                      .OrderBy(s => s.RootItem.Appearance.Sortorder)
                                                                      .ToArray());

        public SiteDefinition GetCurrentSiteDefinition()
        {
            return SiteDefinitions.FirstOrDefault(s => s.IsCurrent);
        }

        public SiteDefinition GetContextSiteDefinition(Item item)
        {
            SiteDefinition rootSite = SiteDefinitions
                .Where(x => item.Paths.FullPath.StartsWith(x.RootItem.Paths.FullPath))
                .OrderByDescending(x => x.IsCurrent)
                .FirstOrDefault();

            return rootSite ?? SiteDefinitions.FirstOrDefault(s => s.IsCurrent);
        }

        private bool IsValidSite([NotNull] SiteInfo site)
        {
            return GetSiteRootItem(site) != null;
        }

        private Item GetSiteRootItem(SiteInfo site)
        {
            if (site == null) throw new ArgumentNullException(nameof(site));

            if (string.IsNullOrEmpty(site.Database)) return null;

            var database = Database.GetDatabase(site.Database);
            Item item = database?.GetItem(site.RootPath);

            return item;
        }

        private SiteDefinition Create([NotNull] SiteInfo site)
        {
            if (site == null) throw new ArgumentNullException(nameof(site));

            Item siteItem = GetSiteRootItem(site);
            return new SiteDefinition
            {
                RootItem = siteItem,
                Name = site.Name,
                HostName = GetHostName(site),
                Site = site,
                RootPath = siteItem?.Paths.FullPath,
                StartPath = $"{siteItem?.Paths.FullPath?.TrimEnd('/')}/{site.StartItem?.Trim('/')}"
            };
        }

        private string GetHostName(SiteInfo site)
        {
            if (!string.IsNullOrEmpty(site.TargetHostName)) return site.TargetHostName;

            if (Uri.CheckHostName(site.HostName) != UriHostNameType.Unknown) return site.HostName;

            Log.Warn($"Cannot determine hostname for site '{site.Name}'.", GetType());
            return null;
        }
    }
}