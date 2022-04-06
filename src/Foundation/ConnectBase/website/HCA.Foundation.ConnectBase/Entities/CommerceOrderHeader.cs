using Sitecore.Commerce.Entities.Orders;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceOrderHeader : OrderHeader
	{
		public DateTime Created { get; set; }

		public bool IsDirty { get; set; }

		public bool IsEmpty { get; set; }

		public DateTime LastModified { get; set; }

		public string ModifiedBy { get; set; }

		public string SoldToAddressId { get; set; }

		public string SoldToName { get; set; }

		public string StatusCode { get; set; }

		public string TrackingNumber { get; set; }
	}
}