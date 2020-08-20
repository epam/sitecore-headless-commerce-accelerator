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

namespace HCA.Foundation.Commerce.Services.Wishlist
{
    using Base.Models.Result;

    using Models.Entities.Catalog;
    using Models.Entities.Wishlist;

    /// <summary>
    /// Performs main operations with wishlist
    /// </summary>
    public interface IWishlistService
    {
        /// <summary>
        /// Adds products to wishlist
        /// </summary>
        /// <param name="product">Product variant</param>
        /// <returns>Updated wishlist result</returns>
        Result<Wishlist> AddWishlistLine(Variant product);

        /// <summary>
        /// Gets current wishlist
        /// </summary>
        /// <returns>Current wishlist result</returns>
        Result<Wishlist> GetWishlist();

        /// <summary>
        /// Removes products from wishlist
        /// </summary>
        /// <param name="variantId">Product variant id</param>
        /// <returns>Updated wishlist result</returns>
        Result<Wishlist> RemoveWishlistLine(string variantId);
    }
}