using HcaApiTestAutomationFramework.HcaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework
{
	public class HcaApiMethods<T>
	{
		public CartResponseDTO GetCart(string endpoint = "carts/cart")
		{
			var cart = new ApiHelper<CartResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			CartResponseDTO content = cart.GetContent<CartResponseDTO>(response);
			return content;
		}

		public CartResponseDTO AddCartLine(dynamic requestBody, string endpoint = "carts/cartLines")
		{
			var cart = new ApiHelper<CartResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			CartResponseDTO content = cart.GetContent<CartResponseDTO>(response);
			return content;
		}

		public CartResponseDTO UpdateCartLine(dynamic requestBody, string endpoint = "carts/cartLines")
		{
			var cart = new ApiHelper<CartResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePutRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			CartResponseDTO content = cart.GetContent<CartResponseDTO>(response);
			return content;
		}

		public CartResponseDTO AddPromoCode(dynamic requestBody, string endpoint = "carts/promoCodes")
		{
			var cart = new ApiHelper<CartResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			CartResponseDTO content = cart.GetContent<CartResponseDTO>(response);
			return content;
		}

		public SearchProductResponseDTO SearhProductsfromShop(dynamic requestBody, string referpoint, string endpoint = "search/products")
		{
			var cart = new ApiHelper<SearchProductResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest, referpoint);
			var response = cart.GetResponse(url, request);
			SearchProductResponseDTO content = cart.GetContent<SearchProductResponseDTO>(response);
			return content;
		}

		public CheckoutShippingInfoResponseDTO GetShippingInfo(string endpoint = "checkout/shippingInfo")
		{
			var cart = new ApiHelper<CheckoutShippingInfoResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			CheckoutShippingInfoResponseDTO content = cart.GetContent<CheckoutShippingInfoResponseDTO>(response);
			return content;
		}

		public CheckoutBillingInfoResponseDTO GetBillingInfo(string endpoint = "checkout/billingInfo")
		{
			var cart = new ApiHelper<CheckoutBillingInfoResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			CheckoutBillingInfoResponseDTO content = cart.GetContent<CheckoutBillingInfoResponseDTO>(response);
			return content;
		}

		public CheckoutDeliveryInfoResponseDTO GetDeliveryInfo(string endpoint = "checkout/deliveryInfo")
		{
			var cart = new ApiHelper<CheckoutDeliveryInfoResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			CheckoutDeliveryInfoResponseDTO content = cart.GetContent<CheckoutDeliveryInfoResponseDTO>(response);
			return content;
		}

	}
}
