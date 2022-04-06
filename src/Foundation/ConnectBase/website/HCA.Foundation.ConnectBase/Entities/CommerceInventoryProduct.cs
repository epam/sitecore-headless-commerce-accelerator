using Sitecore.Commerce.Entities.Inventory;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class CommerceInventoryProduct : InventoryProduct
	{
		public string CatalogName { get; set; }

		public string VariantId { get; set; }
	}
}