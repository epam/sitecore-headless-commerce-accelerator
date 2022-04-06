using Newtonsoft.Json;
using Sitecore.Commerce.Entities.Orders;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceOrder : Order
	{
		public DateTime Created { get; set; }

		public bool IsDirty { get; set; }

		public bool IsEmpty { get; set; }

		public DateTime LastModified { get; set; }

		public int LineItemCount { get; set; }

		public string ModifiedBy { get; set; }

		[XmlIgnore]
		[JsonIgnore]
		public ReadOnlyCollection<CommerceOrderForm> OrderForms { get; set; }

		public string SoldToAddressId { get; set; }

		public string SoldToName { get; set; }

		public string StatusCode { get; set; }
	}
}