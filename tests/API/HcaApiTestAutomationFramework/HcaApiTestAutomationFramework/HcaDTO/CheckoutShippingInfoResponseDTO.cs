namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutShippingInfoResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public Shippingmethod[] shippingMethods { get; set; }
		}

		public class Shippingmethod
		{
			public string description { get; set; }
			public string externalId { get; set; }
			public string name { get; set; }
			public object lineIds { get; set; }
			public object partyId { get; set; }
			public object shippingPreferenceType { get; set; }
		}
	}
}
