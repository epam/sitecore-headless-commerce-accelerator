using System;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class OrdersResponseDTO
	{
		public Datum[] data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }
	}

	public class Datum
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
		public string[] adjustments { get; set; }
		public Shipping[] shipping { get; set; }
		public Payment[] payment { get; set; }
	}
}
