namespace HcaApiTestAutomationFramework.HcaDTO
{
	public class Price
	{
		public string currencyCode { get; set; }
		public string currencySymbol { get; set; }
		public float total { get; set; }
		public float subtotal { get; set; }
		public float handlingTotal { get; set; }
		public float shippingTotal { get; set; }
		public float taxTotal { get; set; }
		public float totalSavings { get; set; }
	}
}
