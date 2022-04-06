using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class ItemPricing
	{
		public decimal ListPrice { get; set; }

		public decimal SellPrice { get; set; }

		public string CurrencyCode { get; set; }

		public List<VariationPricing> VariationPricing { get; } = new List<VariationPricing>();

	}
}