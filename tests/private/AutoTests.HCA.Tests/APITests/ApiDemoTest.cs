using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.Models.Braitree.PaymentToken.Request;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Addresses;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Cart;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Payment;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Checkout.Shipping;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using AutoTests.HCA.Core.API.Services.BraintreeServices;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutoTests.HCA.Tests.APITests
{
    [TestFixture(HcaUserRole.Guest)]
    [TestFixture(HcaUserRole.User)]
    [Description("Base Demo scenario")]
    [ApiTest]
    public class ApiDemoTest : BaseHcaApiTest
    {
        [SetUp]
        public void SetUp()
        {
            Assume.That(_stopTests, Is.False);
        }

        [TearDown]
        public new void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                _stopTests = true;
        }

        private bool _stopTests;
        private readonly IHcaApiService _hcaApiService = TestsHelper.CreateHcaApiClient();
        private readonly IBraintreeApiService _braintreeApiService = TestsHelper.CreateBraintreeClient();

        private readonly HcaUserRole _userRole;

        private const string _shippingMethodName = "Standard";
        private const string _searchKeyword = "phone";
        private static string _productId;
        private static string _guestNewPartyId;
        private static string _shippingMethodId;
        private static string _token;
        private static string _confirmationId;

        public ApiDemoTest(HcaUserRole role)
        {
            _userRole = role;
        }

        [OneTimeSetUp]
        public void SignIn()
        {
            if (_userRole != HcaUserRole.User) return;
            var user = TestsData.GetUser().Credentials;
            var authReq = _hcaApiService.Login(new LoginRequest(user.Email,
                user.Password));
            Assert.True(authReq.IsSuccessful, "The Login POST request is not passed");
        }

        [Test]
        [Order(1)]
        [Description("Find any product.")]
        public void _01_SearchProducts()
        {
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageNumber = 0,
                PageSize = 12,
                SortDirection = 0,
                SearchKeyword = _searchKeyword
            };

            var products = _hcaApiService.SearchProducts(searchOptions);

            Assert.True(products.IsSuccessful, "The GetProducts POST request is not passed");
            _productId = products.OkResponseData.Data.Products.FirstOrDefault()?.ProductId;
        }

        [Test]
        [Order(2)]
        [Description("Add Product to cart.")]
        public void _02_AddProductToCart()
        {
            var productForCard = new CartLinesRequest
            {
                Quantity = 1,
                ProductId = _productId,
                VariantId = 5 + _productId
            };

            var cart = _hcaApiService.AddCartLines(productForCard);

            Assert.True(cart.IsSuccessful, "The Get Cart request is not passed");
            Assert.True(cart.OkResponseData.Data.CartLines.Any(x => x.Product.ProductId == _productId),
                "ProductId is not found");
        }

        [Test]
        [Order(3)]
        [Description("Checkouts Product from cart. DeliveryInfo and ShippingInfo.")]
        public void _03_CheckDeliveryInfoAndShippingInfo()
        {
            var deliveryInfoReq = _hcaApiService.GetDeliveryInfo();
            var shippingInfoReq = _hcaApiService.GetShippingInfo();

            Assert.True(deliveryInfoReq.IsSuccessful, "The delivery info request is not passed");
            Assert.True(shippingInfoReq.IsSuccessful, "The Shipping info request is not passed");

            _guestNewPartyId = deliveryInfoReq.OkResponseData.Data.NewPartyId;
            _shippingMethodId = shippingInfoReq.OkResponseData.Data.ShippingMethods
                .First(x => x.Description == _shippingMethodName).ExternalId;
        }

        [Test]
        [Order(4)]
        [Description("Populate Address data on Shipping tab.")]
        public void _04_AddShippingOptionsTest()
        {
            var shippingOptions = new SetShippingOptionsRequest
            {
                OrderShippingPreferenceType = "1",
                ShippingAddresses = new List<Address>
                {
                    new Address
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
                        ExternalId = _shippingMethodId
                    }
                }
            };

            var shippingOptionsReq = _hcaApiService.SetShippingOptions(shippingOptions);

            Assert.True(shippingOptionsReq.IsSuccessful, "The Shipping Options request is not passed");
        }

        [Test]
        [Order(5)]
        [Description("Add credit card data via graphQL.")]
        public void _05_PaymentTest()
        {
            var creditCard = new CreditCardRequest
            {
                Cvv = "123",
                ExpirationMonth = "1",
                ExpirationYear = "2021",
                Number = "4111111111111111"
            };

            var paymentTokenResult = _braintreeApiService.GetPaymentToken(creditCard);

            Assert.True(paymentTokenResult.IsSuccessful, "TokenizeCreditCard response is not passed");
            Assert.IsNotEmpty(_token, "The token is Empty");

            _token = paymentTokenResult.OkResponseData.Data.TokenizeCreditCard.Token;
        }

        [Test]
        [Order(6)]
        [Description("Add Payment Info card data.")]
        public void _06_SetPaymentInfo()
        {
            var paymentInfo = new SetPaymentInfoRequest
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
                }
            };
            var paymentInfoReq = _hcaApiService.SetPaymentInfo(paymentInfo);
            Assert.True(paymentInfoReq.IsSuccessful, "The paymentInfo request is not passed");
        }

        [Test]
        [Order(7)]
        [Description("Submit order.")]
        public void _07_SubmitOrderTests()
        {
            var submitOrder = _hcaApiService.SubmitOrder();
            Assert.True(submitOrder.IsSuccessful, "The order request is not passed");
            _confirmationId = submitOrder.OkResponseData.Data.ConfirmationId;
        }

        [Test]
        [Order(8)]
        [Description("Verify Order By ConfirmationId.")]
        public void _08_OrderExistsTest()
        {
            var getOrderReq = _hcaApiService.GetOrder(_confirmationId);
            Assert.True(getOrderReq.IsSuccessful, "The order request is not passed");
            Assert.AreEqual(_confirmationId, getOrderReq.OkResponseData.Data.TrackingNumber);
        }
    }
}