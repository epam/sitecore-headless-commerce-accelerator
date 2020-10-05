using System;
using System.Net;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.HcaApi.Services;
using AutoTests.HCA.Core.API.HcaApi.Settings;

namespace AutoTests.HCA.Core.API.HcaApi.Context
{
    public class HcaApiContext : IHcaApiContext
    {
        public const string AUTHORIZATION_COOKIE_NAME = ".AspNet.Cookies";
        private readonly HcaApiSettings _settings;
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
            _settings = apiSettings;
            var siteCoreCookie = _settings.GlobalAuthentication.SiteCoreCookie;
            var basicAuth = _settings.GlobalAuthentication.BasicAuthenticator;

            Client = new HttpClientService(new Uri(_settings.HcaApiUri));
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

        public CookieData GetHcaGlobalCookie()
        {
            return Client.GetCookieValueByName(_settings.GlobalAuthentication.SiteCoreCookie.Name);
        }

        public CookieData GetAuthorizationCookie()
        {
            return Client.GetCookieValueByName(AUTHORIZATION_COOKIE_NAME);
        }
    }
}