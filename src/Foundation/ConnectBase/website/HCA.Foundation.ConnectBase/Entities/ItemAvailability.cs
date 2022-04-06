namespace HCA.Foundation.ConnectBase.Entities
{
    public class ItemAvailability
	{
		public string Catalog { get; set; }

		public string ProductId { get; set; }

		public string VariantId { get; set; }

		public bool IsAvailable { get; set; }

		public bool IsAlwaysAvailable { get; set; }

		public bool CabBePreOrdered { get; set; }

		public bool CanBeBackOrdered { get; set; }
	}
}