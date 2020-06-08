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

namespace HCA.Foundation.Base.Extensions
{
    using System;

    using Sitecore;
    using Sitecore.Data.Fields;
    using Sitecore.Links.UrlBuilders;
    using Sitecore.Resources.Media;

    public static class FieldExtensions
    {
        public static string ImageUrl(this ImageField imageField)
        {
            if (imageField?.MediaItem == null)
            {
                throw new ArgumentNullException(nameof(imageField));
            }

            var options = MediaUrlBuilderOptions.Empty;

            if (int.TryParse(imageField.Width, out var width))
            {
                options.Width = width;
            }

            if (int.TryParse(imageField.Height, out var height))
            {
                options.Height = height;
            }

            return imageField.ImageUrl(options);
        }

        public static string ImageUrl(this ImageField imageField, MediaUrlBuilderOptions builderOptions)
        {
            if (imageField?.MediaItem == null)
            {
                throw new ArgumentNullException(nameof(imageField));
            }

            return builderOptions == null
                ? imageField.ImageUrl()
                : HashingUtils.ProtectAssetUrl(MediaManager.GetMediaUrl(imageField.MediaItem, builderOptions));
        }

        public static bool IsChecked(this Field checkboxField)
        {
            if (checkboxField == null)
            {
                throw new ArgumentNullException(nameof(checkboxField));
            }

            return MainUtil.GetBool(checkboxField.Value, false);
        }
    }
}