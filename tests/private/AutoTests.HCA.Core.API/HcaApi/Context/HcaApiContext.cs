using System;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Services;
using AutoTests.HCA.Core.API.HcaApi.Settings;

namespace AutoTests.HCA.Core.API.HcaApi.Context
{
    public class HcaApiContext : IHcaApiContext
    {
        private AccountService _account;

        private AuthService _authService;
        private CartService _cartService;
        private CheckoutService _checkoutService;
        private OrderService _orderService;
        private ProductService _productService;
        private WishListService _wishListService;
        private StoreLocatorService _storeLocatorService;

        public HcaApiContext(HcaApiSettings apiSettings)
        {
            var siteCoreCookie = apiSettings.GlobalAuthentication.SiteCoreCookie;
            var basicAuth = apiSettings.GlobalAuthentication.BasicAuthenticator;

            Client = new HttpClientService(new Uri(apiSettings.HcaApiUri));
            Client.SetCookieIfNotSet(siteCoreCookie);

            if (basicAuth.IsRequired)
                Client.SetHttpBasicAuthenticator(basicAuth.Account.UserName, basicAuth.Account.Password);
        }

        public IHttpClientService Client { get; }

        public AuthService Auth => _authService ??= new AuthService(Client);
        public AccountService Account => _account ??= new AccountService(Client);
        public CartService Cart => _cartService ??= new CartService(Client);
        public OrderService Order => _orderService ??= new OrderService(Client);
        public ProductService Product => _productService ??= new ProductService(Client);
        public WishListService WishList => _wishListService ??= new WishListService(Client);
        public CheckoutService Checkout => _checkoutService ??= new CheckoutService(Client);
        public StoreLocatorService StoreLocator => _storeLocatorService ??= new StoreLocatorService(Client);
    }
}