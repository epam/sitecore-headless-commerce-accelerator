using HcaApiTestAutomationFramework;
using HcaApiTestAutomationFramework.HcaDTO;
using NUnit.Framework;
using HcaApiTestAutomationFramework.GraphQlDTO;

namespace HcaApiTests
{

	[TestFixture, Description("Base Demo scenario")]
	public class BaseScenarioTest
	{
		/* Base Demo scenario:
		 1. find any product (POST products)
		 2. add found product to cart (POST cartLines/)
		 3. check that product exists in the cart (GET cat)
		 4. check deleveryInfo, shippingInfo, billibgInfo (GET)
		 5. add shipping info (POST shippingInfo)
		 6. add graphql ClientConfiguration (POST query), add graphql TokenizeCreditCard (POST mutation)
		 7. add paymentInfo (POST)
		 8. order to pay (POST)
		 9. check order request by confirmationID (GET)
		 10. Check that cart is empty (GET cart)
		 */

		private string _categoryId = "8e456d84-4251-dba1-4b86-ce103dedcd02";
		private string _productId = "";
		private string _variantId = "";
		private string _guestUserID = "TEMP_NAME";
		private string _guestNewPartyId = "";
		private string _shippingMethodName = "Standard";
		private string _shippingMethodId = "";
		private string _token = "";
		private string _confirmationId = "";

		[Test, Order(1), Description("find any product")]
		public void T01FindProductTest()
		{
			var searchProduct = new SearchProductRequestDTO
			{
				categoryId = _categoryId,
				pageNumber = 0,
				pageSize = 12,
				searchKeyword = "",
				sortDirection = 0,
			};

			var products = new HcaApiMethods();
			var productsReq = products.SearchProducts(searchProduct);
			Assert.True("ok".Equals(productsReq.status.ToLower()), "The GetProducts POST request is not passed");
			_productId = productsReq.data.products[0].productId;
		}

		[Test, Order(2), Description("add Product to cart and check on existence")]
		public void T02VerifyProductAddingOToCartTest()
		{
			_variantId = "5" + _productId;
			//Test Data for Adding Product to the Cart Request
			var cartLine = new CartLineRequestDTO
			{
				productId = _productId,
				quantity = 1,
				variantId = _variantId,
			};

			//Adding Product to the Cart Request
			var cart = new HcaApiMethods();
			var cartReq = cart.AddCartLine(cartLine);
			Assert.True("ok".Equals(cartReq.status.ToLower()), "The Get Cart request is not passed");
			var test1 = "";
			foreach (var cartline in cartReq.data.cartLines)
			{
				if (_productId.Equals(cartline.product.productId))
				{
					test1 = cartline.product.productId;
					break;
				}
			}

			Assert.True(_productId.Equals(test1), "ProductId is not found");
		}

		[Test, Order(3), Description("check on product existence in the cart")]
		public void T03VerifyGetCartTest()
		{
			var cart = new HcaApiMethods();
			var cartReq = cart.GetCart();
			var test1 = "";
			foreach (var cartline in cartReq.data.cartLines)
			{
				if (_productId.Equals(cartline.product.productId))
				{
					test1 = cartline.product.productId;
					break;
				}
			}

			Assert.True(_productId.Equals(test1), "ProductId is not found");
			Assert.True("ok".Equals(cartReq.status.ToLower()), "The getCart GET request is not passed.");

		}

		[Test, Order(4), Description("Checkouts Product from cart")]
		public void T04VerifyCheckoutTest()
		{
			//Checking initial state of Cart Info
			var deliveryInfo = new HcaApiMethods();
			var deliveryInfoReq = deliveryInfo.GetDeliveryInfo();
			Assert.True("ok".Equals(deliveryInfoReq.status.ToLower()), "The delivery info request is not passed");
			_guestNewPartyId = deliveryInfoReq.data.newPartyId;

			var shippingInfo = new HcaApiMethods();
			var shippingInfoReq = shippingInfo.GetShippingInfo();
			Assert.True("ok".Equals(shippingInfoReq.status.ToLower()), "The Shipping info request is not passed");
			foreach (var shippingmethod in shippingInfoReq.data.shippingMethods)
			{
				if (shippingmethod.description.ToLower().Equals(_shippingMethodName.ToLower()))
				{
					_shippingMethodId = shippingmethod.externalId;
					break;
				}
			}

			var billingInfo = new HcaApiMethods();
			var billingInfoReq = billingInfo.GetBillingInfo();
			Assert.True("ok".Equals(billingInfoReq.status.ToLower()), "The Billing info request is not passed");
		}

