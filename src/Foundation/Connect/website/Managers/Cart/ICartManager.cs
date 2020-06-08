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

namespace HCA.Foundation.Connect.Managers.Cart
{
    using System.Collections.Generic;

    using Models;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;

    /// <summary>
    /// Executes CommerceCartServiceProvider methods
    /// </summary>
    public interface ICartManager
    {
        /// <summary>
        /// Adds cart line
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="cartLines">Cart line</param>
        /// <returns>Cart result</returns>
        CartResult AddCartLines(
            Cart cart,
            IEnumerable<CartLine> cartLines);

        /// <summary>
        /// Adds payment info to the cart
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="party">Party</param>
        /// <param name="federatedPaymentInfo">Federated payment info</param>
        /// <returns>Add payment info result</returns>
        AddPaymentInfoResult AddPaymentInfo(Cart cart, Party party, FederatedPaymentInfo federatedPaymentInfo);

        /// <summary>
        /// Adds promo code
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="promoCode">Promo code</param>
        /// <returns>Cart result</returns>
        CartResult AddPromoCode(CommerceCart cart, string promoCode);

        /// <summary>
        /// Adds shipping info to the cart
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="shippingOptionType">Shipping option type</param>
        /// <param name="shippings">List of shipping info</param>
        /// <returns>Add shipping info result</returns>
        AddShippingInfoResult AddShippingInfo(
            Cart cart,
            ShippingOptionType shippingOptionType,
            List<ShippingInfo> shippings);

        /// <summary>
        /// Creates or resumes cart
        /// </summary>
        /// <param name="shopName">Shop name</param>
        /// <param name="customerId">Customer id</param>
        /// <param name="userId">User id</param>
        /// <returns>Cart result</returns>
        CartResult CreateOrResumeCart(string shopName, string userId, string customerId);

        /// <summary>
        /// Loads cart
        /// </summary>
        /// <param name="shopName">Shop name</param>
        /// <param name="customerId">Customer id</param>
        /// <returns>Cart result</returns>
        CartResult LoadCart(string shopName, string customerId);

        /// <summary>
        /// Merges carts
        /// </summary>
        /// <param name="fromCart">Source cart</param>
        /// <param name="toCart">Destination cart</param>
        /// <returns>Cart result</returns>
        CartResult MergeCarts(Cart fromCart, Cart toCart);

        /// <summary>
        /// Removes cart line
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="cartLines">Cart line</param>
        /// <returns>Cart result</returns>
        CartResult RemoveCartLines(Cart cart, IEnumerable<CartLine> cartLines);

        /// <summary>
        /// Removes all payment info from cart
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <returns>Cart result</returns>
        CartResult RemovePaymentInfo(Cart cart);

        /// <summary>
        /// Removes promo code
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="promoCode">Promo code</param>
        /// <returns>Cart result</returns>
        CartResult RemovePromoCode(CommerceCart cart, string promoCode);

        /// <summary>
        /// Updates cart
        /// </summary>
        /// <param name="cart">Cart to update</param>
        /// <param name="cartUpdate">Cart update</param>
        /// <returns>Updated cart result</returns>
        CartResult UpdateCart(Cart cart, CartBase cartUpdate);

        /// <summary>
        /// Updates cart line
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <param name="cartLines">Cart line</param>
        /// <returns>Cart result</returns>
        CartResult UpdateCartLines(
            Cart cart,
            IEnumerable<CartLine> cartLines);
    }
}