using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
		public class CheckoutShippingInfoResponseDTO
		{
			public Data4 data { get; set; }
			public string status { get; set; }
			public object tempData { get; set; }
		}

		public class Data4
		{
			public Shippingmethod1[] shippingMethods { get; set; }
		}

		public class Shippingmethod1
		{
			public string description { get; set; }
			public string externalId { get; set; }
			public string name { get; set; }
			public object lineIds { get; set; }
			public object partyId { get; set; }
			public object shippingPreferenceType { get; set; }
		}

}
