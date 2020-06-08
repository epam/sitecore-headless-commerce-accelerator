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

namespace HCA.Foundation.Connect.Providers.Search
{
    using System;

    using Models.Search;

    /// <summary>
    /// Provides search setting
    /// </summary>
    public interface ISearchSettingsProvider
    {
        /// <summary>
        /// Gets search settings from current context Sitecore Catalog item or category item if specified
        /// </summary>
        /// <param name="categoryId">Category id item</param>
        /// <returns>GetProducts settings</returns>
        SearchSettings GetSearchSettings(Guid categoryId);
    }
}