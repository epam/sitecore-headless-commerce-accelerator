using Sitecore.Data;
using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class FreeGiftItem
	{
		public ID ItemId { get; set; }

		public string Catalog { get; set; }

		public string ProductId { get; set; }

		public string VariantId { get; set; }

		public List<ItemAvailability> Availabilities { get; } = new List<ItemAvailability>();


		public List<ItemPricing> Pricing { get; } = new List<ItemPricing>();

	}
}