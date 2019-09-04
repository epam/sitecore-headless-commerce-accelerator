//    Copyright 2019 EPAM Systems, Inc.
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
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;

    using Wooli.Foundation.Connect.Models;

    public interface ICartManager
    {
        ManagerResponse<CartResult, Cart> AddLineItemsToCart(Cart cart, IEnumerable<CartLineArgument> cartLines, string giftCardProductId, string giftCardPageLink);

        ManagerResponse<CartResult, Cart> UpdateLineItemsInCart(Cart cart, IEnumerable<CartLineArgument> cartLines, string giftCardProductId, string giftCardPageLink);

        ManagerResponse<CartResult, Cart> RemoveLineItemsFromCart(Cart cart, IEnumerable<string> cartLineIds);

        ManagerResponse<CartResult, Cart> GetCurrentCart(string shopName, string customerId);

        ManagerResponse<CartResult, Cart> CreateOrResumeCart(string shopName, string userId, string customerId);

        ManagerResponse<CartResult, Cart> MergeCarts(string shopName, string customerId, string anonymousVisitorId, Cart anonymousVisitorCart);

        ManagerResponse<AddPromoCodeResult, Cart> AddPromoCode(Cart cart, string promoCode);

        ManagerResponse<CartResult, Cart> UpdateCart(string shopName, Cart currentCart, CartBase cartUpdate);

        ManagerResponse<AddPaymentInfoResult, Cart> AddPaymentInfo(
            string shopName,
            Cart cart,
            PartyEntity billingPartyEntity,
            FederatedPaymentArgs federatedPaymentArgs);

        ManagerResponse<AddShippingInfoResult, Cart> AddShippingInfo(Cart cart, List<PartyEntity> partyEntityList, ShippingOptionType shippingOptionType, List<ShippingInfoArgument> shippingInfoList);
    }
}
