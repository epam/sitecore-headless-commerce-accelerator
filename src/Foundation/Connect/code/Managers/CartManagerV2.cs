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
    using System;
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Diagnostics;

    [Service(typeof(ICartManagerV2))]
    public class CartManagerV2 : ICartManagerV2
    {
        private readonly CommerceCartServiceProvider cartServiceProvider;
        private readonly ILogService<CommonLog> logService;

        public CartManagerV2(ILogService<CommonLog> logService, IConnectServiceProvider connectServiceProvider)
        {
            Assert.ArgumentNotNull(logService, nameof(logService));
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.logService = logService;
            this.cartServiceProvider = connectServiceProvider.GetCommerceCartServiceProvider();
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

        private CartResult Execute<TRequest>(TRequest request, Func<TRequest, CartResult> action)
            where TRequest : CartRequest
        {
            var cartResult = action.Invoke(request);
            if (!cartResult.Success)
            {
                foreach (var systemMessage in cartResult.SystemMessages)
                {
                    this.logService.Error(systemMessage.Message);
                }
            }

            return cartResult;
        }
    }
}