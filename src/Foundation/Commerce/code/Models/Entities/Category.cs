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

namespace Wooli.Foundation.Commerce.Models.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class Category
    {
        public Category(Item categoryItem)
        {
            Assert.ArgumentNotNull(categoryItem, nameof(categoryItem));

            this.SitecoreId = categoryItem["SitecoreId"];
            this.Name = categoryItem["Name"];
            this.DisplayName = categoryItem["DisplayName"];
            this.Description = categoryItem["Description"];

            this.ParentCatalogList = categoryItem["ParentCatalogList"]?.Split('|').ToList();
            this.ChildrenCategoryList = categoryItem["ChildrenCategoryList"]?.Split('|').ToList();
        }

        public IList<string> ChildrenCategoryList { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public IList<string> ParentCatalogList { get; set; }

        public string SitecoreId { get; set; }
    }
}