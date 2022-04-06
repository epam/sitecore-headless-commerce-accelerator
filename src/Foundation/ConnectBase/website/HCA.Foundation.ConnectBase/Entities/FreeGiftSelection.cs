using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class FreeGiftSelection
	{
		public string PromotionId { get; set; }

		public int NumberOfFreeGiftsToSelect { get; set; }

		public bool AddToCartAutomatically { get; set; }

		public List<FreeGiftItem> Items { get; set; } = new List<FreeGiftItem>();

	}
}