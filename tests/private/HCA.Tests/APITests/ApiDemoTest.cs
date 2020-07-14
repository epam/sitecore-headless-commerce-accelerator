using System;
using System.Collections.Generic;
using System.Linq;
using HCA.Api.Core.Models.Hca.Entities.Account.Authentication;
using HCA.Api.Core.Models.Hca.Entities.Cart;
using HCA.Api.Core.Models.Hca.Entities.Checkout.Payment;
using HCA.Api.Core.Models.Hca.Entities.Checkout.Shipping;
using HCA.Api.Core.Models.Hca.Entities.Search;
using HCA.Api.Core.Services.BraintreeServices;
using HCA.Api.Core.Services.HcaService;
using NUnit.Framework;
using Address = HCA.Api.Core.Models.Hca.Entities.Addresses.Address;
using Configuration = UIAutomationFramework.Configuration;
using CreditCardRequest = HCA.Api.Core.Models.Braitree.PaymentToken.Request.CreditCardRequest;

namespace HCA.Tests.APITests
{
    [TestFixture(UserState.Guest), TestFixture(UserState.Signed), Description("Base Demo scenario")]
    [ApiTest]
    public class ApiDemoTest : BaseApiTest
    {
        bool _stopTests;
        private readonly IHcaApiService _hcaService = new HcaApiService();
        private readonly IBraintreeApiService _braintreeApiService = new BraintreeApiService();

        private readonly bool _isNeedToSignIn;

        private const string _shippingMethodName = "Standard";
        private const string _searchKeyword = "phone";
        private static string _productId;
        private static string _guestNewPartyId;
        private static string _shippingMethodId;
        private static string _token;
        private static string _confirmationId;

        public ApiDemoTest(UserState state) => _isNeedToSignIn = Convert.ToBoolean(state);

