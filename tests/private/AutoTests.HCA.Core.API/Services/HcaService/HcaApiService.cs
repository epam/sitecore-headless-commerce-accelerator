using System;
using System.Collections.Generic;
using System.Net;
using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Billing;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Delivery;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Payment;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Shipping;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Order;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.ErrorResult;
using AutoTests.HCA.Core.API.Models.Hca.RequestResult.Results.OkResult;
using AutoTests.HCA.Core.API.Settings.Api;
using RestSharp;

namespace AutoTests.HCA.Core.API.Services.HcaService
{
    public class HcaApiService : IHcaApiService
    {
        private readonly Uri _baseUri;
        private readonly IHttpClientService _httpClientService;

        public HcaApiService(HcaApiSettings apiSettings)
        {
            _baseUri = new Uri(apiSettings.HcaApiUri);
            var siteCoreCookie = apiSettings.GlobalAuthentication.SiteCoreCookie;
            var basicAuth = apiSettings.GlobalAuthentication.BasicAuthenticator;

            _httpClientService = new HttpClientService(_baseUri);
            _httpClientService.SetCookieIfNotSet(siteCoreCookie.Name, siteCoreCookie.Value);

            if (basicAuth.IsRequired)
                _httpClientService.SetHttpBasicAuthenticator(basicAuth.Account.UserName, basicAuth.Account.Password);
        }

        public CookieCollection GetClientCookies()
        {
            return _httpClientService.GetCookies();
        }

        public HcaResponse<LoginResult> Login(LoginRequest loginData, string endpoint = "auth/login")
        {
            return ExecuteJsonRequest<LoginResult>(endpoint, Method.POST, loginData);
        }

        public HcaVoidResponse Logout(string endpoint = "auth/logout")
        {
            return ExecuteJsonRequest(endpoint, Method.POST);
        }

        public HcaVoidResponse ChangePassword(ChangePasswordRequest password, string endpoint = "accounts/password")
        {
            return ExecuteJsonRequest(endpoint, Method.PUT, password);
        }

        public HcaResponse<UserResult> CreateUserAccount(CreateAccountRequest newUser, string endpoint = "accounts/account")
        {
            return ExecuteJsonRequest<UserResult>(endpoint, Method.POST, newUser);
        }

        public HcaVoidResponse UpdateAccount(UpdateAccountRequest account, string endpoint = "accounts/account")
        {
            return ExecuteJsonRequest(endpoint, Method.PUT, account);
        }

        public HcaResponse<ValidateEmailResult> ValidateEmail(ValidateEmailRequest email, string endpoint = "/accounts/validate")
        {
            return ExecuteJsonRequest<ValidateEmailResult>(endpoint, Method.POST, email);
        }

        public HcaResponse<IEnumerable<Address>> GetAddresses(string endpoint = "accounts/address")
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(endpoint, Method.GET);
        }

        public HcaResponse<IEnumerable<Address>> RemoveAddress(string externalId, string endpoint = "accounts/address")
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(endpoint + $"?externalid={externalId}", Method.DELETE);
        }

        public HcaResponse<IEnumerable<Address>> UpdateAddress(Address newAddress, string endpoint = "accounts/address")
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(endpoint, Method.PUT, newAddress);
        }

        public HcaResponse<IEnumerable<Address>> AddAddress(Address newAddress, string endpoint = "accounts/address")
        {
            return ExecuteJsonRequest<IEnumerable<Address>>(endpoint, Method.POST, newAddress);
        }

        public HcaResponse<ProductSearchResult> SearchProducts(ProductSearchOptionsRequest product,
            string endpoint = "search/products")
        {
            return ExecuteJsonRequest<ProductSearchResult>(endpoint, Method.POST, product);
        }

        public HcaResponse<CartResult> AddCartLines(CartLinesRequest cartLine, string endpoint = "carts/cartLines")
        {
            return ExecuteJsonRequest<CartResult>(endpoint, Method.POST, cartLine);
        }

        public HcaResponse<CartResult> UpdateCartLines(CartLinesRequest cartLine, string endpoint = "carts/cartLines")
        {
            return ExecuteJsonRequest<CartResult>(endpoint, Method.PUT, cartLine);
        }

        public HcaResponse<CartResult> GetCart(string endpoint = "carts/cart")
        {
            return ExecuteJsonRequest<CartResult>(endpoint, Method.GET);
        }

        public HcaResponse<CartResult> RemoveCartLine(string productId, string variantId, string endpoint = "carts/cartLines")
        {
            return ExecuteJsonRequest<CartResult>(endpoint + $"?productId={productId}&variantId={variantId}",
                Method.DELETE);
        }

        public HcaResponse<DeliveryInfoResult> GetDeliveryInfo(string endpoint = "checkout/deliveryInfo")
        {
            return ExecuteJsonRequest<DeliveryInfoResult>(endpoint, Method.GET);
        }

        public HcaResponse<ShippingInfoResult> GetShippingInfo(string endpoint = "checkout/shippingInfo")
        {
            return ExecuteJsonRequest<ShippingInfoResult>(endpoint, Method.GET);
        }

        public HcaResponse<BillingInfoResult> GetBillingInfo(string endpoint = "checkout/billingInfo")
        {
            return ExecuteJsonRequest<BillingInfoResult>(endpoint, Method.GET);
        }

        public HcaVoidResponse SetShippingOptions(SetShippingOptionsRequest shippingOptions,
            string endpoint = "checkout/shippingOptions")
        {
            return ExecuteJsonRequest(endpoint, Method.POST, shippingOptions);
        }

        public HcaVoidResponse SetPaymentInfo(SetPaymentInfoRequest paymentInfo,
            string endpoint = "checkout/paymentInfo")
        {
            return ExecuteJsonRequest(endpoint, Method.POST, paymentInfo);
        }

        public HcaResponse<OrderConfirmationResult> SubmitOrder(string endpoint = "checkout/orders")
        {
            return ExecuteJsonRequest<OrderConfirmationResult>(endpoint, Method.POST);
        }

        public HcaResponse<OrderResult> GetOrder(string confirmationId, string endpoint = "orders")
        {
            return ExecuteJsonRequest<OrderResult>($"{endpoint}/{confirmationId}", Method.GET);
        }

        public HcaResponse<CartResult> AddPromoCode(PromoCodeRequest promoCode, string endpoint = "carts/promoCodes")
        {
            return ExecuteJsonRequest<CartResult>(endpoint, Method.POST, promoCode);
        }

        public HcaResponse<CartResult> RemovePromoCode(PromoCodeRequest promoCode, string endpoint = "carts/promoCodes")
        {
            return ExecuteJsonRequest<CartResult>(endpoint, Method.DELETE, promoCode);
        }

        private Uri GetAbsoluteUri(string urlPostfix)
        {
            return _baseUri.AddPostfix(urlPostfix);
        }

        protected HcaResponse<TData> ExecuteJsonRequest<TData>(string postfix, Method method, object obj = null)
            where TData : class
        {
            return _httpClientService.ExecuteJsonRequest<HcaResponse<TData>, HcaOkResult<TData>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);
        }

        protected HcaVoidResponse ExecuteJsonRequest(string postfix, Method method, object obj = null)
        {
            return _httpClientService.ExecuteJsonRequest<HcaVoidResponse, HcaOkResult<object>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);
        }
    }
}