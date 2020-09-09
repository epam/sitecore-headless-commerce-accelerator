using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Services;

namespace AutoTests.HCA.Core.API.HcaApi.Context
{
    public interface IHcaApiContext
    {
        public IHttpClientService Client { get; }

        public AuthService Auth { get; }
        public AccountService Account { get; }
        public CartService Cart { get; }
        public OrderService Order { get; }
        public ProductService Product { get; }
        public WishListService WishList { get; }
        public CheckoutService Checkout { get; }
    }
}