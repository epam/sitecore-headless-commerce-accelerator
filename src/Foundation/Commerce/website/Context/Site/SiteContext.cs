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

namespace HCA.Foundation.Commerce.Context.Site
{
    using System.Collections;
    using System.Web;

    using DependencyInjection;

    using Models.Entities.Catalog;

    [Service(typeof(ISiteContext))]
    public class SiteContext : ISiteContext
    {
        private const string CurrentCategoryItemKey = "_CurrentCategoryItem";
        private const string CurrentProductItemKey = "_CurrentProductItem";

        public Category CurrentCategory
        {
            get => this.Items[CurrentCategoryItemKey] as Category;
            set => this.Items[CurrentCategoryItemKey] = value;
        }

        public Product CurrentProduct
        {
            get => this.Items[CurrentProductItemKey] as Product;
            set => this.Items[CurrentProductItemKey] = value;
        }

        private IDictionary Items => HttpContext.Current.Items;
    }
}