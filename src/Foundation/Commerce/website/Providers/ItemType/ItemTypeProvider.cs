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

namespace HCA.Foundation.Commerce.Providers.ItemType
{
    using Connect.Models;

    using DependencyInjection;

    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Data.Templates;

    using Constants = Commerce.Constants;

    [Service(typeof(IItemTypeProvider))]
    public class ItemTypeProvider : IItemTypeProvider
    {
        public Constants.ItemType ResolveByItem(Item item)
        {
            var template = TemplateManager.GetTemplate(item);

            return this.ResolveByTemplate(template);
        }

        public Constants.ItemType ResolveByTemplate(Template template)
        {
            var itemType = Constants.ItemType.Unknown;

            if (template.InheritsFrom(CommerceCategoryModel.TemplateId))
            {
                itemType = Constants.ItemType.Category;
            }
            else if (template.InheritsFrom(CommerceProductModel.TemplateId))
            {
                itemType = Constants.ItemType.Product;
            }
            else if (template.InheritsFrom(CommerceProductVariantModel.TemplateId))
            {
                itemType = Constants.ItemType.Variant;
            }

            return itemType;
        }
    }
}