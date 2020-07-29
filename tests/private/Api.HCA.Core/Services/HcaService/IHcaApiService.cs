using System.Collections.Generic;
using Api.HCA.Core.Models.Hca.Entities.Account;
using Api.HCA.Core.Models.Hca.Entities.Account.Authentication;
using Api.HCA.Core.Models.Hca.Entities.Addresses;
using Api.HCA.Core.Models.Hca.Entities.Billing;
using Api.HCA.Core.Models.Hca.Entities.Cart;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Delivery;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Payment;
using Api.HCA.Core.Models.Hca.Entities.Checkout.Shipping;
using Api.HCA.Core.Models.Hca.Entities.Order;
using Api.HCA.Core.Models.Hca.Entities.Search;
using Api.HCA.Core.Models.Hca.RequestResult;
using HCA.Api.Core.Models.Hca.RequestResult;

namespace Api.HCA.Core.Services.HcaService
{
    public interface IHcaApiService
    {
        HcaResponse<LoginResult> Login(LoginRequest loginData, string endpoint = "auth/login");

        HcaResponse<UserResult> CreateUserAccount(CreateAccountRequest newUser, string endpoint = "accounts/account");

        HcaResponse<IEnumerable<Address>> AddAddress(Address newAddress, string endpoint = "accounts/address");

        HcaResponse<ProductSearchResult> SearchProducts(ProductSearchOptionsRequest product,
            string endpoint = "search/products");

        HcaResponse<CartResult> AddCartLines(AddCartLinesRequest cartLine, string endpoint = "carts/cartLines");

        HcaResponse<CartResult> GetCart(string endpoint = "carts/cart");

        HcaResponse<CartResult> AddPromoCode(PromoCodeRequest promoCode, string endpoint = "carts/promoCodes");

        HcaResponse<DeliveryInfoResult> GetDeliveryInfo(string endpoint = "checkout/deliveryInfo");

        HcaResponse<ShippingInfoResult> GetShippingInfo(string endpoint = "checkout/shippingInfo");

        HcaResponse<BillingInfoResult> GetBillingInfo(string endpoint = "checkout/billingInfo");

        HcaVoidResponse SetShippingOptions(SetShippingOptionsRequest shippingOptions,
            string endpoint = "checkout/shippingOptions");

        HcaVoidResponse SetPaymentInfo(SetPaymentInfoRequest paymentInfo, string endpoint = "checkout/paymentInfo");

        HcaResponse<OrderConfirmationResult> SubmitOrder(string endpoint = "checkout/orders");

        HcaResponse<OrderResult> GetOrder(string trackingNumber, string endpoint = "orders");
    }
}