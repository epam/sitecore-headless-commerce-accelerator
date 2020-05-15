using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CartLineRequestDTO
	{
		public string productId { get; set; }
		public int quantity { get; set; }
		public string variantId { get; set; }

	}
}
