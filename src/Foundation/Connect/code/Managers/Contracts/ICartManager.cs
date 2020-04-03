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

    public interface ICartManager
    {
        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> AddLineItemsToCart(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            IEnumerable<CartLineArgument> cartLines,
            string giftCardProductId,
            string giftCardPageLink);

        ManagerResponse<AddPaymentInfoResult, Sitecore.Commerce.Entities.Carts.Cart> AddPaymentInfo(
            string shopName,
            Sitecore.Commerce.Entities.Carts.Cart cart,
            Party billingPartyEntity,
            FederatedPaymentArgs federatedPaymentArgs);

        ManagerResponse<AddPromoCodeResult, Sitecore.Commerce.Entities.Carts.Cart> AddPromoCode(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            string promoCode);

        ManagerResponse<AddShippingInfoResult, Sitecore.Commerce.Entities.Carts.Cart> AddShippingInfo(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            List<Party> partyEntityList,
            ShippingOptionType shippingOptionType,
            List<ShippingInfoArgument> shippingInfoList);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> CreateOrResumeCart(
            string shopName,
            string userId,
            string customerId);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> GetCurrentCart(
            string shopName,
            string customerId);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> MergeCarts(
            string shopName,
            string customerId,
            string anonymousVisitorId,
            Sitecore.Commerce.Entities.Carts.Cart anonymousVisitorCart);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> RemoveLineItemsFromCart(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            IEnumerable<string> cartLineIds);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> UpdateCart(
            string shopName,
            Sitecore.Commerce.Entities.Carts.Cart currentCart,
            CartBase cartUpdate);

        ManagerResponse<CartResult, Sitecore.Commerce.Entities.Carts.Cart> UpdateLineItemsInCart(
            Sitecore.Commerce.Entities.Carts.Cart cart,
            IEnumerable<CartLineArgument> cartLines,
            string giftCardProductId,
            string giftCardPageLink);
    }
}