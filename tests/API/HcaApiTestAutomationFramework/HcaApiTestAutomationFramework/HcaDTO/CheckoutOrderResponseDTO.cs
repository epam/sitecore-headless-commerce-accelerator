namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutOrderResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string confirmationId { get; set; }
		}
	}
}
