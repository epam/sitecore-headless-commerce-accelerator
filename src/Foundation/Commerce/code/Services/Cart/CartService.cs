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
    using System.Collections.Generic;
    using Connect.Managers;
    using Context;
    using DependencyInjection;
    using ModelMappers;
    using Models;
    using Models.Entities;
    using Sitecore.Diagnostics;
    using Commerce = Sitecore.Commerce.Entities.Carts;

    using CartResult = Sitecore.Commerce.Services.Carts.CartResult;

    [Service(typeof(ICartService), Lifetime = Lifetime.Singleton)]
    public class CartService : ICartService
    {
        private readonly ICartManagerV2 cartManager;
        private readonly IEntityMapper entityMapper;
        private readonly IStorefrontContext storefrontContext;
        private readonly IVisitorContext visitorContext;

        public CartService(
            ICartManagerV2 cartManager,
            IEntityMapper entityMapper,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
        {
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(entityMapper, nameof(entityMapper));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.cartManager = cartManager;
            this.entityMapper = entityMapper;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
        }

        public Result<Cart> GetCart()
        {
            var response = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> MergeCarts(Cart source, Cart destination)
        {
            var fromCart = this.entityMapper.Map<Commerce.Cart, Cart>(source);
            var toCart = this.entityMapper.Map<Commerce.Cart, Cart>(destination);

            var response = this.cartManager.MergeCarts(fromCart, toCart);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }
        
        public Result<Cart> AddCartLine(Cart cart, CartLine cartLine)
        {
            var cartModel = this.entityMapper.Map<Commerce.Cart, Cart>(cart);
            var cartLines = this.entityMapper.Map<IEnumerable<Commerce.CartLine>, IEnumerable<CartLine>>(new[] { cartLine });

            var response = this.cartManager.AddCartLines(cartModel, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> UpdateCartLine(Cart cart, CartLine cartLine)
        {
            var cartModel = this.entityMapper.Map<Commerce.Cart, Cart>(cart);
            var cartLines = this.entityMapper.Map<IEnumerable<Commerce.CartLine>, IEnumerable<CartLine>>(new[] { cartLine });

            var response = this.cartManager.RemoveCartLines(cartModel, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemoveCartLine(Cart cart, CartLine cartLine)
        {
            var cartModel = this.entityMapper.Map<Commerce.Cart, Cart>(cart);
            var cartLines = this.entityMapper.Map<IEnumerable<Commerce.CartLine>, IEnumerable<CartLine>>(new[] { cartLine });

            var response = this.cartManager.RemoveCartLines(cartModel, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> AddPromoCode(Cart cart, string promoCode)
        {
            var cartModel = this.entityMapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceCart, Cart>(cart);

            var response = this.cartManager.AddPromoCode(cartModel, promoCode);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemovePromoCode(Cart cart, string promoCode)
        {
            var cartModel = this.entityMapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceCart, Cart>(cart);

            var response = this.cartManager.RemovePromoCode(cartModel, promoCode);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }
    }
}