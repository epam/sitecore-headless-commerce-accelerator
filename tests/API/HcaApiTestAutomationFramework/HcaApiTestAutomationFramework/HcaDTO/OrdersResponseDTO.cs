using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	public class Address
	{
		public string name { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string address1 { get; set; }
		public string address2 { get; set; }
		public string country { get; set; }
		public string countryCode { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zipPostalCode { get; set; }
		public string externalId { get; set; }
		public string partyId { get; set; }
		public bool isPrimary { get; set; }
		public string email { get; set; }
	}

	public class Shipping
	{
		public object description { get; set; }
		public string externalId { get; set; }
		public string name { get; set; }
		public string[] lineIds { get; set; }
		public string partyId { get; set; }
		public string shippingPreferenceType { get; set; }
	}

	public class Payment
	{
		public object cardToken { get; set; }
		public string partyId { get; set; }
		public string paymentMethodId { get; set; }
	}
}
