using HcaApiTestAutomationFramework;
using HcaApiTestAutomationFramework.HcaDTO;
using NUnit.Framework;

namespace HcaApiTests
{
	[TestFixture]
	public class SimpleScenarioTests
	{
		[Test]
		public void VerifyGetCartTest()
		{
			var cart = new HcaApiMethods<CartResponseDTO>();
			var cartReq = cart.GetCart();

			Assert.True("USD".Equals(cartReq.data.price.currencyCode), "Currency Code does not match");
		}

		[Test]
		public void VerifyAddCartLineTest()
		{

			//add Test Data for Request
			var cartLine = new CartLineRequestDTO
			{
				productId = "6042238",
				quantity = 1,
				variantId = "56042238"
			};

			//request
			var cart = new HcaApiMethods<CartResponseDTO>();
			var cartReq = cart.AddCartLine(cartLine);
			Assert.True("6042238".Equals(cartReq.data.cartLines[0].product.productId.ToString()));
		}

		[Test]
		public void VerifyUpdateCartLineTest()
		{

			//add Test Data for Request
			var cartLine = new CartLineRequestDTO
			{
				productId = "6042238",
				quantity = 1,
				variantId = "56042238"
			};

			//request
			var cart = new HcaApiMethods<CartResponseDTO>();
			var cartReq = cart.UpdateCartLine(cartLine);
			Assert.True("6042238".Equals(cartReq.data.cartLines[0].product.productId.ToString()));
		}

	}
}
