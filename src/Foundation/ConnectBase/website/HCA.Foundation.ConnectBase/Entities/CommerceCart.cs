using Sitecore.Commerce.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceCart : Cart
	{
		public DateTime Created { get; set; }

		public bool IsDirty { get; set; }

		public bool IsEmpty { get; set; }

		public DateTime LastModified { get; set; }

		public int LineItemCount { get; set; }

		public string ModifiedBy { get; set; }

		public ReadOnlyCollection<CommerceOrderForm> OrderForms { get; set; }

		public string SoldToAddressId { get; set; }

		public string SoldToName { get; set; }

		public string StatusCode { get; set; }

		public string TrackingNumber { get; set; }

		public List<FreeGiftSelection> FreeGiftSelections { get; set; } = new List<FreeGiftSelection>();

	}
}