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

namespace HCA.Foundation.Commerce.Providers.StorefrontSettings
{
    /// <summary>
    /// Provides storefront settings
    /// </summary>
    public interface IStorefrontSettingsProvider
    {
        /// <summary>
        /// Gets payment option id
        /// </summary>
        /// <param name="optionTitle">Payment option title</param>
        /// <returns>Payment option id</returns>
        string GetPaymentOptionId(string optionTitle);
    }
}