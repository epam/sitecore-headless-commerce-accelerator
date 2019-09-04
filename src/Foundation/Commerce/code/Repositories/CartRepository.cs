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

namespace Wooli.Foundation.Commerce.Repositories
{
    using System;
    using System.Linq;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Carts;

    using Wooli.Foundation.Commerce.Context;
    using Wooli.Foundation.Commerce.ModelInitilizers;
    using Wooli.Foundation.Commerce.ModelMappers;
    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Connect.Managers;
    using Wooli.Foundation.Connect.Models;
    using Wooli.Foundation.DependencyInjection;

    [Service(typeof(ICartRepository))]
    public class CartRepository : BaseCheckoutRepository, ICartRepository
    {
        public CartRepository(ICartManager cartManager, ICatalogRepository catalogRepository, IAccountManager accountManager, ICartModelBuilder cartModelBuilder, IEntityMapper entityMapper, IStorefrontContext storefrontContext, IVisitorContext visitorContext)
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext, visitorContext)
        {
        }

        public virtual CartModel GetCurrentCart()
        {
           ManagerResponse<CartResult, Cart> cart = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);
           // ManagerResponse<CartResult, Cart> cart = this.CartManager.CreateOrResumeCart(this.StorefrontContext.ShopName,this.VisitorContext.CurrentUser.ContactId,  this.VisitorContext.ContactId);

            Result<CartModel> result = this.GetCart(cart.ServiceProviderResult, cart.Result);

            if (result.Success)
            {
                return result.Data;
            }

            return null;
        }

        public virtual void MergeCarts(string anonymousContactId)
        {
            ManagerResponse<CartResult, Cart> cart = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, anonymousContactId);
            if (cart.ServiceProviderResult.Success)
            {
                this.CartManager.MergeCarts(
                    this.StorefrontContext.ShopName,
                    this.VisitorContext.ContactId,
                    anonymousContactId,
                    cart.Result);
            }

        }

        public Result<CartModel> AddProductVariantToCart(string productId, string variantId, decimal quantity)
        {
            ManagerResponse<CartResult, Cart> model = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);

            var cartLineArgument = new CartLineArgument
            {
                CatalogName = this.StorefrontContext.CatalogName,
                ProductId = productId,
                Quantity = quantity,
                VariantId = variantId
            };

            ManagerResponse<CartResult, Cart> cart = this.CartManager.AddLineItemsToCart(model.Result, new[] { cartLineArgument }, null, null);

            Result<CartModel> result = this.GetCart(cart.ServiceProviderResult, cart.Result);

            return result;
        }

        public Result<CartModel> UpdateProductVariantQuantity(string productId, string variantId, decimal quantity)
        {
            ManagerResponse<CartResult, Cart> model = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);

            CartLine cartLine = model.Result.Lines.FirstOrDefault(x =>
                {
                    var current = x.Product as CommerceCartProduct;
                    return current?.ProductId == productId && current?.ProductVariantId == variantId;
                });

            if (cartLine != null)
            {
                if (quantity <= 0)
                {
                    return this.RemoveProductVariantFromCart(cartLine.ExternalCartLineId);
                }

                var cartLineArgument = new CartLineArgument
                {
                    CatalogName = this.StorefrontContext.CatalogName,
                    ProductId = productId,
                    Quantity = quantity,
                    VariantId = variantId
                };

                ManagerResponse<CartResult, Cart> cart = this.CartManager.UpdateLineItemsInCart(model.Result, new[] { cartLineArgument }, null, null);
                Result<CartModel> result = this.GetCart(cart.ServiceProviderResult, cart.Result);
                return result;
            }
            else if (quantity > 0)
            {
                return this.AddProductVariantToCart(productId, variantId, quantity);
            }

            return null;
        }

        public Result<CartModel> RemoveProductVariantFromCart(string cartLineId)
        {
            ManagerResponse<CartResult, Cart> model = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);

            ManagerResponse<CartResult, Cart> cart = this.CartManager.RemoveLineItemsFromCart(model.Result, new[] { cartLineId });

            Result<CartModel> result = this.GetCart(cart.ServiceProviderResult, cart.Result);

            return result;
        }

        public Result<CartModel> AddPromoCode(string promoCode)
        {
            var getCartResponse = this.CartManager.GetCurrentCart(this.StorefrontContext.ShopName, this.VisitorContext.ContactId);
            var addPromoCodeResponse = this.CartManager.AddPromoCode(getCartResponse.Result, promoCode);
            var result = this.GetCart(addPromoCodeResponse.ServiceProviderResult, addPromoCodeResponse.Result);

            return result;
        }
    }
}
