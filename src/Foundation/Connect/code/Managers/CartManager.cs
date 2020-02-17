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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection;
    using Extensions.Extensions;
    using ModelMappers;
    using Models;
    using Providers.Contracts;
    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Links;
    using Utils;
    using AddShippingInfoRequest = Sitecore.Commerce.Engine.Connect.Services.Carts.AddShippingInfoRequest;

    [Service(typeof(ICartManager))]
    public class CartManager : ICartManager
    {
        private readonly CommerceCartServiceProvider cartServiceProvider;
        private readonly IConnectEntityMapper connectEntityMapper;
        private readonly ISearchManager searchManager;

        public CartManager(
            ISearchManager searchManager,
            IConnectServiceProvider connectServiceProvider, IConnectEntityMapper connectEntityMapper)
        {
            Assert.ArgumentNotNull(searchManager, nameof(searchManager));
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(connectEntityMapper, nameof(connectEntityMapper));
            this.searchManager = searchManager;
            cartServiceProvider = connectServiceProvider.GetCommerceCartServiceProvider();
            this.connectEntityMapper = connectEntityMapper;
        }

        public ManagerResponse<CartResult, Cart> AddLineItemsToCart(Cart cart, IEnumerable<CartLineArgument> cartLines,
            string giftCardProductId, string giftCardPageLink)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLines, nameof(cartLines));

            var cartLineList = new List<CartLine>();

            foreach (var cartLine in cartLines)
            {
                Assert.ArgumentNotNullOrEmpty(cartLine.ProductId, "inputModel.ProductId");
                Assert.ArgumentNotNullOrEmpty(cartLine.CatalogName, "inputModel.CatalogName");
                Assert.ArgumentNotNull(cartLine.Quantity, "inputModel.Quantity");
                var quantity = cartLine.Quantity;

                var commerceCartLine = new CommerceCartLine(cartLine.CatalogName, cartLine.ProductId,
                    cartLine.VariantId == "-1" ? null : cartLine.VariantId, quantity);
                cartLineList.Add(commerceCartLine);
            }

            var request = new AddCartLinesRequest(cart, cartLineList);
            var cartResult = cartServiceProvider.AddCartLines(request);
            if (!cartResult.Success) cartResult.SystemMessages.LogSystemMessages(cartResult);

            return new ManagerResponse<CartResult, Cart>(cartResult, cartResult.Cart);
        }

        public ManagerResponse<CartResult, Cart> UpdateLineItemsInCart(Cart cart,
            IEnumerable<CartLineArgument> cartLines, string giftCardProductId, string giftCardPageLink)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLines, nameof(cartLines));

            var cartLineList = new List<CartLine>();
            foreach (var cartLine in cartLines)
            {
                Assert.ArgumentNotNullOrEmpty(cartLine.ProductId, "inputModel.ProductId");
                Assert.ArgumentNotNullOrEmpty(cartLine.CatalogName, "inputModel.CatalogName");
                Assert.ArgumentNotNull(cartLine.Quantity, "inputModel.Quantity");
                var quantity = cartLine.Quantity;

                bool Selector(CartLine x)
                {
                    var product = (CommerceCartProduct) x.Product;
                    return x.Product.ProductId == cartLine.ProductId &&
                           product.ProductVariantId == cartLine.VariantId &&
                           product.ProductCatalog == cartLine.CatalogName;
                }

                var commerceCartLine = cart.Lines.FirstOrDefault(Selector)
                                       ?? new CommerceCartLine(cartLine.CatalogName, cartLine.ProductId,
                                           cartLine.VariantId == "-1" ? null : cartLine.VariantId, quantity);
                commerceCartLine.Quantity = quantity;
                cartLineList.Add(commerceCartLine);
            }

            var request = new UpdateCartLinesRequest(cart, cartLineList);
            var updateCartResult = cartServiceProvider.UpdateCartLines(request);
            if (!updateCartResult.Success) updateCartResult.SystemMessages.LogSystemMessages(this);

            return new ManagerResponse<CartResult, Cart>(updateCartResult, updateCartResult.Cart);
        }

        public ManagerResponse<CartResult, Cart> RemoveLineItemsFromCart(Cart cart, IEnumerable<string> cartLineIds)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLineIds, nameof(cartLineIds));

            var cartLineList = new List<CartLine>();

            foreach (var cartLineId in cartLineIds)
            {
                var cartLine = cart.Lines.FirstOrDefault(line => line.ExternalCartLineId == cartLineId);
                cartLineList.Add(cartLine);
            }

            var request = new RemoveCartLinesRequest(cart, cartLineList);
            var serviceProviderResult = cartServiceProvider.RemoveCartLines(request);
            return new ManagerResponse<CartResult, Cart>(serviceProviderResult, serviceProviderResult.Cart);
        }

        public ManagerResponse<CartResult, Cart> GetCurrentCart(string shopName, string customerId)
        {
            var request = new LoadCartByNameRequest(shopName, Constants.DefaultCartName, customerId);

            var cartResult = cartServiceProvider.LoadCart(request);
            var stringList = new List<string>();

            if (cartResult.Cart is CommerceCart cart && cart.OrderForms.Count > 0)
                stringList.AddRange(cart.OrderForms[0].PromoCodes ?? Enumerable.Empty<string>());

            cartResult.Cart.GetProperties().Add("PromoCodes", stringList);

            return new ManagerResponse<CartResult, Cart>(cartResult, cartResult.Cart);
        }

        public ManagerResponse<CartResult, Cart> CreateOrResumeCart(string shopName, string userId, string customerId)
        {
            var request = new CreateOrResumeCartRequest(shopName, userId, Constants.DefaultCartName, customerId);
            var cartResult = cartServiceProvider.CreateOrResumeCart(request);

            return new ManagerResponse<CartResult, Cart>(cartResult, cartResult.Cart);
        }

        public ManagerResponse<CartResult, Cart> MergeCarts(string shopName, string customerId,
            string anonymousVisitorId, Cart anonymousVisitorCart)
        {
            Assert.ArgumentNotNullOrEmpty(anonymousVisitorId, "anonymousVisitorId");
            var request = new LoadCartByNameRequest(shopName, Constants.DefaultCartName, customerId);
            var cartResult = cartServiceProvider.LoadCart(request);

            if (!cartResult.Success || cartResult.Cart == null)
            {
                Log.Warn("Cart Not Found Error", GetType());
                return new ManagerResponse<CartResult, Cart>(cartResult, cartResult.Cart);
            }

            var commerceCart = (CommerceCart) cartResult.Cart;
            var newCartResult = new CartResult
            {
                Cart = commerceCart,
                Success = true
            };
            if (customerId != anonymousVisitorId)
            {
                var flag = anonymousVisitorCart is CommerceCart &&
                           ((CommerceCart) anonymousVisitorCart).OrderForms.Any(of => of.PromoCodes.Any());
                if (anonymousVisitorCart != null && anonymousVisitorCart.Lines.Any() | flag &&
                    (commerceCart.ShopName == anonymousVisitorCart.ShopName ||
                     commerceCart.ExternalId != anonymousVisitorCart.ExternalId))
                    newCartResult =
                        cartServiceProvider.MergeCart(new MergeCartRequest(anonymousVisitorCart, commerceCart));
            }

            return new ManagerResponse<CartResult, Cart>(newCartResult, newCartResult.Cart);
        }

        public ManagerResponse<AddPromoCodeResult, Cart> AddPromoCode(Cart cart, string promoCode)
        {
            var commerceCart = (CommerceCart) cart;
            var request = new AddPromoCodeRequest(commerceCart, promoCode);
            var result = cartServiceProvider.AddPromoCode(request);

            return new ManagerResponse<AddPromoCodeResult, Cart>(result, result.Cart);
        }

        public ManagerResponse<CartResult, Cart> UpdateCart(string shopName, Cart currentCart, CartBase cartUpdate)
        {
            var request = new UpdateCartRequest(currentCart, cartUpdate);
            var serviceProviderResult = cartServiceProvider.UpdateCart(request);
            return new ManagerResponse<CartResult, Cart>(serviceProviderResult, serviceProviderResult.Cart);
        }

        public ManagerResponse<AddPaymentInfoResult, Cart> AddPaymentInfo(string shopName, Cart cart,
            PartyEntity billingPartyEntity, FederatedPaymentArgs federatedPaymentArgs)
        {
            var payments = new List<PaymentInfo>();
            cart = RemoveAllPaymentMethods(cart).Result;

            if (federatedPaymentArgs != null && !string.IsNullOrEmpty(federatedPaymentArgs.CardToken) &&
                billingPartyEntity != null)
            {
                var commerceParty = connectEntityMapper.MapToCommerceParty(billingPartyEntity);
                commerceParty.PartyId = Guid.NewGuid().ToString().Replace("-", string.Empty);
                commerceParty.ExternalId = commerceParty.PartyId;
                if (string.IsNullOrWhiteSpace(commerceParty.Name))
                    commerceParty.Name = $"billing{commerceParty.PartyId}";
                cart.Parties.Add(commerceParty);
                var federatedPaymentInfo =
                    connectEntityMapper.MapToFederatedPaymentInfo(federatedPaymentArgs);
                federatedPaymentInfo.PartyID = commerceParty.PartyId;
                federatedPaymentInfo.Amount = cart.Total.Amount;
                payments.Add(federatedPaymentInfo);
            }

            var request = new AddPaymentInfoRequest(cart, payments);
            var paymentInfoResult = cartServiceProvider.AddPaymentInfo(request);
            if (!paymentInfoResult.Success) paymentInfoResult.SystemMessages.LogSystemMessages(paymentInfoResult);

            return new ManagerResponse<AddPaymentInfoResult, Cart>(paymentInfoResult, paymentInfoResult.Cart);
        }

        public ManagerResponse<AddShippingInfoResult, Cart> AddShippingInfo(Cart cart,
            List<PartyEntity> partyEntityList, ShippingOptionType shippingOptionType,
            List<ShippingInfoArgument> shippingInfoList)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(shippingOptionType, nameof(shippingOptionType));
            Assert.ArgumentNotNull(shippingInfoList, nameof(shippingInfoList));
            cart = RemoveAllShipmentFromCart(cart);

            if (partyEntityList != null && partyEntityList.Any())
            {
                var cartParties = cart.Parties.ToList();
                var commercePartyList = connectEntityMapper.MapToCommercePartyList(partyEntityList);
                cartParties.AddRange(commercePartyList);
                cart.Parties = cartParties;
            }

            if (shippingOptionType != ShippingOptionType.DeliverItemsIndividually)
                foreach (var shippingInfo in shippingInfoList)
                    shippingInfo.LineIds = cart.Lines.Select(lineItem => lineItem.ExternalCartLineId).ToList();

            var shippings = new List<ShippingInfo>();

            foreach (var shippingInfo in shippingInfoList)
                shippings.Add(connectEntityMapper.MapToCommerceShippingInfo(shippingInfo));

            var addShippingInfoRequest = new AddShippingInfoRequest(cart, shippings, shippingOptionType);

            var shippingInfoResult = cartServiceProvider.AddShippingInfo(addShippingInfoRequest);

            if (!shippingInfoResult.Success) shippingInfoResult.SystemMessages.LogSystemMessages(this);

            return new ManagerResponse<AddShippingInfoResult, Cart>(shippingInfoResult, shippingInfoResult.Cart);
        }

        public virtual ManagerResponse<CartResult, Cart> RemoveAllPaymentMethods(Cart cart)
        {
            RemovePaymentInfoResult paymentInfoResult = null;
            if (cart.Payment != null && cart.Payment.Any())
            {
                var request = new RemovePaymentInfoRequest(cart, cart.Payment);
                paymentInfoResult = cartServiceProvider.RemovePaymentInfo(request);
                return new ManagerResponse<CartResult, Cart>(paymentInfoResult, paymentInfoResult.Cart);
            }

            paymentInfoResult = new RemovePaymentInfoResult {Success = true};
            return new ManagerResponse<CartResult, Cart>(paymentInfoResult, cart);
        }


        private string GetProductLink(Item productItem, string productId, string giftCardProductId,
            string giftCardPageLink)
        {
            return !productId.Equals(giftCardProductId, StringComparison.OrdinalIgnoreCase)
                ? LinkManager.GetDynamicUrl(productItem)
                : giftCardPageLink;
        }

        private string GetImageLink(Item productItem)
        {
            var field = (MultilistField) productItem.Fields["Images"];
            if (field != null && field.TargetIDs.Any())
            {
                var mediaItem = (MediaItem) productItem.Database.GetItem(field.TargetIDs[0]);

                return mediaItem.ImageUrl(300, 300);
            }

            return string.Empty;
        }

        protected virtual Cart RemoveAllShipmentFromCart(Cart cart)
        {
            if (cart.Shipping != null && cart.Shipping.Any())
            {
                var list = cart.Parties.ToList();

                foreach (var shippingInfo in cart.Shipping)
                {
                    var shipment = shippingInfo;
                    var party = list.Find(
                        cp => cp.PartyId.Equals(shipment.PartyID, StringComparison.OrdinalIgnoreCase));
                    if (party != null) list.Remove(party);
                }

                cart.Parties = list;
                cart.Shipping = null;
            }

            return cart;
        }
    }
}