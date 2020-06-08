namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutDeliveryInfoResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string newPartyId { get; set; }
			public Shippingoption[] shippingOptions { get; set; }
			public Useraddress[] userAddresses { get; set; }
		}
	}

	public class Shippingoption
	{
		public string description { get; set; }
		public string name { get; set; }
		public int shippingOptionType { get; set; }
		public object shopName { get; set; }
	}

	public class Useraddress
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
		public object email { get; set; }
	}

}
