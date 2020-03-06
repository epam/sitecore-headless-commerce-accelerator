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
    using System.Linq;
    using Connect.Managers;
    using Context;
    using DependencyInjection;
    using ModelMappers;
    using Models;
    using Models.Entities;
    using Sitecore.Diagnostics;
    using Connect = Sitecore.Commerce.Engine.Connect.Entities;

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
            //TODO: Cart caching 
            var response = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> MergeCarts(Cart source, Cart destination)
        {
            Assert.ArgumentNotNull(source, nameof(source));
            Assert.ArgumentNotNull(destination, nameof(destination));

            var fromCart = this.entityMapper.Map<Connect.CommerceCart, Cart>(source);
            var toCart = this.entityMapper.Map<Connect.CommerceCart, Cart>(destination);

            var response = this.cartManager.MergeCarts(fromCart, toCart);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }
        
        public Result<Cart> AddCartLine(Cart cart, string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));
            Assert.ArgumentNotNull(quantity, nameof(quantity));

            var cartModel = this.entityMapper.Map<Connect.CommerceCart, Cart>(cart);
            var cartLine = new Connect.CommerceCartLine(this.storefrontContext.CatalogName, productId, variantId == "-1" ? null : variantId, quantity);

            var response = this.cartManager.AddCartLines(cartModel, new[] { cartLine });

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> UpdateCartLine(Cart cart, string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));
            Assert.ArgumentNotNull(quantity, nameof(quantity));

            var cartModel = this.entityMapper.Map<Connect.CommerceCart, Cart>(cart);
            var cartLines = this.GetCartLinesByProduct(cartModel.Lines.Cast<Connect.CommerceCartLine>(), productId, variantId).ToList();

            var response = !cartLines.Any()
                ? this.cartManager.UpdateCartLines(cartModel, cartLines)
                : this.cartManager.AddCartLines(cartModel, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemoveCartLine(Cart cart, string productId, string variantId)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            var cartModel = this.entityMapper.Map<Connect.CommerceCart, Cart>(cart);
            var cartLines = this.GetCartLinesByProduct(cartModel.Lines.Cast<Connect.CommerceCartLine>(), productId, variantId).ToList();
                    
            var response = this.cartManager.RemoveCartLines(cartModel, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> AddPromoCode(Cart cart, string promoCode)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartModel = this.entityMapper.Map<Connect.CommerceCart, Cart>(cart);

            var response = this.cartManager.AddPromoCode(cartModel, promoCode);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemovePromoCode(Cart cart, string promoCode)
        {
            Assert.ArgumentNotNull(cart, nameof(cart));
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartModel = this.entityMapper.Map<Connect.CommerceCart, Cart>(cart);

            var response = this.cartManager.RemovePromoCode(cartModel, promoCode);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        private IEnumerable<Connect.CommerceCartLine> GetCartLinesByProduct(IEnumerable<Connect.CommerceCartLine> cartLines, string productId, string variantId)
        {
            return cartLines
                .Where(
                    cartLine =>
                    {
                        var product = cartLine.Product as Connect.CommerceCartProduct;
                        return product?.ProductId == productId && product?.ProductVariantId == variantId;
                    });
        }
    }
}