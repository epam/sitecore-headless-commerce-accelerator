﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class GraphlResponseDTO
	{

		public Data data { get; set; }
		public Extensions extensions { get; set; }

		public class Data
		{
			public Tokenizecreditcard tokenizeCreditCard { get; set; }
		}

		public class Tokenizecreditcard
		{
			public string token { get; set; }
			public Creditcard creditCard { get; set; }
		}

		public class Creditcard
		{
			public string bin { get; set; }
			public string brandCode { get; set; }
			public string last4 { get; set; }
			public Bindata binData { get; set; }
		}

		public class Bindata
		{
			public string prepaid { get; set; }
			public string healthcare { get; set; }
			public string debit { get; set; }
			public string durbinRegulated { get; set; }
			public string commercial { get; set; }
			public string payroll { get; set; }
			public object issuingBank { get; set; }
			public object countryOfIssuance { get; set; }
			public object productId { get; set; }
		}

		public class Extensions
		{
			public string requestId { get; set; }
		}
	}

}
