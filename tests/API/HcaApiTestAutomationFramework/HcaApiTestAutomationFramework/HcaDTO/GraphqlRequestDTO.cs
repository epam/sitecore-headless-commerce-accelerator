using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class GraphqlRequestDTO
	{
		public Clientsdkmetadata clientSdkMetadata { get; set; }
		public string operationName { get; set; }
		public string query { get; set; }
		public Variables variables { get; set; }
	}

	public class Clientsdkmetadata
	{
		public string source { get; set; }
		public string integration { get; set; }
		public string sessionId { get; set; }
	}

	public class Variables
	{
		public Input input { get; set; }
	}

	public class Input
	{
		public Creditcard creditCard { get; set; }
		public Options options { get; set; }
	}

	public class Creditcard
	{
		public string number { get; set; }
		public string expirationMonth { get; set; }
		public string expirationYear { get; set; }
		public string cvv { get; set; }
	}

	public class Options
	{
		public string validate { get; set; }
	}

}
