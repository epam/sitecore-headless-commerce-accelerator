using Sitecore.Commerce.Entities.Carts;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceCartProduct : CartProduct
	{
		public string ProductCatalog { get; set; }

		public string ProductCategory { get; set; }

		public string ProductVariantId { get; set; }

		public string DisplayName { get; set; }

		public string Description { get; set; }
	}
}