using Sitecore.Commerce.Entities.Prices;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommercePrice : Price
	{
		public decimal ListPrice { get; set; }

		public decimal? LowestPricedVariant { get; set; }

		public decimal? LowestPricedVariantListPrice { get; set; }

		public decimal? HighestPricedVariant { get; set; }
	}
}