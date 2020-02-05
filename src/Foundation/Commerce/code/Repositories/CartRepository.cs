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
    using System.Linq;
    using Connect.Managers;
    using Connect.Models;
    using Context;
    using DependencyInjection;
    using ModelInitilizers;
    using ModelMappers;
    using Models;
    using Models.Checkout;
    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Services.Carts;

    [Service(typeof(ICartRepository))]
    public class CartRepository : BaseCheckoutRepository, ICartRepository
    {
        public CartRepository(ICartManager cartManager, ICatalogRepository catalogRepository,
            IAccountManager accountManager, ICartModelBuilder cartModelBuilder, IEntityMapper entityMapper,
            IStorefrontContext storefrontContext, IVisitorContext visitorContext)
            : base(cartManager, catalogRepository, accountManager, cartModelBuilder, entityMapper, storefrontContext,
                visitorContext)
        {
        }

        public virtual CartModel GetCurrentCart()
        {
            ManagerResponse<CartResult, Cart> cart =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);
            // ManagerResponse<CartResult, Cart> cart = this.CartManager.CreateOrResumeCart(this.StorefrontContext.ShopName,this.VisitorContext.CurrentUser.ContactId,  this.VisitorContext.ContactId);

            Result<CartModel> result = GetCart(cart.ServiceProviderResult, cart.Result);

            if (result.Success) return result.Data;

            return null;
        }

        public virtual void MergeCarts(string anonymousContactId)
        {
            ManagerResponse<CartResult, Cart> cart =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, anonymousContactId);
            if (cart.ServiceProviderResult.Success)
                CartManager.MergeCarts(
                    StorefrontContext.ShopName,
                    VisitorContext.ContactId,
                    anonymousContactId,
                    cart.Result);
        }

        public Result<CartModel> AddProductVariantToCart(string productId, string variantId, decimal quantity)
        {
            ManagerResponse<CartResult, Cart> model =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);

            var cartLineArgument = new CartLineArgument
            {
                CatalogName = StorefrontContext.CatalogName,
                ProductId = productId,
                Quantity = quantity,
                VariantId = variantId
            };

            ManagerResponse<CartResult, Cart> cart =
                CartManager.AddLineItemsToCart(model.Result, new[] {cartLineArgument}, null, null);

            Result<CartModel> result = GetCart(cart.ServiceProviderResult, cart.Result);

            return result;
        }

        public Result<CartModel> UpdateProductVariantQuantity(string productId, string variantId, decimal quantity)
        {
            ManagerResponse<CartResult, Cart> model =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);

            CartLine cartLine = model.Result.Lines.FirstOrDefault(x =>
            {
                var current = x.Product as CommerceCartProduct;
                return current?.ProductId == productId && current?.ProductVariantId == variantId;
            });

            if (cartLine != null)
            {
                if (quantity <= 0) return RemoveProductVariantFromCart(cartLine.ExternalCartLineId);

                var cartLineArgument = new CartLineArgument
                {
                    CatalogName = StorefrontContext.CatalogName,
                    ProductId = productId,
                    Quantity = quantity,
                    VariantId = variantId
                };

                ManagerResponse<CartResult, Cart> cart =
                    CartManager.UpdateLineItemsInCart(model.Result, new[] {cartLineArgument}, null, null);
                Result<CartModel> result = GetCart(cart.ServiceProviderResult, cart.Result);
                return result;
            }

            if (quantity > 0) return AddProductVariantToCart(productId, variantId, quantity);

            return null;
        }

        public Result<CartModel> RemoveProductVariantFromCart(string cartLineId)
        {
            ManagerResponse<CartResult, Cart> model =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);

            ManagerResponse<CartResult, Cart> cart =
                CartManager.RemoveLineItemsFromCart(model.Result, new[] {cartLineId});

            Result<CartModel> result = GetCart(cart.ServiceProviderResult, cart.Result);

            return result;
        }

        public Result<CartModel> AddPromoCode(string promoCode)
        {
            ManagerResponse<CartResult, Cart> getCartResponse =
                CartManager.GetCurrentCart(StorefrontContext.ShopName, VisitorContext.ContactId);
            ManagerResponse<AddPromoCodeResult, Cart> addPromoCodeResponse =
                CartManager.AddPromoCode(getCartResponse.Result, promoCode);
            Result<CartModel> result = GetCart(addPromoCodeResponse.ServiceProviderResult, addPromoCodeResponse.Result);

            return result;
        }
    }
}