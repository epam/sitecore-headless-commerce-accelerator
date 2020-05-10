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

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Mappers.Cart;

    using Models;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Diagnostics;

    using AddShippingInfoRequest = Sitecore.Commerce.Engine.Connect.Services.Carts.AddShippingInfoRequest;

    [Service(typeof(ICartManager), Lifetime = Lifetime.Singleton)]
    public class CartManager : BaseManager, ICartManager
    {
        private readonly CommerceCartServiceProvider cartServiceProvider;

        private readonly ICartMapper cartMapper;

        public CartManager(
            ILogService<CommonLog> logService,
            IConnectServiceProvider connectServiceProvider,
            ICartMapper cartMapper)
            : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));
            Assert.ArgumentNotNull(cartMapper, nameof(cartMapper));

            this.cartServiceProvider = connectServiceProvider.GetCommerceCartServiceProvider();
            this.cartMapper = cartMapper;
        }

        public CartResult LoadCart(string shopName, string customerId)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));

            return this.Execute(
                new LoadCartByNameRequest(shopName, Constants.DefaultCartName, customerId),
                this.cartServiceProvider.LoadCart);
        }

        public CartResult CreateOrResumeCart(string shopName, string userId, string customerId)
        {
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));
            Assert.ArgumentNotNullOrEmpty(userId, nameof(userId));
            Assert.ArgumentNotNullOrEmpty(customerId, nameof(customerId));

            return this.Execute(
                new CreateOrResumeCartRequest(shopName, userId),
                this.cartServiceProvider.CreateOrResumeCart);
        }

        public CartResult AddCartLines(Cart cart, IEnumerable<CartLine> cartLines)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLines, nameof(cartLines));

            return this.Execute(new AddCartLinesRequest(cart, cartLines), this.cartServiceProvider.AddCartLines);
        }

        public AddPaymentInfoResult AddPaymentInfo(
            Cart cart,
            Party party,
            FederatedPaymentInfo federatedPaymentInfo)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(party, nameof(party));
            Assert.ArgumentNotNull(federatedPaymentInfo, nameof(federatedPaymentInfo));

            var commerceParty = this.cartMapper.Map<Party, CommerceParty>(party);

            federatedPaymentInfo.PartyID = party.PartyId;
            federatedPaymentInfo.Amount = cart.Total.Amount;

            cart.Parties.Add(commerceParty);

            var payments = new List<PaymentInfo>
            {
                federatedPaymentInfo
            };

            return this.Execute(new AddPaymentInfoRequest(cart, payments), this.cartServiceProvider.AddPaymentInfo);
        }

        public AddShippingInfoResult AddShippingInfo(
            Cart cart,
            ShippingOptionType shippingOptionType,
            List<ShippingInfo> shippings)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(shippingOptionType, nameof(shippingOptionType));
            Assert.ArgumentNotNull(shippings, nameof(shippings));

            return this.Execute(
                new AddShippingInfoRequest(cart, shippings, shippingOptionType),
                this.cartServiceProvider.AddShippingInfo);
        }

        public CartResult UpdateCart(Cart cart, CartBase cartUpdate)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartUpdate, nameof(cartUpdate));

            return this.Execute(new UpdateCartRequest(cart, cartUpdate), this.cartServiceProvider.UpdateCart);
        }

        public CartResult UpdateCartLines(Cart cart, IEnumerable<CartLine> cartLines)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLines, nameof(cartLines));

            return this.Execute(new UpdateCartLinesRequest(cart, cartLines), this.cartServiceProvider.UpdateCartLines);
        }

        public CartResult RemoveCartLines(Cart cart, IEnumerable<CartLine> cartLines)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(cartLines, nameof(cartLines));

            return this.Execute(new RemoveCartLinesRequest(cart, cartLines), this.cartServiceProvider.RemoveCartLines);
        }

        public CartResult MergeCarts(Cart fromCart, Cart toCart)
        {
            Assert.ArgumentNotNull(fromCart, nameof(fromCart));
            Assert.ArgumentNotNull(toCart, nameof(toCart));

            return this.Execute(new MergeCartRequest(fromCart, toCart), this.cartServiceProvider.MergeCart);
        }

        public CartResult AddPromoCode(CommerceCart cart, string promoCode)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNullOrEmpty(promoCode, nameof(promoCode));

            return this.Execute(new AddPromoCodeRequest(cart, promoCode), this.cartServiceProvider.AddPromoCode);
        }

        public CartResult RemovePromoCode(CommerceCart cart, string promoCode)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNullOrEmpty(promoCode, nameof(promoCode));

            return this.Execute(new RemovePromoCodeRequest(cart, promoCode), this.cartServiceProvider.RemovePromoCode);
        }

        public CartResult RemovePaymentInfo(Cart cart)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));

            return this.Execute(
                new RemovePaymentInfoRequest(cart, cart.Payment),
                this.cartServiceProvider.RemovePaymentInfo);
        }
    }
}