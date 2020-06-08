namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class SearchProductRequestDTO
	{
		public string categoryId { get; set; }
		public string searchKeyword { get; set; }
		public int pageNumber { get; set; }
		public int pageSize { get; set; }
		public int sortDirection { get; set; }
		public string sortField { get; set; }
		public Facet[] facets { get; set; }
	}

	public class Facet
	{
		public string name { get; set; }
		public string[] values { get; set; }
	}
}
