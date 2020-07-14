using HCA.Api.Core.Models.Hca.Entities.Account;
using HCA.Api.Core.Models.Hca.Entities.Account.Authentication;
using HCA.Api.Core.Models.Hca.Entities.Addresses;
using HCA.Api.Core.Models.Hca.Entities.Billing;
using HCA.Api.Core.Models.Hca.Entities.Cart;
using HCA.Api.Core.Models.Hca.Entities.Checkout.Delivery;
using HCA.Api.Core.Models.Hca.Entities.Checkout.Payment;
using HCA.Api.Core.Models.Hca.Entities.Checkout.Shipping;
using HCA.Api.Core.Models.Hca.Entities.Order;
using HCA.Api.Core.Models.Hca.Entities.Search;
using HCA.Api.Core.Models.Hca.RequestResult;
using HCA.Api.Core.Models.Hca.RequestResult.Results.ErrorResult;
using HCA.Api.Core.Models.Hca.RequestResult.Results.OkResult;
using HCA.Api.Core.Services.RestService;
using RestSharp;
using System;
using System.Collections.Generic;
using UIAutomationFramework;
using UIAutomationFramework.Utils;

namespace HCA.Api.Core.Services.HcaService
{
    public class HcaApiService : IHcaApiService
    {
        protected readonly Uri BaseUri;
        private readonly IHttpClientService _httpClientService;

        public HcaApiService()
        {
            BaseUri = UriManager.AddPostfix(Configuration.GetEnvironmentUri("HCAEnvironment"), "apix/client/commerce");
            _httpClientService = new HttpClientService(BaseUri);
            //todo put lines in the configuration
            _httpClientService.AddClientCookie("SC_ANALYTICS_GLOBAL_COOKIE", "b916df89e9444a968dc1b74f53813e1e|True");
        }

        public HcaResponse<LoginResult> Login(LoginRequest loginData, string endpoint = "auth/login") =>
            ExecuteJsonRequest<LoginResult>(endpoint, Method.POST, loginData);

        public HcaResponse<UserResult> CreateUserAccount(CreateAccountRequest newUser, string endpoint = "accounts/account") =>
            ExecuteJsonRequest<UserResult>(endpoint, Method.POST, newUser);

        public HcaResponse<IEnumerable<Address>> AddAddress(Address newAddress, string endpoint = "accounts/address") =>
            ExecuteJsonRequest<IEnumerable<Address>>(endpoint, Method.POST, newAddress);

        public HcaResponse<ProductSearchResult> SearchProducts(ProductSearchOptionsRequest product, string endpoint = "search/products") =>
            ExecuteJsonRequest<ProductSearchResult>(endpoint, Method.POST, product);

        public HcaResponse<CartResult> AddCartLines(AddCartLinesRequest cartLine, string endpoint = "carts/cartLines") =>
            ExecuteJsonRequest<CartResult>(endpoint, Method.POST, cartLine);

        public HcaResponse<CartResult> GetCart(string endpoint = "carts/cart") =>
            ExecuteJsonRequest<CartResult>(endpoint, Method.GET);

        public HcaResponse<DeliveryInfoResult> GetDeliveryInfo(string endpoint = "checkout/deliveryInfo") =>
            ExecuteJsonRequest<DeliveryInfoResult>(endpoint, Method.GET);

        public HcaResponse<ShippingInfoResult> GetShippingInfo(string endpoint = "checkout/shippingInfo") =>
            ExecuteJsonRequest<ShippingInfoResult>(endpoint, Method.GET);

        public HcaResponse<BillingInfoResult> GetBillingInfo(string endpoint = "checkout/billingInfo") =>
            ExecuteJsonRequest<BillingInfoResult>(endpoint, Method.GET);

        public HcaVoidResponse SetShippingOptions(SetShippingOptionsRequest shippingOptions, string endpoint = "checkout/shippingOptions") =>
            ExecuteJsonRequest(endpoint, Method.POST, shippingOptions);

        public HcaVoidResponse SetPaymentInfo(SetPaymentInfoRequest paymentInfo, string endpoint = "checkout/paymentInfo") =>
            ExecuteJsonRequest(endpoint, Method.POST, paymentInfo);

        public HcaResponse<OrderConfirmationResult> SubmitOrder(string endpoint = "checkout/orders") =>
            ExecuteJsonRequest<OrderConfirmationResult>(endpoint, Method.POST);

        public HcaResponse<OrderResult> GetOrder(string confirmationId, string endpoint = "orders") =>
            ExecuteJsonRequest<OrderResult>($"{endpoint}/{confirmationId}", Method.GET);

        public HcaResponse<CartResult> UpdateCartLine(dynamic requestBody, string endpoint = "carts/cartLines") =>
            ExecuteJsonRequest<OrderResult>(endpoint, Method.PUT, requestBody);

        public HcaResponse<CartResult> AddPromoCode(PromoCodeRequest promoCode, string endpoint = "carts/promoCodes") =>
            ExecuteJsonRequest<CartResult>(endpoint, Method.POST, promoCode);

        private Uri GetAbsoluteUri(string urlPostfix) => UriManager.AddPostfix(BaseUri, urlPostfix);

        protected HcaResponse<TData> ExecuteJsonRequest<TData>(string postfix, Method method, object obj = null)
            where TData : class =>
            _httpClientService.ExecuteJsonRequest<HcaResponse<TData>, HcaOkResult<TData>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);

        protected HcaVoidResponse ExecuteJsonRequest(string postfix, Method method, object obj = null) =>
            _httpClientService.ExecuteJsonRequest<HcaVoidResponse, HcaOkResult<object>, HcaErrorsResult>(
                GetAbsoluteUri(postfix), method, obj);
    }
}
