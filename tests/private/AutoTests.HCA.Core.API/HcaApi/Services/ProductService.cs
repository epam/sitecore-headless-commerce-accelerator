using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Search;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class ProductService : BaseHcaService
    {
        public const string PRODUCTS_ENDPOINT = "search/products";

        public ProductService(IHttpClientService httpClientService) : base(httpClientService)
        {
        }

        public HcaResponse<ProductSearchResult> SearchProducts(ProductSearchOptionsRequest product)
        {
            return ExecuteJsonRequest<ProductSearchResult>(PRODUCTS_ENDPOINT, Method.POST, product);
        }
    }
}