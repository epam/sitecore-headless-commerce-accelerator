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

namespace HCA.Foundation.Base.Providers.SiteDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    using Sitecore.Annotations;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Sites;
    using Sitecore.Web;

    public class SiteDefinitionsProvider : ISiteDefinitionsProvider
    {
        private readonly IEnumerable<SiteInfo> sites;

        private IEnumerable<SiteDefinition> siteDefinitions;

        public SiteDefinitionsProvider()
            : this(SiteContextFactory.Sites)
        {
        }

        public SiteDefinitionsProvider(IEnumerable<SiteInfo> sites)
        {
            this.sites = sites;
        }

        public IEnumerable<SiteDefinition> SiteDefinitions =>
            this.siteDefinitions ?? (this.siteDefinitions = this.sites.Where(this.IsValidSite)
                .Select(this.Create)
                .OrderBy(s => s.RootItem.Appearance.Sortorder)
                .ToArray());

        public SiteDefinition GetContextSiteDefinition(Item item)
        {
            var rootSite = this.SiteDefinitions.Where(x => item.Paths.FullPath.StartsWith(x.RootItem.Paths.FullPath))
                .OrderByDescending(x => x.IsCurrent)
                .FirstOrDefault();

            return rootSite ?? this.SiteDefinitions.FirstOrDefault(s => s.IsCurrent);
        }

        public SiteDefinition GetCurrentSiteDefinition()
        {
            return this.SiteDefinitions.FirstOrDefault(s => s.IsCurrent);
        }

        private SiteDefinition Create([NotNull] SiteInfo site)
        {
            if (site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            var siteItem = this.GetSiteRootItem(site);
            return new SiteDefinition
            {
                RootItem = siteItem,
                Name = site.Name,
                HostName = this.GetHostName(site),
                Site = site,
                RootPath = siteItem?.Paths.FullPath,
                StartPath = $"{siteItem?.Paths.FullPath?.TrimEnd('/')}/{site.StartItem?.Trim('/')}"
            };
        }

        private string GetHostName(SiteInfo site)
        {
            if (!string.IsNullOrEmpty(site.TargetHostName))
            {
                return site.TargetHostName;
            }

            if (Uri.CheckHostName(site.HostName) != UriHostNameType.Unknown)
            {
                return site.HostName;
            }

            Log.Warn($"Cannot determine hostname for site '{site.Name}'.", this.GetType());
            return null;
        }

        private Item GetSiteRootItem(SiteInfo site)
        {
            if (site == null)
            {
                throw new ArgumentNullException(nameof(site));
            }

            if (string.IsNullOrEmpty(site.Database))
            {
                return null;
            }

            var database = Database.GetDatabase(site.Database);
            var item = database?.GetItem(site.RootPath);

            return item;
        }

        private bool IsValidSite([NotNull] SiteInfo site)
        {
            return this.GetSiteRootItem(site) != null;
        }
    }
}