using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class CheckoutBillingInfoResponseDTO
	{
		public Data2 data { get; set; }
		public string status { get; set; }
		public object tempData { get; set; }
	}

	public class Data2
	{
		public string paymentClientToken { get; set; }
		public object[] paymentMethods { get; set; }
		public Paymentoption[] paymentOptions { get; set; }
	}

	public class Paymentoption
	{
		public string description { get; set; }
		public object name { get; set; }
		public string paymentOptionTypeName { get; set; }
	}
}
