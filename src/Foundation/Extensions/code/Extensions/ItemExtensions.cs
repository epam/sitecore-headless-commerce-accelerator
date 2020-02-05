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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Wooli.Foundation.Extensions.Extensions
{
    public static class ItemExtensions
    {
        public static string Url(this Item item, UrlOptions options = null)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (options != null) return LinkManager.GetItemUrl(item, options);

            return !item.Paths.IsMediaItem ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
        }

        public static string ImageUrl(this Item item, ID imageFieldId, MediaUrlOptions options = null)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var imageField = (ImageField) item.Fields[imageFieldId];
            return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
        }

        public static string ImageUrl(this MediaItem mediaItem, int width, int height)
        {
            if (mediaItem == null) throw new ArgumentNullException(nameof(mediaItem));

            var options = new MediaUrlOptions {Height = height, Width = width};
            string url = MediaManager.GetMediaUrl(mediaItem, options);
            string cleanUrl = StringUtil.EnsurePrefix('/', url);
            string hashedUrl = HashingUtils.ProtectAssetUrl(cleanUrl);

            return hashedUrl;
        }

        public static string ImageUrl(this MediaItem mediaItem)
        {
            if (mediaItem == null) throw new ArgumentNullException(nameof(mediaItem));

            string url = MediaManager.GetMediaUrl(mediaItem);
            string cleanUrl = StringUtil.EnsurePrefix('/', url);
            string hashedUrl = HashingUtils.ProtectAssetUrl(cleanUrl);

            return hashedUrl;
        }

        public static Item TargetItem(this Item item, ID linkFieldId)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (item.Fields[linkFieldId] == null || !item.Fields[linkFieldId].HasValue) return null;

            return ((LinkField) item.Fields[linkFieldId]).TargetItem ??
                   ((ReferenceField) item.Fields[linkFieldId]).TargetItem;
        }

        public static bool IsImage(this Item item)
        {
            return new MediaItem(item).MimeType.StartsWith("image/", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsVideo(this Item item)
        {
            return new MediaItem(item).MimeType.StartsWith("video/", StringComparison.InvariantCultureIgnoreCase);
        }

        public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateId)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return item.IsDerived(templateId)
                ? item
                : item.Axes.GetAncestors().LastOrDefault(i => i.IsDerived(templateId));
        }

        public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item item, ID templateId)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var returnValue = new List<Item>();
            if (item.IsDerived(templateId)) returnValue.Add(item);

            returnValue.AddRange(item.Axes.GetAncestors().Reverse().Where(i => i.IsDerived(templateId)));
            return returnValue;
        }

        public static string LinkFieldUrl(this Item item, ID fieldId)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (ID.IsNullOrEmpty(fieldId)) throw new ArgumentNullException(nameof(fieldId));

            Field field = item.Fields[fieldId];
            if (field == null || !(FieldTypeManager.GetField(field) is LinkField)) return string.Empty;

            LinkField linkField = field;
            switch (linkField.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    return linkField.TargetItem != null ? LinkManager.GetItemUrl(linkField.TargetItem) : string.Empty;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    return linkField.TargetItem != null ? MediaManager.GetMediaUrl(linkField.TargetItem) : string.Empty;
                case "external":
                    // Just return external links
                    return linkField.Url;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                case "mailto":
                    // Just return mailto link
                    return linkField.Url;
                case "javascript":
                    // Just return javascript
                    return linkField.Url;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    return linkField.Url;
            }
        }

        public static string LinkFieldTarget(this Item item, ID fieldId)
        {
            return item.LinkFieldOptions(fieldId, LinkFieldOption.Target);
        }

        public static string LinkFieldOptions(this Item item, ID fieldId, LinkFieldOption option)
        {
            XmlField field = item.Fields[fieldId];
            switch (option)
            {
                case LinkFieldOption.Text:
                    return field?.GetAttribute("text");
                case LinkFieldOption.LinkType:
                    return field?.GetAttribute("linktype");
                case LinkFieldOption.Class:
                    return field?.GetAttribute("class");
                case LinkFieldOption.Alt:
                    return field?.GetAttribute("title");
                case LinkFieldOption.Target:
                    return field?.GetAttribute("target");
                case LinkFieldOption.QueryString:
                    return field?.GetAttribute("querystring");
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }

        public static bool HasLayout(this Item item)
        {
            return item?.Visualization?.Layout != null;
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null) return false;

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        public static bool IsDerived(this Item item, Item templateItem)
        {
            if (item == null) return false;

            if (templateItem == null) return false;

            Template itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null &&
                   (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }

        public static bool FieldHasValue(this Item item, ID fieldId)
        {
            return item.Fields[fieldId] != null && !string.IsNullOrWhiteSpace(item.Fields[fieldId].Value);
        }

        public static int? GetInteger(this Item item, ID fieldId)
        {
            return !int.TryParse(item.Fields[fieldId].Value, out int result) ? new int?() : result;
        }

        public static double? GetDouble(this Item item, ID fieldId)
        {
            string value = item?.Fields[fieldId]?.Value;
            if (value == null) return null;

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double num) ||
                double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out num) ||
                double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out num))
                return num;

            return null;
        }

        public static IEnumerable<Item> GetMultiListValueItems(this Item item, ID fieldId)
        {
            return new MultilistField(item.Fields[fieldId]).GetItems();
        }

        public static bool HasContextLanguage(this Item item)
        {
            Item latestVersion = item.Versions.GetLatestVersion();
            return latestVersion?.Versions.Count > 0;
        }
    }
}