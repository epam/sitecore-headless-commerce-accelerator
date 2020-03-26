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

namespace Wooli.Foundation.Connect.Managers
{
    using System.Collections.Generic;

    using Models;

    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;

    using Carts = Sitecore.Commerce.Entities.Carts;

    public interface ICartManager
    {
        ManagerResponse<CartResult, Carts.Cart> AddLineItemsToCart(
            Carts.Cart cart,
            IEnumerable<CartLineArgument> cartLines,
            string giftCardProductId,
            string giftCardPageLink);

        ManagerResponse<AddPaymentInfoResult, Carts.Cart> AddPaymentInfo(
            string shopName,
            Carts.Cart cart,
            Party billingPartyEntity,
            FederatedPaymentArgs federatedPaymentArgs);

        ManagerResponse<AddPromoCodeResult, Carts.Cart> AddPromoCode(Carts.Cart cart, string promoCode);

        ManagerResponse<AddShippingInfoResult, Carts.Cart> AddShippingInfo(
            Carts.Cart cart,
            List<Party> partyEntityList,
            ShippingOptionType shippingOptionType,
            List<ShippingInfoArgument> shippingInfoList);

        ManagerResponse<CartResult, Carts.Cart> CreateOrResumeCart(string shopName, string userId, string customerId);

        ManagerResponse<CartResult, Carts.Cart> GetCurrentCart(string shopName, string customerId);

        ManagerResponse<CartResult, Carts.Cart> MergeCarts(
            string shopName,
            string customerId,
            string anonymousVisitorId,
            Carts.Cart anonymousVisitorCart);

        ManagerResponse<CartResult, Carts.Cart> RemoveLineItemsFromCart(Carts.Cart cart, IEnumerable<string> cartLineIds);

        ManagerResponse<CartResult, Carts.Cart> UpdateCart(string shopName, Carts.Cart currentCart, CartBase cartUpdate);

        ManagerResponse<CartResult, Carts.Cart> UpdateLineItemsInCart(
            Carts.Cart cart,
            IEnumerable<CartLineArgument> cartLines,
            string giftCardProductId,
            string giftCardPageLink);
    }
}