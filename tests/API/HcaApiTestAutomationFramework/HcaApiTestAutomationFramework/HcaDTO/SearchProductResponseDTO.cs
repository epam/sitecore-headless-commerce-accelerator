using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
		public class SearchProductResponseDTO
		{
			public Data1 data { get; set; }
			public string status { get; set; }
			public object tempData { get; set; }
		}

		public class Data1
		{
			public Product[] products { get; set; }
			public Facet1[] facets { get; set; }
			public int totalItemCount { get; set; }
			public int totalPageCount { get; set; }
		}


		public class Facet1
		{
			public string displayName { get; set; }
			public Foundvalue[] foundValues { get; set; }
			public object[] values { get; set; }
			public string name { get; set; }
		}

		public class Foundvalue
		{
			public int aggregateCount { get; set; }
			public string name { get; set; }
		}
}
