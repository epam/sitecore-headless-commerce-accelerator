using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Configuration;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceCartLine : CartLine
	{
		private IEntityFactory _entityFactory = Factory.CreateObject("entityFactory", assert: true) as IEntityFactory;

		public bool AllowBackordersAndPreorders { get; set; }

		public decimal BackorderQuantity { get; set; }

		public DateTime Created { get; set; }

		public decimal InStockQuantity { get; set; }

		public string InventoryCondition { get; set; }

		public DateTime LastModified { get; set; }

		public string ModifiedBy { get; set; }

		public Guid OrderFormId { get; set; }

		public decimal PreorderQuantity { get; set; }

		public int Index { get; set; }

		public string ShippingAddressId { get; set; }

		public Guid ShippingMethodId { get; set; }

		public string ShippingMethodName { get; set; }

		public string Status { get; set; }

		public bool IsFreeGift { get; set; }

		public string PromotionId { get; set; }

		public decimal FreeGiftListPrice { get; set; }

		public decimal FreeGiftSellPrice { get; set; }

		public CommerceCartLine()
		{
		}

		public CommerceCartLine(string productCatalog, string productId, string variantId, decimal quantity)
		{			
			Quantity = quantity;
            Product = new CommerceCartProduct
			{
				ProductCatalog = productCatalog,
				ProductId = productId,
				ProductVariantId = variantId
			};
		}
	}
}