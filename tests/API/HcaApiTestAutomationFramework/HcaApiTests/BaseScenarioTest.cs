using HcaApiTestAutomationFramework;
using HcaApiTestAutomationFramework.HcaDTO;
using NUnit.Framework;
using System.Linq;

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
			//Checking initial state of Cart
			var deliveryInfo = new HcaApiMethods<CheckoutDeliveryInfoResponseDTO>();
			var deliveryInfoReq = deliveryInfo.GetDeliveryInfo();
			Assert.True("ok".Equals(deliveryInfoReq.status.ToLower()), "The delivery info request is not passed");
			
			var shippingInfo = new HcaApiMethods<CheckoutShippingInfoResponseDTO>();
			var shippingInfoReq = shippingInfo.GetShippingInfo();
			Assert.True("ok".Equals(shippingInfoReq.status.ToLower()), "The Shipping info request is not passed");

			var billingInfo = new HcaApiMethods<CheckoutBillingInfoResponseDTO>();
			var billingInfoReq = billingInfo.GetShippingInfo();
			Assert.True("ok".Equals(billingInfoReq.status.ToLower()), "The Billing info request is not passed");

		}
	}
}
