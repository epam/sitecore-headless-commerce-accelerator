namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class SearchProductResponseDTO
	{
		public Data data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }

		public class Data
		{
			public Product[] products { get; set; }
			public Facet[] facets { get; set; }
			public int totalItemCount { get; set; }
			public int totalPageCount { get; set; }
		}

		public class Facet
		{
			public string displayName { get; set; }
			public Foundvalue[] foundValues { get; set; }
			public object[] values { get; set; }
			public string name { get; set; }
		}
	}

	public class Foundvalue
	{
		public int aggregateCount { get; set; }
		public string name { get; set; }
	}
}
