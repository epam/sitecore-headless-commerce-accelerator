using HcaApiTestAutomationFramework.HcaDTO;

namespace HcaApiTestAutomationFramework
{
	public class HcaApiMethods
	{

		public AccountCreateAccountResponseDTO CreateAccount(dynamic requestBody, string endpoint = "accounts/account")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<AccountCreateAccountResponseDTO>(response);
		}

		public CartResponseDTO GetCart(string endpoint = "carts/cart")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CartResponseDTO>(response);
		}

		public OrdersOrderResponseDTO GetOrder(string confirmationId)
		{
			var cart = new ApiHelper();
			var endpoint = $"orders/{confirmationId}";
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<OrdersOrderResponseDTO>(response);
		}

		public SearchProductResponseDTO SearchProducts(dynamic requestBody, string endpoint = "search/products")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<SearchProductResponseDTO>(response);
		}

		public CartResponseDTO AddCartLine(dynamic requestBody, string endpoint = "carts/cartLines")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CartResponseDTO>(response);
		}

		public CartResponseDTO UpdateCartLine(dynamic requestBody, string endpoint = "carts/cartLines")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePutRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CartResponseDTO>(response);
		}

		public CartResponseDTO AddPromoCode(dynamic requestBody, string endpoint = "carts/promoCodes")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CartResponseDTO>(response);
		}

		public CheckoutDeliveryInfoResponseDTO GetDeliveryInfo(string endpoint = "checkout/deliveryInfo")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutDeliveryInfoResponseDTO>(response);
		}

		public CheckoutShippingInfoResponseDTO GetShippingInfo(string endpoint = "checkout/shippingInfo")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutShippingInfoResponseDTO>(response);
		}

		public CheckoutBillingInfoResponseDTO GetBillingInfo(string endpoint = "checkout/billingInfo")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutBillingInfoResponseDTO>(response);
		}

		public CheckoutShippingOptionsResponseDTO SetShippingOptions(dynamic requestBody, string endpoint = "checkout/shippingOptions")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutShippingOptionsResponseDTO>(response);
		}

		public CheckoutPaymentInfoResponseDTO SetPaymentInfo(dynamic requestBody, string endpoint = "checkout/paymentInfo")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutPaymentInfoResponseDTO>(response);
		}

		public CheckoutSubmitOrderResponseDTO SubmitOrder(string endpoint = "checkout/orders")
		{
			var cart = new ApiHelper();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreatePostRequest();
			var response = cart.GetResponse(url, request);
			return cart.GetContent<CheckoutSubmitOrderResponseDTO>(response);
		}
	}
}
