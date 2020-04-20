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

namespace HCA.Foundation.Base.Models
{
    using System;

    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Web;

    public class SiteDefinition
    {
        public string HostName { get; set; }

        public bool IsCurrent =>
            Context.Site != null && Context.Site.Name.Equals(this.Name, StringComparison.OrdinalIgnoreCase);

        public string Name { get; set; }

        public Item RootItem { get; set; }

        public string RootPath { get; set; }

        public SiteInfo Site { get; set; }

        public string StartPath { get; set; }

        public virtual Item GetRootItem(Database database)
        {
            return database.GetItem(this.Site.RootPath);
        }
    }
}