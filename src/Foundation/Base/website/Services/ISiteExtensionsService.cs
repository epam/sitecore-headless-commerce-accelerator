﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.Base.Services
{
    using Sitecore.Data.Items;
    using Sitecore.Sites;

    /// <summary>
    /// Proxy interface for static SiteExtensions class
    /// </summary>
    public interface ISiteExtensionsService
    {
        /// <summary>
        /// Method for getting Sitecore.Data.Item.Item depending on conditions
        /// </summary>
        /// <param name="site"></param>
        /// <returns>Item</returns>
        Item GetStartItem(SiteContext site);
    }
}