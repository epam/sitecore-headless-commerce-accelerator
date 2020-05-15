using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class Cartline
	{
		public string id { get; set; }
		public Product product { get; set; }
		public Variant variant { get; set; }
		public float quantity { get; set; }
		public Price price { get; set; }
	}
}
