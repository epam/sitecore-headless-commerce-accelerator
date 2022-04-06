namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class GetSupportedCurrenciesRequest : Sitecore.Commerce.Services.Prices.GetSupportedCurrenciesRequest
	{
		public string UserId { get; set; }

		public GetSupportedCurrenciesRequest(string shopName)
			: base(shopName)
		{
		}
	}
}