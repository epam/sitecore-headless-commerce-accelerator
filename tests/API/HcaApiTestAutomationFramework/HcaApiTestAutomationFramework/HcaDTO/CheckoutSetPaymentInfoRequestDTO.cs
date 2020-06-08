namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutSetPaymentInfoRequestDTO
	{
		public Billingaddress billingAddress { get; set; }
		public Federatedpayment federatedPayment { get; set; }
	}

	public class Billingaddress
	{
		public string address1 { get; set; }
		public string address2 { get; set; }
		public string city { get; set; }
		public string country { get; set; }
		public string countryCode { get; set; }
		public string email { get; set; }
		public string externalId { get; set; }
		public string firstName { get; set; }
		public bool isPrimary { get; set; }
		public string lastName { get; set; }
		public string name { get; set; }
		public string partyId { get; set; }
		public string state { get; set; }
		public string zipPostalCode { get; set; }
	}

	public class Federatedpayment
	{
		public string cardToken { get; set; }
		public object partyId { get; set; }
		public string paymentMethodId { get; set; }
	}

}
