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

        public Result<Cart> MergeCarts(string anonymousContactId)
        {
            Assert.ArgumentNotNull(anonymousContactId, nameof(anonymousContactId));

            var sourceCartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, anonymousContactId);
            var destinationCartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            if (!sourceCartResult.Success || !destinationCartResult.Success)
            {
                return this.entityMapper.Map<Result<Cart>, CartResult>(
                    sourceCartResult.Success ? destinationCartResult : sourceCartResult);
            }

            var response = this.cartManager.MergeCarts(sourceCartResult.Cart, destinationCartResult.Cart);
            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }
        
        public Result<Cart> AddCartLine(string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));
            Assert.ArgumentNotNull(quantity, nameof(quantity));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLine = new Connect.CommerceCartLine(this.storefrontContext.CatalogName, productId, variantId == "-1" ? null : variantId, quantity);

            var response = this.cartManager.AddCartLines(cartResult?.Cart, new[] { cartLine });

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> UpdateCartLine(string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));
            Assert.ArgumentNotNull(quantity, nameof(quantity));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLines = this.GetCartLinesByProduct(cartResult?.Cart?.Lines?.Cast<Connect.CommerceCartLine>(), productId, variantId).ToList();

            cartLines.ForEach(cartLine => cartLine.Quantity = quantity);

            var response = cartLines.Any()
                ? quantity == 0 
                ? this.cartManager.RemoveCartLines(cartResult?.Cart, cartLines)
                : this.cartManager.UpdateCartLines(cartResult?.Cart, cartLines)
                : this.cartManager.AddCartLines(cartResult?.Cart, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemoveCartLine(string productId, string variantId)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLines = this.GetCartLinesByProduct(cartResult?.Cart?.Lines?.Cast<Connect.CommerceCartLine>(), productId, variantId).ToList();
                    
            var response = this.cartManager.RemoveCartLines(cartResult?.Cart, cartLines);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> AddPromoCode(string promoCode)
        {
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var response = this.cartManager.AddPromoCode(cartResult?.Cart as Connect.CommerceCart, promoCode);

            return this.entityMapper.Map<Result<Cart>, CartResult>(response);
        }

        public Result<Cart> RemovePromoCode(string promoCode)
        {
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var response = this.cartManager.RemovePromoCode(cartResult?.Cart as Connect.CommerceCart, promoCode);

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