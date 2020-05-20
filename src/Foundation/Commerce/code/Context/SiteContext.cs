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

namespace HCA.Foundation.Commerce.Context
{
    using System.Collections;
    using System.Web;

    using DependencyInjection;

    using Providers;

    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    [Service(typeof(ISiteContext))]
    public class SiteContext : ISiteContext
    {
        private const string CurrentCategoryItemKey = "_CurrentCategoryItem";

        private const string CurrentItemKey = "_CurrentItem";

        private const string CurrentProductItemKey = "_CurrentProductItem";

        private const string IsCategoryKey = "_IsCategory";

        private const string IsProductKey = "_IsProduct";

        public SiteContext(IItemTypeProvider itemTypeProvider)
        {
            Assert.ArgumentNotNull(itemTypeProvider, "itemTypeProvider must not be null");
            this.ItemTypeProvider = itemTypeProvider;
        }

        public IItemTypeProvider ItemTypeProvider { get; set; }

        public Item CurrentCategoryItem
        {
            get => this.Items[CurrentCategoryItemKey] as Item;
            set => this.Items[CurrentCategoryItemKey] = value;
        }

        public virtual HttpContext CurrentContext => HttpContext.Current;

        public Item CurrentItem
        {
            get => this.Items[CurrentItemKey] as Item;
            set
            {
                this.Items[CurrentItemKey] = value;
                if (value != null)
                {
                    var itemType = this.ItemTypeProvider.ResolveByItem(value);
                    this.Items[IsCategoryKey] = itemType == Commerce.Constants.ItemType.Category;
                    this.Items[IsProductKey] = itemType == Commerce.Constants.ItemType.Product;
                }
                else
                {
                    this.Items[IsCategoryKey] = false;
                    this.Items[IsProductKey] = false;
                }
            }
        }

        public Item CurrentProductItem
        {
            get => this.Items[CurrentProductItemKey] as Item;
            set => this.Items[CurrentProductItemKey] = value;
        }

        public bool IsCategory
        {
            get
            {
                if (this.Items[IsCategoryKey] != null)
                {
                    return (bool)this.Items[IsCategoryKey];
                }

                return false;
            }
        }

        public bool IsProduct
        {
            get
            {
                if (this.Items[IsProductKey] != null)
                {
                    return (bool)this.Items[IsProductKey];
                }

                return false;
            }
        }

        public IDictionary Items => HttpContext.Current.Items;

        public string VirtualFolder => Context.Site.VirtualFolder;
    }
}