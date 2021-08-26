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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Connect.Context.Catalog;
    using Connect.Context.Storefront;
    using Connect.Managers.Cart;
    using Connect.Managers.Inventory;

    using Context;

    using Converters.Cart;

    using DependencyInjection;
    using HCA.Foundation.Commerce.Services.Catalog;

    using Models.Entities.Cart;


    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Diagnostics;

    using CommerceConnect = Sitecore.Commerce.Engine.Connect.Entities;
    using Connect = Sitecore.Commerce.Entities.Carts;

    [Service(typeof(ICartService), Lifetime = Lifetime.Singleton)]
    public class CartService : ICartService
    {
        private readonly ICartConverter<Connect.Cart> cartConverter;

        private readonly ICartManager cartManager;

        private readonly IInventoryManager inventoryManager;

        private readonly ICatalogContext catalogContext;

        private readonly ICatalogService catalogService;


        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        public CartService(
            ICartConverter<Connect.Cart> cartConverter,
            ICartManager cartManager,
            IInventoryManager inventoryManager,
            ICatalogContext catalogContext,
            ICatalogService catalogService,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext)
        {
            Assert.ArgumentNotNull(cartConverter, nameof(cartConverter));
            Assert.ArgumentNotNull(cartManager, nameof(cartManager));
            Assert.ArgumentNotNull(inventoryManager, nameof(inventoryManager));
            Assert.ArgumentNotNull(catalogContext, nameof(catalogContext));
            Assert.ArgumentNotNull(catalogService, nameof(catalogService));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            this.cartConverter = cartConverter;
            this.cartManager = cartManager;
            this.inventoryManager = inventoryManager;
            this.catalogContext = catalogContext;
            this.catalogService = catalogService;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
        }

        public Result<Cart> GetCart()
        {
            // TODO: Cart caching

            var response = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.CurrentUser?.CustomerId ??
                this.visitorContext.ContactId);

            return this.BuildResult(response);
        }

        public Result<Cart> MergeCarts(string anonymousContactId)
        {
            Assert.ArgumentNotNull(anonymousContactId, nameof(anonymousContactId));

            var sourceCartResult = this.cartManager.LoadCart(this.storefrontContext.ShopName, anonymousContactId);

            var destinationCartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.CurrentUser?.CustomerId ??
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

            return this.ExecuteWithCart(
                (result, cart) =>
                {
                    var validateCartLineResult = this.ValidateCartLine(productId, variantId);

                    if (!validateCartLineResult.Success) return validateCartLineResult;

                    var response = this.cartManager.AddCartLines(cart,
                        new[]
                        {
                            new CommerceConnect.CommerceCartLine(
                            this.catalogContext.CatalogName,
                            productId,
                            variantId == "-1" ? null : variantId,
                            quantity)
                        });

                    return this.BuildResult(response);
                });
        }

        public Result<Cart> UpdateCartLine(string productId, string variantId, decimal quantity)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            return this.ExecuteWithCart(
                (result, cart) =>
                {
                    var cartLines = this.GetCartLinesByProduct(
                            cart?.Lines?.Cast<CommerceConnect.CommerceCartLine>(),
                            productId,
                            variantId)
                        .ToList();
                    cartLines.ForEach(cartLine => cartLine.Quantity = quantity);

                    var response = cartLines.Any()
                        ? quantity == 0
                            ? this.cartManager.RemoveCartLines(cart, cartLines)
                            : this.cartManager.UpdateCartLines(cart, cartLines)
                        : this.cartManager.AddCartLines(cart, cartLines);

                    return this.BuildResult(response);
                });

        }

        public Result<Cart> RemoveCartLine(string productId, string variantId)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));

            return this.ExecuteWithCart(
                (result, cart) =>
                {
                    var response = this.cartManager.RemoveCartLines(cart,
                            this.GetCartLinesByProduct(cart?.Lines?.Cast<CommerceConnect.CommerceCartLine>(),
                            productId,
                            variantId)
                        .ToList());

                    return this.BuildResult(response);
                });
        }
        private Result<Cart> BuildResult(CartResult cartResult)
        {
            var cart = this.cartConverter.Convert(cartResult?.Cart);
            return new Result<Cart>(cart, cartResult?.SystemMessages.Select(_ => _.Message).ToList());
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

        private Result<Cart> ExecuteWithCart(Func<Result<Cart>, Connect.Cart, Result<Cart>> action)
        {
            var result = new Result<Cart>();

            var loadCartResult = this.cartManager.LoadCart(
                this.storefrontContext.ShopName,
                this.visitorContext.CurrentUser?.CustomerId ??
                this.visitorContext.ContactId);

            return action(result, loadCartResult?.Cart);
        }

        //This method is performing additional checking for out of stock product status and ProductVariant existence before adding to cart.
        //By default this validation must be done on the commerce provider side, but since Sitecore Commerce doesn't return any error codes
        //for that kind of issues, this pre-adding validation was created.
        private Result<Cart> ValidateCartLine(string productId, string variantId)
        {
            var productResult = this.catalogService.GetProduct(productId);

            if (productResult != null && !productResult.Success)
            {

                var errorList = new List<string>();
                errorList.AddRange(productResult.Errors);
                return new Result<Cart>(new Cart(), errorList);
            }

            if (productResult != null && !productResult.Data.Variants.Any(variant => variant.VariantId == variantId))
            {
                return new Result<Cart>(new Cart(), new List<string>() { "VariantId Not Found." });
            }

            var stockStatus = this.inventoryManager.GetStockInformation(this.storefrontContext.ShopName,
                new List<CommerceConnect.CommerceInventoryProduct>
                {
                    new CommerceConnect.CommerceInventoryProduct
                    {
                        ProductId = productId,
                        VariantId = variantId,
                        CatalogName = this.catalogContext.CatalogName
                    }
                },
                Sitecore.Commerce.Entities.Inventory.StockDetailsLevel.StatusAndAvailability);

            if (stockStatus != null
                && stockStatus.Success
                && stockStatus.StockInformation.Any()
                && stockStatus.StockInformation.FirstOrDefault().Status.Name == "OutOfStock")
            {
                return new Result<Cart>(new Cart(), new List<string>() { "Product which is out of stock can't be added to cart" });
            }

            return new Result<Cart>();
        }
    }
}