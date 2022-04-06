using Sitecore.Commerce.Entities.Prices;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceTotal : Total
	{
		public decimal Subtotal { get; set; }

		public decimal HandlingTotal { get; set; }

		public decimal ShippingTotal { get; set; }

		public decimal LineItemDiscountAmount { get; set; }

		public decimal OrderLevelDiscountAmount { get; set; }
	}
}