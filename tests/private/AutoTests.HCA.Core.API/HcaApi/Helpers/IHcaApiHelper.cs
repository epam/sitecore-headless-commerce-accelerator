using System.Collections.Generic;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Promotions;

namespace AutoTests.HCA.Core.API.HcaApi.Helpers
{
    public interface IHcaApiHelper
    {
        public void CleanCart();

        public void AddProductToCart(string productId, int quantity, string variantId);

        public void AddProductsToCart(IEnumerable<CartLinesRequest> products);

        public void AddProductsToCart(IEnumerable<ProductTestsDataSettings> products);

        public void AddPromotion(HcaPromotionTestsDataSettings promotion);

        public void CleanPromotion(HcaPromotionTestsDataSettings promotion);

        public void CleanPromotions();
    }
}