		[Test, Order(5), Description("Populate Address data on Shipping tab")]
		public void T05VerifyAddingShippingOptionsTest()
		{

			//add Shipping Options
			var shippingData = new CheckoutSetShippingOptionRequestDTO
			{

				orderShippingPreferenceType = "1",
				shippingAddresses = new[]
				{
					new Shippingaddress
					{
						address1 = "testAddress",
						address2 = "",
						city = "testCity",
						country = "Canada",
						countryCode = "CA",
						email = "test@email.com",
						externalId = _guestNewPartyId,
						firstName = "testFirstName",
						isPrimary = false,
						lastName = "testLastName",
						name = _guestUserID,
						partyId = _guestNewPartyId,
						state = "AB",
						zipPostalCode = "123",
					}
				},
				shippingMethods = new[]
				{
					new Shippingmethod
					{
						description = _shippingMethodName,
						externalId = _shippingMethodId,
						lineIds = null,
						name = _shippingMethodName,
						partyId = _guestNewPartyId,
						shippingPreferenceType = "1",

					}
				},
			};
			var shippingOptions = new HcaApiMethods();
			var shippingOptionsReq = shippingOptions.SetShippingOptions(shippingData);
			Assert.True("ok".Equals(shippingOptionsReq.status.ToLower()), "The Shipping Options request is not passed");
		}

		[Test, Order(6), Description("Add credit card data via graphQL")]
		public void T06VerifyAddingPaymentCreditCardDataTest()
		{
			var graphqlCall = new GraphQLApiCalls();
			var responseClientConfiguration = graphqlCall.ClientConfiguration();
			Assert.IsNotEmpty(responseClientConfiguration, "ClientConfiguration response is not empty");

			var creditCard = new Input.Creditcard
			{
				cvv = "123",
				expirationMonth = "1",
				expirationYear = "2021",
				number = "4111111111111111",
			};
			var responseTokenizeCreditCard = graphqlCall.TokenizeCreditCard(creditCard);
			Assert.IsNotEmpty(responseTokenizeCreditCard, "TokenizeCreditCard response is not empty");
			_token = responseTokenizeCreditCard.data.tokenizeCreditCard.token;
			Assert.True((_token.Length>5), "The token was not received");
			
		}

		[Test, Order(7), Description("Add Payment Info card data")]
		public void T07VerifyAddingPaymentInfoTest()
		{
			var paymentData = new CheckoutSetPaymentInfoRequestDTO
			{

				billingAddress = new Billingaddress
				{
					name = "",
					firstName = "firstName",
					lastName = "lastName",
					address1 = "testAddress",
					address2 = "",
					country = "Canada",
					countryCode = "CA",
					city = "testCity",
					state = "AB",
					zipPostalCode = "123",
					externalId = "",
					partyId = "",
					isPrimary = false,
					email = "test@email.com"
				},
				federatedPayment = new Federatedpayment
				{
					cardToken = _token,
					partyId = null,
					paymentMethodId = ""
				},
			};

			var paymentInfo = new HcaApiMethods();
			var paymentInfoReq = paymentInfo.SetPaymentInfo(paymentData);
			Assert.True("ok".Equals(paymentInfoReq.status.ToLower()), "The paymentInfo request is not passed");
		}

		[Test, Order(8), Description("order to pay")]
		public void T08VerifyOrderToPayTest()
		{
			var order = new HcaApiMethods();
			var orderReq = order.SubmitOrder();
			_confirmationId = orderReq.data.confirmationId;
			Assert.True("ok".Equals(orderReq.status.ToLower()), "The order request is not passed");
		}

		[Test, Order(9), Description("Verify Order By ConfirmationId")]
		public void T09VerifyOrderByConfirmationIdTest()
		{
			var order = new HcaApiMethods();
			var orderReq = order.GetOrder(_confirmationId);
			Assert.True("ok".Equals(orderReq.status.ToLower()), "The order request is not passed");
		}


		[Test, Order(10), Description("Verify Cart Is Empty")]
		public void T10VerifyCartIsEmptyTest()
		{
			var cart = new HcaApiMethods();
			var cartReq = cart.GetCart();
			Assert.True("ok".Equals(cartReq.status.ToLower()), "The getCart GET request is not passed.");
			Assert.True(cartReq.data.cartLines.Length.ToString().Equals("0"), "Cart is not empty");
		}
	}
}