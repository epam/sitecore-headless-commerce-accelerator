using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class AccountChangePasswordRequestDTO
	{
		public string email { get; set; }
		public string newPassword { get; set; }
		public string oldPassword { get; set; }

	}
}
