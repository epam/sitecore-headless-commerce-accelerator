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

namespace Wooli.Foundation.Commerce.Services.Cart
{
    using Models;
    using Models.Entities;

    /// <summary>
    /// Performs main operations with cart 
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Gets current cart
        /// </summary>
        /// <returns>Current cart result</returns>
        Result<Cart> GetCart();

        /// <summary>
        /// Merges two carts in one
        /// </summary>
        /// <param name="source">Source cart</param>
        /// <param name="destination">Destination cart</param>
        /// <returns>Merged cart result</returns>
        Result<Cart> MergeCarts(Cart source, Cart destination);

        /// <summary>
        /// Adds product to cart
        /// </summary>
        /// <param name="cart">Cart to add</param>
        /// <param name="cartLine">Cart line</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> AddCartLine(Cart cart, CartLine cartLine);

        /// <summary>
        /// Updates product in cart
        /// </summary>
        /// <param name="cart">Cart to update</param>
        /// <param name="cartLine">Cart line</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> UpdateCartLine(Cart cart, CartLine cartLine);

        /// <summary>
        /// Removes product from cart
        /// </summary>
        /// <param name="cart">Cart to remove product from</param>
        /// <param name="cartLine">Cart line</param>
        /// <returns>Updated cart result</returns>
        Result<Cart> RemoveCartLine(Cart cart, CartLine cartLine);

        /// <summary>
        /// Adds promo code to cart
        /// </summary>
        /// <param name="cart">Cart to add promo code to</param>
        /// <param name="promoCode">Promo code to add</param>
        /// <returns></returns>
        Result<Cart> AddPromoCode(Cart cart, string promoCode);

        /// <summary>
        /// Removes promo code from cart
        /// </summary>
        /// <param name="cart">Cart to remove promo code from</param>
        /// <param name="promoCode">Promo code to add</param>
        /// <returns></returns>
        Result<Cart> RemovePromoCode(Cart cart, string promoCode);
    }
}