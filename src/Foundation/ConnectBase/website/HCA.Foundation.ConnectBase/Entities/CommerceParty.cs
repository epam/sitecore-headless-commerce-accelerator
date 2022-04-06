using Sitecore.Commerce.Entities;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceParty : Party
	{
		public Guid UserProfileAddressId { get; set; }

		public string CountryCode { get; set; }

		public string EveningPhoneNumber { get; set; }

		public string FaxNumber { get; set; }

		public string Name { get; set; }

		public string RegionCode { get; set; }

		public string RegionName { get; set; }
	}
}