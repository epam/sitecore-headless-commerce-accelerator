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

namespace HCA.Foundation.Commerce.Mappers.Wishlist
{
    using Base.Mappers;

    using Models.Entities.Wishlist;

    using Sitecore.Commerce.Entities.WishLists;

    /// <summary>
    /// Performs mapping for wishlist models
    /// </summary>
    public interface IWishlistMapper : IMapper
    {
        /// <summary>
        /// Map Sitecore wishlist model to local wishlist model
        /// </summary>
        /// <param name="source">Local wishlist</param>
        /// <returns>Sitecore wishlist</returns>
        Wishlist Map(WishList source);

        /// <summary>
        /// Map Sitecore wishlist line model to local wishlist line model
        /// </summary>
        /// <param name="source">Sitecore wishlist line</param>
        /// <returns>Local wishlist line</returns>
        WishlistLine Map(WishListLine source);
    }
}