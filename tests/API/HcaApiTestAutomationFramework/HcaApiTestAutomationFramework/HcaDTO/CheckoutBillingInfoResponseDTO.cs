namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutBillingInfoResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string paymentClientToken { get; set; }
			public object[] paymentMethods { get; set; }
			public Paymentoption[] paymentOptions { get; set; }
		}
	}

	public class Paymentoption
	{
		public string description { get; set; }
		public object name { get; set; }
		public string paymentOptionTypeName { get; set; }
	}
}
