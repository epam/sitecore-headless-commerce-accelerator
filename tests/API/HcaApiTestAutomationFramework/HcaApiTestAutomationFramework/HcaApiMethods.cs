﻿using HcaApiTestAutomationFramework.HcaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

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

		public CheckoutDeliveryInfoResponseDTO GetDeliveryInfo(string endpoint = "checkout/deliveryInfo")
		{
			var cart = new ApiHelper<CheckoutDeliveryInfoResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreateGetRequest();
			var response = cart.GetResponse(url, request);
			CheckoutDeliveryInfoResponseDTO content = cart.GetContent<CheckoutDeliveryInfoResponseDTO>(response);
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



		public CheckoutShippingOptionsResponseDTO SetShippingOptions(dynamic requestBody, string endpoint = "checkout/shippingOptions")
		{
			var cart = new ApiHelper<CheckoutShippingOptionsResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			CheckoutShippingOptionsResponseDTO content = cart.GetContent<CheckoutShippingOptionsResponseDTO>(response);
			return content;
		}


		public CheckoutPaymentInfoResponseDTO SetPaymentInfo(dynamic requestBody, string endpoint = "checkout/paymentInfo")
		{
			var cart = new ApiHelper<CheckoutPaymentInfoResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			CheckoutPaymentInfoResponseDTO content = cart.GetContent<CheckoutPaymentInfoResponseDTO>(response);
			return content;
		}

		public CheckoutSubmitOrderResponseDTO SubmitOrder(string endpoint = "checkout/orders")
		{
			var cart = new ApiHelper<CheckoutSubmitOrderResponseDTO>();
			var url = cart.SetUrl(endpoint);
			var request = cart.CreatePostRequest();
			var response = cart.GetResponse(url, request);
			CheckoutSubmitOrderResponseDTO content = cart.GetContent<CheckoutSubmitOrderResponseDTO>(response);
			return content;
		}

		public GraphlResponseDTO GetToken(dynamic requestBody)
		{
			var cart = new ApiHelper<GraphlResponseDTO>();
			var url = new RestClient("https://payments.sandbox.braintree-api.com/graphql");
			var jsonRequest = cart.Serialize(requestBody);
			var request = cart.CreatePostRequest(jsonRequest);
			var response = cart.GetResponse(url, request);
			GraphlResponseDTO content = cart.GetContent<GraphlResponseDTO>(response);
			return content;

		}
	}
}
