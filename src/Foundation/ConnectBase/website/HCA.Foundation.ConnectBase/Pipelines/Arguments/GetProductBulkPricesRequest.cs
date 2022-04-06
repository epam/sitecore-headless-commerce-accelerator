using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class GetProductBulkPricesRequest : Sitecore.Commerce.Services.Prices.GetProductBulkPricesRequest
	{
		public string UserId { get; set; }

		public string ShopName { get; set; }

		public DateTime DateTime { get; set; }

		public string ProductCatalogName { get; set; }

		public IEnumerable<string> PriceTypeIds { get; protected set; }

		public GetProductBulkPricesRequest(string catalogName, IEnumerable<string> productIds, params string[] priceTypeIds)
			: base(productIds)
		{
			Assert.ArgumentNotNull(catalogName, nameof(catalogName));
			ProductCatalogName = catalogName;
			PriceTypeIds = priceTypeIds;
		}
	}
}