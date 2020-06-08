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

namespace HCA.Foundation.GlassMapper.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;

    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Globalization;

    public abstract class GlassBase : IGlassBase
    {
        [SitecoreInfo(SitecoreInfoType.BaseTemplateIds)]
        public virtual IEnumerable<Guid> BaseTemplateIds { get; set; }

        [SitecoreChildren]
        public virtual IEnumerable<Item> Children { get; set; }

        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreItem]
        public virtual Item Item { get; set; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string ItemName { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        public virtual Guid ItemTemplateId { get; set; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        public virtual Language Language { get; set; }

        [SitecoreParent]
        public virtual Item Parent { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int Version { get; set; }

        public virtual bool InheritsTemplate(Guid templateGuid)
        {
            if (!this.BaseTemplateIds.Contains(templateGuid))
            {
                return this.ItemTemplateId.Equals(templateGuid);
            }

            return true;
        }

        public virtual bool InheritsTemplate(ID templateId)
        {
            return this.InheritsTemplate(templateId.Guid);
        }
    }
}