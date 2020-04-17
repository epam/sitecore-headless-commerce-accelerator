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

namespace HCA.Foundation.Commerce.Services.Analytics
{
    using Base.Models.Result;

    using Models.Entities.Catalog;

    /// <summary>
    /// Raise commerce related analytics events
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Raise "Category Visited" event.
        /// </summary>
        /// <param name="category">Category model.</param>
        /// <returns>Void result</returns>
        Result<VoidResult> RaiseCategoryVisitedEvent(Category category);

        /// <summary>
        /// Raise "Product Visited" event.
        /// </summary>
        /// <param name="product">Product model.</param>
        /// <returns>Void result</returns>
        Result<VoidResult> RaiseProductVisitedEvent(Product product);

        /// <summary>
        /// Raise "Search initiated" event.
        /// </summary>
        /// <param name="searchKeyword">Search keyword</param>
        /// <param name="totalItemCount">Total item count</param>
        /// <returns>Void result</returns>
        Result<VoidResult> RaiseSearchInitiatedEvent(string searchKeyword, int totalItemCount);
    }
}