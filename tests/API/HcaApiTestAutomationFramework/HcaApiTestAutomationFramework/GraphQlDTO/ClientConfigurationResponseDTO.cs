namespace HcaApiTestAutomationFramework.GraphQlDTO
{
	public class ClientConfigurationResponseDTO
	{
		public Data data { get; set; }
		public Extensions extensions { get; set; }

		public class Data
		{
			public Clientconfiguration clientConfiguration { get; set; }
		}

	}

	public class Clientconfiguration
	{
		public string analyticsUrl { get; set; }
		public string environment { get; set; }
		public string merchantId { get; set; }
		public string assetsUrl { get; set; }
		public string clientApiUrl { get; set; }
		public Creditcard creditCard { get; set; }
		public object applePayWeb { get; set; }
		public object googlePay { get; set; }
		public object ideal { get; set; }
		public Kount kount { get; set; }
		public object masterpass { get; set; }
		public Paypal paypal { get; set; }
		public object unionPay { get; set; }
		public object usBankAccount { get; set; }
		public object venmo { get; set; }
		public object visaCheckout { get; set; }
		public object braintreeApi { get; set; }
		public string[] supportedFeatures { get; set; }

		public class Creditcard
		{
			public string[] supportedCardBrands { get; set; }
			public object[] challenges { get; set; }
			public bool threeDSecureEnabled { get; set; }
		}
	}

	public class Kount
	{
		public object merchantId { get; set; }
	}

	public class Paypal
	{
		public string displayName { get; set; }
		public object clientId { get; set; }
		public string privacyUrl { get; set; }
		public string userAgreementUrl { get; set; }
		public string assetsUrl { get; set; }
		public string environment { get; set; }
		public bool environmentNoNetwork { get; set; }
		public bool unvettedMerchant { get; set; }
		public string braintreeClientId { get; set; }
		public bool billingAgreementsEnabled { get; set; }
		public string merchantAccountId { get; set; }
		public string currencyCode { get; set; }
		public object payeeEmail { get; set; }
	}

}
