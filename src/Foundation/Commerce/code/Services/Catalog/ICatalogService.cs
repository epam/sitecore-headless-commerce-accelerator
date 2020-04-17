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

namespace HCA.Foundation.Commerce.Services.Catalog
{
    using Base.Models.Result;

    using Models.Entities.Catalog;

    /// <summary>
    /// Performs operations with catalog
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Gets current context category
        /// </summary>
        /// <returns>Category result</returns>
        Result<Category> GetCurrentCategory();

        /// <summary>
        /// Gets current context product
        /// </summary>
        /// <returns>Product result</returns>
        Result<Product> GetCurrentProduct();

        /// <summary>
        /// Gets product model by Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Product result</returns>
        Result<Product> GetProduct(string productId);
    }
}