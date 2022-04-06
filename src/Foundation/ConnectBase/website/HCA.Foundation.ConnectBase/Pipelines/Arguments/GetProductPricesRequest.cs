using Sitecore.Diagnostics;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class GetProductPricesRequest : Sitecore.Commerce.Services.Prices.GetProductPricesRequest
    {
		public string ProductCatalogName { get; set; }

		public bool IncludeVariantPrices { get; set; }

		public GetProductPricesRequest(string catalogName, string productId, params string[] priceTypeIds)
			: base(productId, priceTypeIds)
		{
			Assert.ArgumentNotNull(catalogName, nameof(catalogName));
			ProductCatalogName = catalogName;
			IncludeVariantPrices = false;
		}
	}
}