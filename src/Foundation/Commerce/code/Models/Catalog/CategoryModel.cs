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

namespace Wooli.Foundation.Commerce.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Sitecore.Diagnostics;

    using TypeLite;

    using Wooli.Foundation.Connect.Models;

    [TsClass]
    public class CategoryModel
    {
        public string SitecoreId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IList<string> ParentCatalogList { get; set; }

        public IList<string> ChildrenCategoryList { get; set; }

        public void Initialize(IConnectCategoryModel categoryModel)
        {
            Assert.ArgumentNotNull(categoryModel, nameof(categoryModel));

            this.SitecoreId = categoryModel.SitecoreId;
            this.Name = categoryModel.Name;
            this.DisplayName = categoryModel.DisplayName;
            this.Description = categoryModel.Description;

            this.ParentCatalogList = categoryModel.ParentCatalogList?.Split('|').ToList();
            this.ChildrenCategoryList = categoryModel.ChildrenCategoryList?.Split('|').ToList();
        }
    }
}
