using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.GraphQlDTO
{
	public class ClientConfigurationRequestDTO
	{
		public Clientsdkmetadata clientSdkMetadata { get; set; }
		public string query { get; set; }
		public string operationName { get; set; }
	}

}
