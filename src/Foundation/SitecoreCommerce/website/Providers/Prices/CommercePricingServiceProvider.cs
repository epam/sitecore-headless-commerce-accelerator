using HCA.Foundation.ConnectBase.Providers;
using Sitecore.Commerce.Services.Prices;
using System.Linq;

namespace HCA.Foundation.SitecoreCommerce.Providers.Prices
{
    public class CommercePricingServiceProvider: PricingServiceProviderBase
    {
        public override GetProductBulkPricesResult GetProductBulkPrices(GetProductBulkPricesRequest request)
        {
            var req = request is ConnectBase.Pipelines.Arguments.GetProductBulkPricesRequest r ? 
                new Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductBulkPricesRequest(r.ProductCatalogName, r.ProductIds, r.PriceTypeIds.ToArray()) 
                {
                    DateTime = r.DateTime,
                } : 
                request;
            return base.GetProductBulkPrices(req);
        }

        public override GetProductPricesResult GetProductPrices(GetProductPricesRequest request)
        {
            var req = request is ConnectBase.Pipelines.Arguments.GetProductPricesRequest r ? 
                new Sitecore.Commerce.Engine.Connect.Services.Prices.GetProductPricesRequest(r.ProductCatalogName, r.ProductId, r.PriceTypeIds.ToArray()) 
                {
                    IncludeVariantPrices = r.IncludeVariantPrices,
                    DateTime = r.DateTime
                } : 
                request;
            return base.GetProductPrices(req);
        }

        public override GetSupportedCurrenciesResult GetSupportedCurrencies(GetSupportedCurrenciesRequest request)
        {
            var req = request is ConnectBase.Pipelines.Arguments.GetSupportedCurrenciesRequest r ?
                new Sitecore.Commerce.Engine.Connect.Services.Prices.GetSupportedCurrenciesRequest(r.Shop?.Name) : request;
            return base.GetSupportedCurrencies(req);
        }
    }
}