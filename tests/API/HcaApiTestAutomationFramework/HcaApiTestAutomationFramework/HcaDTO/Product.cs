namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class Product
	{
		public string sitecoreId { get; set; }
		public Variant[] variants { get; set; }
		public string productId { get; set; }
		public string displayName { get; set; }
		public string description { get; set; }
		public string brand { get; set; }
		public string[] tags { get; set; }
		public string[] imageUrls { get; set; }
		public string currencySymbol { get; set; }
		public float listPrice { get; set; }
		public float adjustedPrice { get; set; }
		public string stockStatusName { get; set; }
		public object customerAverageRating { get; set; }
	}
}
