using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class SearchProductRequestDTO
	{
		public string searchKeyword { get; set; }
		public int pageNumber { get; set; }
		public int pageSize { get; set; }
		public string sortDirection { get; set; }
		public string sortField { get; set; }
		public Facet[] facets { get; set; }
	}

	public class Facet
	{
		public string name { get; set; }
		public string[] values { get; set; }
	}
}
