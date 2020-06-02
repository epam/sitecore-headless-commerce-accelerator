using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class Payment
	{
		public object cardToken { get; set; }
		public string partyId { get; set; }
		public string paymentMethodId { get; set; }
	}
}
