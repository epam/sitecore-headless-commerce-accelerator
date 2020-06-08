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

namespace HCA.Foundation.Commerce.Services.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Builders.Cart;

    using Connect.Context.Catalog;
    using Connect.Context.Storefront;
    using Connect.Managers.Cart;

    using Context;

    using DependencyInjection;

    using Models.Entities.Cart;

    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Diagnostics;

    using CommerceConnect = Sitecore.Commerce.Engine.Connect.Entities;
    using Connect = Sitecore.Commerce.Entities.Carts;

    [Service(typeof(ICartService), Lifetime = Lifetime.Singleton)]
    public class CartService : ICartService
    {
        private readonly ICartManager cartManager;

        private readonly ICatalogContext catalogContext;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        private readonly ICartBuilder<Connect.Cart> cartBuilder;

        public CartService(
            ICartManager cartManager,
            IStorefrontContext storefrontContext,
            ICatalogContext catalogContext,
            IVisitorContext visitorContext,
            ICartBuilder<Connect.Cart> cartBuilder)
        {
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(catalogContext, nameof(catalogContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));
            Assert.ArgumentNotNull(cartBuilder, nameof(cartBuilder));

            this.cartManager = cartManager;
            this.storefrontContext = storefrontContext;
            this.catalogContext = catalogContext;
            this.visitorContext = visitorContext;
            this.cartBuilder = cartBuilder;
        }

        public Result<Cart> GetCart()
        {
            // TODO: Cart caching
            var response = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            return this.BuildResult(response);
        }

        public Result<Cart> MergeCarts(string anonymousContactId)
        {
            Assert.ArgumentNotNull(anonymousContactId, nameof(anonymousContactId));

            var sourceCartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, anonymousContactId);
            var destinationCartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.ContactId);
            if (!sourceCartResult.Success || !destinationCartResult.Success)
            {
                return this.BuildResult(sourceCartResult.Success ? destinationCartResult : sourceCartResult);
            }

            var response = this.cartManager.MergeCarts(sourceCartResult.Cart, destinationCartResult.Cart);
            return this.BuildResult(response);
        }

        public Result<Cart> AddCartLine(string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLine = new CommerceConnect.CommerceCartLine(
                this.catalogContext.CatalogName,
                productId,
                variantId == "-1" ? null : variantId,
                quantity);

            var response = this.cartManager.AddCartLines(cartResult?.Cart, new[] { cartLine });

            return this.BuildResult(response);
        }

        public Result<Cart> UpdateCartLine(string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLines = this.GetCartLinesByProduct(
                    cartResult?.Cart?.Lines?.Cast<CommerceConnect.CommerceCartLine>(),
                    productId,
                    variantId)
                .ToList();

            // TODO: this condition used because for any REMOVE action on FE used UPDATE controller method with 0 quantity. It should be refactored.
            // If remove this line now, results in analytics table will look like "Items removed: 0"
            if (quantity != 0)
            {
                cartLines.ForEach(cartLine => cartLine.Quantity = quantity);
            }

            var response = cartLines.Any()
                ? quantity == 0
                    ? this.cartManager.RemoveCartLines(cartResult?.Cart, cartLines)
                    : this.cartManager.UpdateCartLines(cartResult?.Cart, cartLines)
                : this.cartManager.AddCartLines(cartResult?.Cart, cartLines);

            return this.BuildResult(response);
        }

        public Result<Cart> RemoveCartLine(string productId, string variantId)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var cartLines = this.GetCartLinesByProduct(
                    cartResult?.Cart?.Lines?.Cast<CommerceConnect.CommerceCartLine>(),
                    productId,
                    variantId)
                .ToList();

            var response = this.cartManager.RemoveCartLines(cartResult?.Cart, cartLines);

            return this.BuildResult(response);
        }

        public Result<Cart> AddPromoCode(string promoCode)
        {
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var response = this.cartManager.AddPromoCode(cartResult?.Cart as CommerceConnect.CommerceCart, promoCode);

            return this.BuildResult(response);
        }

        public Result<Cart> RemovePromoCode(string promoCode)
        {
            Assert.ArgumentNotNull(promoCode, nameof(promoCode));

            var cartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, this.visitorContext.ContactId);
            var response = this.cartManager.RemovePromoCode(
                cartResult?.Cart as CommerceConnect.CommerceCart,
                promoCode);

            return this.BuildResult(response);
        }

        private IEnumerable<CommerceConnect.CommerceCartLine> GetCartLinesByProduct(
            IEnumerable<CommerceConnect.CommerceCartLine> cartLines,
            string productId,
            string variantId)
        {
            return cartLines
                .Where(
                    cartLine =>
                    {
                        var product = cartLine.Product as CommerceConnect.CommerceCartProduct;
                        return product?.ProductId == productId && product?.ProductVariantId == variantId;
                    });
        }

        private Result<Cart> BuildResult(CartResult cartResult)
        {
            var cart = this.cartBuilder.Build(cartResult?.Cart);
            return new Result<Cart>(cart, cartResult?.SystemMessages.Select(_ => _.Message).ToList());
        }
    }
}