        [SetUp]
        public void SetUp() =>
            Assume.That(_stopTests, Is.False);

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Passed)
                _stopTests = true;
        }

        [OneTimeSetUp]
        public void SignIn()
        {
            if (!_isNeedToSignIn) return;
            var user = Configuration.GetDefaultUserLogin();
            var authReq = _hcaService.Login(new LoginRequest(user.Email, user.Password));
            Assert.True(authReq.IsSuccessful, "The Login POST request is not passed");
        }

        [Test, Order(1), Description("Find any product.")]
        public void _01_SearchProducts()
        {
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageNumber = 0,
                PageSize = 12,
                SortDirection = 0,
                SearchKeyword = _searchKeyword
            };

            var products = _hcaService.SearchProducts(searchOptions);

            Assert.True(products.IsSuccessful, "The GetProducts POST request is not passed");
            _productId = products.OkResponseData.Data.Products.FirstOrDefault()?.ProductId;
        }

        [Test, Order(2), Description("Add Product to cart.")]
        public void _02_AddProductToCart()
        {
            var productForCard = new AddCartLinesRequest()
            {
                Quantity = 1,
                ProductId = _productId,
                VariantId = 5 + _productId
            };

            var cart = _hcaService.AddCartLines(productForCard);

            Assert.True(cart.IsSuccessful, "The Get Cart request is not passed");
            Assert.True(cart.OkResponseData.Data.CartLines.Any(x => x.Product.ProductId == _productId),
                "ProductId is not found");
        }

        [Test, Order(3), Description("Checkouts Product from cart. DeliveryInfo and ShippingInfo.")]
        public void _03_CheckDeliveryInfoAndShippingInfo()
        {
            var deliveryInfoReq = _hcaService.GetDeliveryInfo();
            var shippingInfoReq = _hcaService.GetShippingInfo();

            Assert.True(deliveryInfoReq.IsSuccessful, "The delivery info request is not passed");
            Assert.True(shippingInfoReq.IsSuccessful, "The Shipping info request is not passed");

            _guestNewPartyId = deliveryInfoReq.OkResponseData.Data.NewPartyId;
            _shippingMethodId = shippingInfoReq.OkResponseData.Data.ShippingMethods
                .First(x => x.Description == _shippingMethodName).ExternalId;
        }

        [Test, Order(4), Description("Populate Address data on Shipping tab.")]
        public void _04_AddShippingOptionsTest()
        {
            var shippingOptions = new SetShippingOptionsRequest()
            {
                OrderShippingPreferenceType = "1",
                ShippingAddresses = new List<Address>
                {
                    new Address()
                    {
                        Address1 = "testAddress",
                        Address2 = "",
                        City = "testCity",
                        Country = "Canada",
                        CountryCode = "CA",
                        Email = "test@email.com",
                        FirstName = "testFirstName",
                        IsPrimary = false,
                        LastName = "testLastName",
                        Name = "TEMP_NAME",
                        State = "AB",
                        ZipPostalCode = "123",
                        ExternalId = _guestNewPartyId,
                        PartyId = _guestNewPartyId
                    }
                },
                ShippingMethods = new List<ShippingMethod>
                {
                    new ShippingMethod
                    {
                        Description = _shippingMethodName,
                        LineIds = null,
                        Name = _shippingMethodName,
                        PartyId = _guestNewPartyId,
                        ShippingPreferenceType = "1",
                        ExternalId = _shippingMethodId,
                    }
                },
            };

            var shippingOptionsReq = _hcaService.SetShippingOptions(shippingOptions);

            Assert.True(shippingOptionsReq.IsSuccessful, "The Shipping Options request is not passed");
        }

        [Test, Order(5), Description("Add credit card data via graphQL.")]
        public void _05_PaymentTest()
        {
            var creditCard = new CreditCardRequest
            {
                Cvv = "123",
                ExpirationMonth = "1",
                ExpirationYear = "2021",
                Number = "4111111111111111",
            };

            var paymentTokenResult = _braintreeApiService.GetPaymentToken(creditCard);

            Assert.True(paymentTokenResult.IsSuccessful, "TokenizeCreditCard response is not passed");
            Assert.IsNotEmpty(_token, "The token is Empty");

            _token = paymentTokenResult.OkResponseData.Data.TokenizeCreditCard.Token;
        }

        [Test, Order(6), Description("Add Payment Info card data.")]
        public void _06_SetPaymentInfo()
        {
            var paymentInfo = new SetPaymentInfoRequest()
            {
                BillingAddress = new Address
                {
                    Name = "",
                    FirstName = "firstName",
                    LastName = "lastName",
                    Address1 = "testAddress",
                    Address2 = "",
                    Country = "Canada",
                    CountryCode = "CA",
                    City = "testCity",
                    State = "AB",
                    ZipPostalCode = "123",
                    ExternalId = "",
                    PartyId = "",
                    IsPrimary = false,
                    Email = "test@email.com"
                },
                FederatedPayment = new FederatedPaymentInfo
                {
                    PartyId = null,
                    PaymentMethodId = "",
                    CardToken = _token
                },
            };
            var paymentInfoReq = _hcaService.SetPaymentInfo(paymentInfo);
            Assert.True(paymentInfoReq.IsSuccessful, "The paymentInfo request is not passed");
        }

        [Test, Order(7), Description("Submit order.")]
        public void _07_SubmitOrderTests()
        {
            var submitOrder = _hcaService.SubmitOrder();
            Assert.True(submitOrder.IsSuccessful, "The order request is not passed");
            _confirmationId = submitOrder.OkResponseData.Data.ConfirmationId;
        }

        [Test, Order(8), Description("Verify Order By ConfirmationId.")]
        public void _08_OrderExistsTest()
        {
            var getOrderReq = _hcaService.GetOrder(_confirmationId);
            Assert.True(getOrderReq.IsSuccessful, "The order request is not passed");
            Assert.AreEqual(_confirmationId, getOrderReq.OkResponseData.Data.TrackingNumber);
        }
    }
}
