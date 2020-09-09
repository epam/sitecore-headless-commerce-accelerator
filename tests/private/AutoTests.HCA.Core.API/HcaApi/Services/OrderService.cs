using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Order;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class OrderService : BaseHcaService
    {
        public const string ORDER_ENDPOINT = "orders";

        public OrderService(IHttpClientService hcaApiService) : base(hcaApiService)
        {
        }

        public HcaResponse<OrderResult> GetOrder(string confirmationId)
        {
            return ExecuteJsonRequest<OrderResult>($"{ORDER_ENDPOINT}/{confirmationId}", Method.GET);
        }
    }
}