namespace HcaApiTestAutomationFramework.GraphQlDTO
{
	public class TokenizeCreditCardRequestDTO
	{
		public Clientsdkmetadata clientSdkMetadata { get; set; }
		public string query { get; set; }
		public Variables variables { get; set; }
		public string operationName { get; set; }
	}

	public class Variables
	{
		public Input input { get; set; }
	}

	public class Input
	{
		public Creditcard creditCard { get; set; }
		public Options options { get; set; }

		public class Creditcard
		{
			public string number { get; set; }
			public string expirationMonth { get; set; }
			public string expirationYear { get; set; }
			public string cvv { get; set; }
		}
	}

	public class Options
	{
		public bool validate { get; set; }
	}

}
