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

namespace Wooli.Foundation.Commerce.Context
{
    using System.Collections;
    using System.Web;
    using DependencyInjection;
    using Providers;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Utils;

    [Service(typeof(ISiteContext))]
    public class SiteContext : ISiteContext
    {
        private const string CurrentProductItemKey = "_CurrentProductItem";
        private const string CurrentCategoryItemKey = "_CurrentCategoryItem";
        private const string CurrentItemKey = "_CurrentItem";
        private const string IsCategoryKey = "_IsCategory";
        private const string IsProductKey = "_IsProduct";

        public SiteContext(IItemTypeProvider itemTypeProvider)
        {
            Assert.ArgumentNotNull(itemTypeProvider, "itemTypeProvider must not be null");
            ItemTypeProvider = itemTypeProvider;
        }

        public IItemTypeProvider ItemTypeProvider { get; set; }

        public Item CurrentCategoryItem
        {
            get => Items[CurrentCategoryItemKey] as Item;
            set => Items[CurrentCategoryItemKey] = value;
        }

        public Item CurrentProductItem
        {
            get => Items[CurrentProductItemKey] as Item;
            set => Items[CurrentProductItemKey] = value;
        }

        public Item CurrentItem
        {
            get => Items[CurrentItemKey] as Item;
            set
            {
                Items[CurrentItemKey] = value;
                if (value != null)
                {
                    var itemType = ItemTypeProvider.ResolveByItem(value);
                    Items[IsCategoryKey] = itemType == Constants.ItemType.Category;
                    Items[IsProductKey] = itemType == Constants.ItemType.Product;
                }
                else
                {
                    Items[IsCategoryKey] = false;
                    Items[IsProductKey] = false;
                }
            }
        }

        public virtual HttpContext CurrentContext => HttpContext.Current;

        public string VirtualFolder => Sitecore.Context.Site.VirtualFolder;

        public bool IsCategory
        {
            get
            {
                if (Items[IsCategoryKey] != null) return (bool) Items[IsCategoryKey];

                return false;
            }
        }

        public bool IsProduct
        {
            get
            {
                if (Items[IsProductKey] != null) return (bool) Items[IsProductKey];

                return false;
            }
        }

        public IDictionary Items => HttpContext.Current.Items;
    }
}