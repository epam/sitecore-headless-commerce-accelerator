using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Services;

namespace AutoTests.HCA.Core.API.HcaApi.Context
{
    public interface IHcaApiContext
    {
        IHttpClientService Client { get; }

        AuthService Auth { get; }
        AccountService Account { get; }
        CartService Cart { get; }
        OrderService Order { get; }
        ProductService Product { get; }
        WishListService WishList { get; }
        CheckoutService Checkout { get; }
        StoreLocatorService StoreLocator { get; }
    }
}