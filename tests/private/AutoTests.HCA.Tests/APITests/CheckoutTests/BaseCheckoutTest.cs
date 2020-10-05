using System;
using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Helpers;
using AutoTests.HCA.Core.API.BraintreeApi.Models.PaymentToken.Request;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Helpers;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Addresses;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Payment;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Checkout.Shipping;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings.Checkout;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Users;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.CheckoutTests
{
    [TestFixture(HcaUserRole.Guest)]
    [TestFixture(HcaUserRole.User)]
    [ApiTest]
    public class BaseCheckoutTest : BaseHcaApiTest
    {
        public BaseCheckoutTest(HcaUserRole userRole)
        {
            IsUser = userRole == HcaUserRole.User;
        }

        protected readonly bool IsUser;
        protected IHcaApiContext ApiContext;
        protected IHcaApiHelper HcaApiHelper;

        protected static readonly HcaCreditCardTDSettings DefaultCreditCartData = TestsData.GetDefaultCreditCard();
        protected static readonly HcaShippingMethodTDSettings DefaultShippingMethod = TestsData.GetDefaultShippingMethod();

        [SetUp]
        public virtual void SetUp()
        {

            ApiContext = TestsHelper.CreateHcaApiContext();
            if (IsUser)
            {
                HcaApiHelper = TestsHelper.CreateHcaUserApiHelper(TestsData.GetDefaultUser().Credentials, ApiContext);
            }
            else
            {
                HcaApiHelper = TestsHelper.CreateHcaGuestApiHelper(ApiContext);
            }
            HcaApiHelper.CleanCart();
        }

        protected Address GetRandomAddress()
        {
            var guid = Guid.NewGuid().ToString("N").ToLower();
            return new Address
            {
                FirstName = StringHelpers.RandomString(10),
                LastName = StringHelpers.RandomString(10),
                Address1 = StringHelpers.GetRandomAddressString(),
                Address2 = StringHelpers.GetRandomAddressString(),
                City = StringHelpers.RandomString(10),
                Country = "United States",
                Email = StringHelpers.GetRandomEmail(),
                CountryCode = "US",
                State = "AL",
                ZipPostalCode = "2335",
                IsPrimary = true,
                Name = StringHelpers.RandomString(10),
                ExternalId = guid,
                PartyId = guid
            };
        }

        protected void FillCart(IEnumerable<ProductTestsDataSettings> products = null)
        {
            HcaApiHelper.AddProductsToCart(products ?? TestsData.GetProducts(1));
        }

        protected void SetShippingOptions(Address address = null, ShippingMethod shippingMethod = null)
        {
            var model = new SetShippingOptionsRequest
            {
                ShippingAddresses = new List<Address> { address ?? GetRandomAddress() },
                OrderShippingPreferenceType = "1",
                ShippingMethods = new List<ShippingMethod> {shippingMethod ?? new ShippingMethod
                {
                    Description = DefaultShippingMethod.Description,
                    ExternalId = DefaultShippingMethod.ExternalId,
                    Name = DefaultShippingMethod.Name.ToString(),
                    PartyId = Guid.NewGuid().ToString("N").ToLower(),
                    LineIds = null,
                } }
            };
            ApiContext.Checkout.SetShippingOptions(model).CheckSuccessfulResponse();
        }

        protected string GetPaymentToken()
        {
            var cartData = new CreditCardRequest
            {
                Number = DefaultCreditCartData.Number,
                ExpirationMonth = DefaultCreditCartData.ExpirationMonth,
                ExpirationYear = DefaultCreditCartData.ExpirationYear,
                Cvv = DefaultCreditCartData.Cvv
            };
            var res = TestsHelper.CreateBraintreeClient().GetPaymentToken(cartData);
            return res.OkResponseData.Data.TokenizeCreditCard.Token;
        }

        protected void SetPaymentInfo(Address address)
        {
            var paymentInfo = new SetPaymentInfoRequest
            {
                BillingAddress = address,
                FederatedPayment = new FederatedPaymentInfo
                {
                    CardToken = GetPaymentToken(),
                }
            };
            ApiContext.Checkout.SetPaymentInfo(paymentInfo).CheckSuccessfulResponse();
        }

    }
}
