using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.StoreLocator;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class StoreLocatorService : BaseHcaService
    {
        public const string STORES_ENDPOINT = "storelocator/stores";
        public const string SEARCH_STORES_ENDPOINT = "storelocator/search";

        public StoreLocatorService(IHttpClientService httpClientService) : base(httpClientService) { }

        public HcaResponse<SearchByGeolocationResult> GetStores()
        {
            return ExecuteJsonRequest<SearchByGeolocationResult>(STORES_ENDPOINT, Method.GET);
        }

        public HcaResponse<SearchByGeolocationResult> SearchLocations(StoreLocationRequest coordinate)
        {
            var path = $"{SEARCH_STORES_ENDPOINT}?" +
                       $"{nameof(coordinate.Lat)}=" + coordinate?.Lat +
                       $"&{nameof(coordinate.Lng)}=" + coordinate?.Lng +
                       $"&{nameof(coordinate.Radius)}=" + coordinate?.Radius;

            return ExecuteJsonRequest<SearchByGeolocationResult>(path, Method.GET);
        }
    }
}
