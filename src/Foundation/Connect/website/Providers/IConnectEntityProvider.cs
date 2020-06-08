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

namespace HCA.Foundation.Connect.Providers
{
    using Sitecore.Commerce.Multishop;
    using Sitecore.Commerce.Providers;

    /// <summary>
    /// Provides specified connect entities
    /// </summary>
    public interface IConnectEntityProvider
    {
        /// <summary>
        /// Gets IShopProvider entity
        /// </summary>
        /// <returns>Instance of IShopProvider</returns>
        IShopProvider GetShopProvider();

        /// <summary>
        /// Get IConnectStorefrontContext entity
        /// </summary>
        /// <returns>Instance of IConnectStorefrontContext</returns>
        IConnectStorefrontContext GetConnectStorefrontContext();
    }
}