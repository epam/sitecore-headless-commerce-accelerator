using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class CartService : BaseHcaService
    {
        public const string CART_ENDPOINT = "carts/cart";
        public const string CART_LINES_ENDPOINT = "carts/cartLines";
        public const string PROMOTIONS_ENDPOINT = "carts/promoCodes";

        public CartService(IHttpClientService hcaApiService) : base(hcaApiService)
        {
        }

        public HcaResponse<CartResult> AddCartLines(CartLinesRequest cartLine)
        {
            return ExecuteJsonRequest<CartResult>(CART_LINES_ENDPOINT, Method.POST, cartLine);
        }

        public HcaResponse<CartResult> UpdateCartLines(CartLinesRequest cartLine)
        {
            return ExecuteJsonRequest<CartResult>(CART_LINES_ENDPOINT, Method.PUT, cartLine);
        }

        public HcaResponse<CartResult> GetCart()
        {
            return ExecuteJsonRequest<CartResult>(CART_ENDPOINT, Method.GET);
        }

        public HcaResponse<CartResult> RemoveCartLine(string productId, string variantId)
        {
            return ExecuteJsonRequest<CartResult>(CART_LINES_ENDPOINT + $"?productId={productId}&variantId={variantId}",
                Method.DELETE);
        }

        public HcaResponse<CartResult> AddPromoCode(PromoCodeRequest promoCode)
        {
            return ExecuteJsonRequest<CartResult>(PROMOTIONS_ENDPOINT, Method.POST, promoCode);
        }

        public HcaResponse<CartResult> RemovePromoCode(PromoCodeRequest promoCode)
        {
            return ExecuteJsonRequest<CartResult>(PROMOTIONS_ENDPOINT, Method.DELETE, promoCode);
        }
    }
}