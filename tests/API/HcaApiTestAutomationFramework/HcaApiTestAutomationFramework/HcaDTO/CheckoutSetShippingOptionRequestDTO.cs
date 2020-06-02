namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutSetShippingOptionRequestDTO
	{
		public string orderShippingPreferenceType { get; set; }
		public Shippingaddress[] shippingAddresses { get; set; }
		public Shippingmethod[] shippingMethods { get; set; }
	}

	public class Shippingaddress
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

	public class Shippingmethod
	{
		public string description { get; set; }
		public string externalId { get; set; }
		public string name { get; set; }
		public object lineIds { get; set; }
		public string partyId { get; set; }
		public string shippingPreferenceType { get; set; }
	}

}
