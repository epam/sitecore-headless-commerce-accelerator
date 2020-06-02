using System;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class OrdersOrderResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string orderID { get; set; }
			public DateTime orderDate { get; set; }
			public string status { get; set; }
			public string trackingNumber { get; set; }
			public bool isOfflineOrder { get; set; }
			public string id { get; set; }
			public string email { get; set; }
			public Price price { get; set; }
			public Cartline[] cartLines { get; set; }
			public Address[] addresses { get; set; }
			public object[] adjustments { get; set; }
			public Shipping[] shipping { get; set; }
			public Payment[] payment { get; set; }
		}

		public class Cartline
		{
			public string id { get; set; }
			public Product product { get; set; }
			public Variant1 variant { get; set; }
			public float quantity { get; set; }
			public Price1 price { get; set; }
		}
	}
	
	public class Variant1
	{
		public string variantId { get; set; }
		public Properties1 properties { get; set; }
		public string productId { get; set; }
		public string displayName { get; set; }
		public string description { get; set; }
		public string brand { get; set; }
		public string[] tags { get; set; }
		public string[] imageUrls { get; set; }
		public string currencySymbol { get; set; }
		public float listPrice { get; set; }
		public float adjustedPrice { get; set; }
		public string stockStatusName { get; set; }
		public object customerAverageRating { get; set; }
	}

	public class Properties1
	{
		public string color { get; set; }
		public string size { get; set; }
	}

	public class Price1
	{
		public string currencyCode { get; set; }
		public string currencySymbol { get; set; }
		public float total { get; set; }
		public float subtotal { get; set; }
		public float handlingTotal { get; set; }
		public float shippingTotal { get; set; }
		public float taxTotal { get; set; }
		public float totalSavings { get; set; }
	}
}
