namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class AccountCreateAccountResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string contactId { get; set; }
			public object customerId { get; set; }
			public string email { get; set; }
			public string firstName { get; set; }
			public string lastName { get; set; }
			public string userName { get; set; }
		}
	}

	

}
