using AutoTests.AutomationFramework.API.Services.RestService;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Billing;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Delivery;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Payment;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Order;
using AutoTests.HCA.Core.API.HcaApi.Models.RequestResult;
using RestSharp;

namespace AutoTests.HCA.Core.API.HcaApi.Services
{
    public class CheckoutService : BaseHcaService
    {
        public const string DELIVERY_INFO_ENDPOINT = "checkout/deliveryInfo";
        public const string BILLING_INFO_ENDPOINT = "checkout/billingInfo";
        public const string PAYMENT_INFO_ENDPOINT = "checkout/paymentInfo";
        public const string SHIPPING_INFO_ENDPOINT = "checkout/shippingInfo";
        public const string SHIPPING_OPTIONS_ENDPOINT = "checkout/shippingOptions";
        public const string ORDER_ENDPOINT = "checkout/orders";

        public CheckoutService(IHttpClientService httpClientService) : base(httpClientService)
        {
        }

        public HcaResponse<DeliveryInfoResult> GetDeliveryInfo()
        {
            return ExecuteJsonRequest<DeliveryInfoResult>(DELIVERY_INFO_ENDPOINT, Method.GET);
        }

        public HcaResponse<ShippingInfoResult> GetShippingInfo()
        {
            return ExecuteJsonRequest<ShippingInfoResult>(SHIPPING_INFO_ENDPOINT, Method.GET);
        }

        public HcaResponse<BillingInfoResult> GetBillingInfo()
        {
            return ExecuteJsonRequest<BillingInfoResult>(BILLING_INFO_ENDPOINT, Method.GET);
        }

        public HcaVoidResponse SetShippingOptions(SetShippingOptionsRequest shippingOptions)
        {
            return ExecuteJsonRequest(SHIPPING_OPTIONS_ENDPOINT, Method.POST, shippingOptions);
        }

        public HcaVoidResponse SetPaymentInfo(SetPaymentInfoRequest paymentInfo)
        {
            return ExecuteJsonRequest(PAYMENT_INFO_ENDPOINT, Method.POST, paymentInfo);
        }

        public HcaResponse<OrderConfirmationResult> SubmitOrder()
        {
            return ExecuteJsonRequest<OrderConfirmationResult>(ORDER_ENDPOINT, Method.POST);
        }
    }
}