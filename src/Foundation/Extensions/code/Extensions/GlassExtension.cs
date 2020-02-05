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

namespace Wooli.Foundation.Extensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GlassMapper.Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public static class GlassExtension
    {
        public static IList<MediaItem> ExtractMediaItems<T>(this T glassModel, Func<T, IEnumerable<Guid>> selector)
            where T : IGlassBase
        {
            Assert.ArgumentNotNull(glassModel, nameof(glassModel));
            Assert.ArgumentNotNull(selector, nameof(selector));

            IEnumerable<Guid> imageGuids = selector(glassModel);

            IList<MediaItem> imageUrls = imageGuids
                ?.Select(x => glassModel.Item.Database.GetItem(ID.Parse(x)))
                .Where(x => x != null)
                .Select(x => new MediaItem(x))
                .ToList();

            return imageUrls;
        }
    }
}