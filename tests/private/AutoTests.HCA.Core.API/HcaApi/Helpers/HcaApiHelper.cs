using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Promotions;

namespace AutoTests.HCA.Core.API.HcaApi.Helpers
{
    public class HcaApiHelper : IHcaApiHelper
    {
        protected readonly IHcaApiContext ApiContext;

        public HcaApiHelper(IHcaApiContext apiContext)
        {
            ApiContext = apiContext;
        }

        public void CleanCart()
        {
            var res = ApiContext.Cart.GetCart();

            res.CheckSuccessfulResponse();

            var cartIsNotEmpty = res?.OkResponseData?.Data?.CartLines?.Any();
            if (!cartIsNotEmpty.HasValue || !cartIsNotEmpty.Value) return;

            foreach (var cartLine in res.OkResponseData.Data.CartLines)
                ApiContext.Cart.RemoveCartLine(cartLine.Product.ProductId, cartLine.Variant.VariantId)
                    .CheckSuccessfulResponse();
        }

        public void AddProductToCart(ProductTestsDataSettings product)
        {
            AddProductToCart(product.ProductId, 1, product.VariantId);
        }

        public void AddProductToCart(string productId, int quantity, string variantId)
        {
            ApiContext.Cart.AddCartLines(new CartLinesRequest(productId, quantity, variantId))
                .CheckSuccessfulResponse();
        }

        public void AddProductsToCart(IEnumerable<CartLinesRequest> products)
        {
            foreach (var product in products) ApiContext.Cart.AddCartLines(product).CheckSuccessfulResponse();
        }

        public void AddProductsToCart(IEnumerable<ProductTestsDataSettings> products)
        {
            foreach (var product in products) AddProductToCart(product);
        }

        public void AddPromotion(HcaPromotionTestsDataSettings promotion)
        {
            ApiContext.Cart.AddPromoCode(new PromoCodeRequest(promotion.Code)).CheckSuccessfulResponse();
        }

        public void CleanPromotion(HcaPromotionTestsDataSettings promotion)
        {
            ApiContext.Cart.RemovePromoCode(new PromoCodeRequest(promotion.DisplayCartText))
                .CheckSuccessfulResponse();
        }

        public void CleanPromotions()
        {
            var res = ApiContext.Cart.GetCart();

            //TODO did the request pass

            var adjustmentsNotNull = res?.OkResponseData?.Data?.Adjustments?.Any();

            if (!adjustmentsNotNull.HasValue || !adjustmentsNotNull.Value) return;

            foreach (var promotion in res.OkResponseData.Data.Adjustments)
                ApiContext.Cart.RemovePromoCode(new PromoCodeRequest(promotion));
        }
    }
}