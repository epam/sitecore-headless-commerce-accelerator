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

namespace Wooli.Foundation.Commerce.Services.Analytics
{
    using Wooli.Foundation.Commerce.Models.Catalog;

    /// <summary>
    ///     Raise commerce related analytics events
    /// </summary>
    public interface ICommerceAnalyticsService
    {
        /// <summary>
        ///     Raise "Category Visited" event.
        /// </summary>
        /// <param name="category">Category model.</param>
        void RaiseCategoryVisitedEvent(CategoryModel category);

        /// <summary>
        ///     Raise "Product Visited" event.
        /// </summary>
        /// <param name="product">Product model.</param>
        void RaiseProductVisitedEvent(ProductModel product);
    }
}