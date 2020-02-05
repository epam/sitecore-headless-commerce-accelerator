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

namespace Wooli.Foundation.GlassMapper.Models
{
    using System;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Globalization;

    public interface IGlassBase
    {
        [SitecoreId] Guid Id { get; }

        [SitecoreInfo(SitecoreInfoType.Name)] string ItemName { get; set; }

        [SitecoreItem] Item Item { get; }

        [SitecoreParent] Item Parent { get; }

        [SitecoreChildren] IEnumerable<Item> Children { get; }

        [SitecoreInfo(SitecoreInfoType.Language)]
        Language Language { get; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

        [SitecoreInfo(SitecoreInfoType.Url)] string Url { get; }

        [SitecoreInfo(SitecoreInfoType.BaseTemplateIds)]
        IEnumerable<Guid> BaseTemplateIds { get; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        Guid ItemTemplateId { get; }

        bool InheritsTemplate(Guid templateGuid);

        bool InheritsTemplate(ID templateId);
    }
}