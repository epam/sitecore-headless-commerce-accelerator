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

namespace HCA.Foundation.Commerce.Services.Cart
{
    using Base.Models.Result;

    using Models.Entities.Cart;

    /// <summary>
    /// Performs main operations with cart
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Adds product to cart
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="variantId">Variant product id</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> AddCartLine(string productId, string variantId, decimal quantity);

        /// <summary>
        /// Adds promo code to cart
        /// </summary>
        /// <param name="promoCode">Promo code to add</param>
        /// <returns></returns>
        Result<Cart> AddPromoCode(string promoCode);

        /// <summary>
        /// Gets current cart
        /// </summary>
        /// <returns>Current cart result</returns>
        Result<Cart> GetCart();

        /// <summary>
        /// Merges two carts in one
        /// </summary>
        /// <param name="anonymousContactId">Id of anonymous user</param>
        /// <returns>Merged cart result</returns>
        Result<Cart> MergeCarts(string anonymousContactId);

        /// <summary>
        /// Removes product from cart
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="variantId">Variant product id</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> RemoveCartLine(string productId, string variantId);

        /// <summary>
        /// Removes promo code from cart
        /// </summary>
        /// <param name="promoCode">Promo code to add</param>
        /// <returns></returns>
        Result<Cart> RemovePromoCode(string promoCode);

        /// <summary>
        /// Updates product in cart
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="variantId">Variant product id</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> UpdateCartLine(string productId, string variantId, decimal quantity);
    }
}