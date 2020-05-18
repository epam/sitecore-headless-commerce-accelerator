using HcaApiTestAutomationFramework;
using HcaApiTestAutomationFramework.HcaDTO;
using NUnit.Framework;
using System.Linq;
using System.Security.Principal;

namespace HcaApiTests
{
	
	[TestFixture, Description("Base Demo scenario")]
	public class BaseScenarioTest
	{
		[Test, Description("add Product to cart and check on existence")]
		public void VerifyProductAddingOToCartTest()
		{
			//Checking initial state of Cart
			var cart = new HcaApiMethods<CartResponseDTO>();
			var cartReq = cart.GetCart();
			Assert.True("ok".Equals(cartReq.status.ToLower()), "The Get Cart request is not passed");
			var initialQuantity = cartReq.data.cartLines.Aggregate(0, (current, prod) => current + (int) prod.quantity);

			//Test Data for Adding Product to the Cart Request
			var cartLine = new CartLineRequestDTO
			{
				productId = "6042238",
				quantity = 1,
				variantId = "56042238"
			};

			//Adding Product to the Cart Request
			cart = new HcaApiMethods<CartResponseDTO>();
			cartReq = cart.AddCartLine(cartLine);
			Assert.True("ok".Equals(cartReq.status.ToLower()), "The Get Cart request is not passed");
			var newQuantity = cartReq.data.cartLines.Aggregate(0, (current, prod) => current + (int)prod.quantity);
			Assert.True(initialQuantity<newQuantity, "The new product was not added, or other product was deleted during the adding one.");
			Assert.True("6042238".Equals(cartReq.data.cartLines[0].product.productId.ToString()));

		}

		[Test, Description("Checkouts Product from cart")]
		public void VerifyCheckoutTest()
		{
			//Checking initial state of Cart Info
			var deliveryInfo = new HcaApiMethods<CheckoutDeliveryInfoResponseDTO>();
			var deliveryInfoReq = deliveryInfo.GetDeliveryInfo();
			Assert.True("ok".Equals(deliveryInfoReq.status.ToLower()), "The delivery info request is not passed");
			
			var shippingInfo = new HcaApiMethods<CheckoutShippingInfoResponseDTO>();
			var shippingInfoReq = shippingInfo.GetShippingInfo();
			Assert.True("ok".Equals(shippingInfoReq.status.ToLower()), "The Shipping info request is not passed");

			var billingInfo = new HcaApiMethods<CheckoutBillingInfoResponseDTO>();
			var billingInfoReq = billingInfo.GetBillingInfo();
			Assert.True("ok".Equals(billingInfoReq.status.ToLower()), "The Billing info request is not passed");

			//add Shipping Options
			var shippingData = new CheckoutSetShippingOptionRequestDTO
			{
				
				orderShippingPreferenceType = "1",
				shippingAddresses = new[] {new Shippingaddress
				{
					name = "dd1c0c6dcb3844db9bccd1d862bb4cbb",
					firstName = "firstName",
					lastName = "lastName",
					address1 = "address 1",
					address2 = "",
					city = "TestC",
					country = "United States",
					state = "Al",
					zipPostalCode = "35004",
					externalId = "5eb7abba8825410d88e4fc781039c40f",
					partyId = "5eb7abba8825410d88e4fc781039c40f",
					isPrimary = false,
					email = "test@email.com"
				}},
				shippingMethods = new[] {new Shippingmethod
				{
					description = "Standard",
					externalId = "cf0af82a-e1b8-45c2-91db-7b9847af287c",
					name = "Standard",
					lineIds = null,
					partyId = "5eb7abba8825410d88e4fc781039c40f",
					shippingPreferenceType = "1",

				}},
			};
			var shippingOptions = new HcaApiMethods<CheckoutShippingOptionsResponseDTO>();
			var shippingOptionsReq = shippingOptions.SetShippingOptions(shippingData);
			Assert.True("ok".Equals(shippingOptionsReq.status.ToLower()), "The Shipping Options request is not passed");


			var graphqlData = new GraphqlRequestDTO()
			{
				clientSdkMetadata = new Clientsdkmetadata
				{
					source = "client",
					integration = "custom",
					sessionId = "02a61903-b8cf-4b4e-b913-bafba3bd9c1c"
				},
				operationName = "TokenizeCreditCard",
				query = "mutation TokenizeCreditCard($input: TokenizeCreditCardInput!) {   tokenizeCreditCard(input: $input) {     token     creditCard {       bin       brandCode       last4       binData {         prepaid         healthcare         debit         durbinRegulated         commercial         payroll         issuingBank         countryOfIssuance         productId       }     }   } }",
				variables = new Variables
				{
					input = new Input
					{
						creditCard = new Creditcard
						{
							number = "4111111111111111",
							expirationMonth = "1",
							expirationYear = "2021",
							cvv = "123"
						},
						options = new Options
						{
							validate = "true"
						}
					}
				}
			};

			var graphqlInfo = new HcaApiMethods<GraphlResponseDTO>();
			var graphqlInfoReq = graphqlInfo.GetToken(graphqlData);

			var token = graphqlInfoReq.data.tokenizeCreditCard.token;

			var paymentData = new CheckoutSetPaymentInfoRequestDTO
			{

				billingAddress = new[] {new Billingaddress
				{
					name = "dd1c0c6dcb3844db9bccd1d862bb4cbb",
					firstName = "firstName",
					lastName = "lastName",
					address1 = "address 1",
					address2 = "",
					country = "United States",
					countryCode = "US",
					city = "TestC",
					state = "AL",
					zipPostalCode = "35004",
					externalId = "5eb7abba8825410d88e4fc781039c40f",
					partyId = "5eb7abba8825410d88e4fc781039c40f",
					isPrimary = false,
					email = "test@email.com"
				}},
				federatedPayment = new[] {new Federatedpayment
				{
					cardToken = token,//"tokencc_bh_y9sbdx_8z3n8w_gqsx49_jtg2ct_w53",
					partyId = null,
					paymentMethodId = ""

				}},
			};

			var paymentInfo = new HcaApiMethods<CheckoutPaymentInfoResponseDTO>();
			var paymentInfoReq = paymentInfo.SetPaymentInfo(paymentData);
			Assert.True("ok".Equals(paymentInfoReq.status.ToLower()), "The paymentInfo request is not passed");

			var order = new HcaApiMethods<CheckoutSubmitOrderResponseDTO>();
			var orderReq = order.SubmitOrder();
			Assert.True("ok".Equals(orderReq.status.ToLower()), "The order request is not passed");
		}
	}
}
