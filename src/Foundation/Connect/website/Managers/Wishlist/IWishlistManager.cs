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

namespace HCA.Foundation.Connect.Managers.Wishlist
{
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities.WishLists;
    using Sitecore.Commerce.Services.WishLists;

    /// <summary>
    /// Executes WishListServiceProvider methods
    /// </summary>
    public interface IWishlistManager
    {
        /// <summary>
        /// Adds products to wishlist
        /// </summary>
        /// <param name="wishlist">Current wishlist</param>
        /// <param name="lines">Lines for adding</param>
        /// <returns>Add lines to wishlist result</returns>
        AddLinesToWishListResult AddLinesToWishlist(WishList wishlist, IEnumerable<WishListLine> lines);

        /// <summary>
        /// Gets current wishlist
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="shopName">Shop name</param>
        /// <returns>Get wishlist result</returns>
        GetWishListResult GetWishlist(string userId, string shopName);

        /// <summary>
        /// Removes products from wishlist
        /// </summary>
        /// <param name="wishlist">Current wishlist</param>
        /// <param name="lineIds">Product variant ids</param>
        /// <returns>Remove wishlist lines result</returns>
        RemoveWishListLinesResult RemoveWishlistLines(WishList wishlist, IEnumerable<string> lineIds);
    }
}