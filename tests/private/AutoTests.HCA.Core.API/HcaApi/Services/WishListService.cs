using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.WishList;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class WishListService : BaseHcaService
    {
        public const string WISH_LIST_ENDPOINT = "wishlist";

        public WishListService(IHttpClientService client) : base(client)
        {
        }

        public HcaResponse<WishListResult> AddWishListLine(VariantRequest variant)
        {
            return ExecuteJsonRequest<WishListResult>(WISH_LIST_ENDPOINT, Method.POST, variant);
        }

        public HcaResponse<WishListResult> GetWishList()
        {
            return ExecuteJsonRequest<WishListResult>(WISH_LIST_ENDPOINT, Method.GET);
        }

        public HcaResponse<WishListResult> RemoveWishListLine(string variantId)
        {
            return ExecuteJsonRequest<WishListResult>(WISH_LIST_ENDPOINT + $"?variantId={variantId}", Method.DELETE);
        }
    }
}