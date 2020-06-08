namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class Shipping
	{
		public object description { get; set; }
		public string externalId { get; set; }
		public string name { get; set; }
		public string[] lineIds { get; set; }
		public string partyId { get; set; }
		public string shippingPreferenceType { get; set; }
	}
}
