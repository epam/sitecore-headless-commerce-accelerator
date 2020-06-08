namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CartResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public string id { get; set; }
			public string email { get; set; }
			public Price price { get; set; }
			public Cartline[] cartLines { get; set; }
			public object[] addresses { get; set; }
			public object[] adjustments { get; set; }
			public object[] shipping { get; set; }
			public object[] payment { get; set; }
		}
	}
}
