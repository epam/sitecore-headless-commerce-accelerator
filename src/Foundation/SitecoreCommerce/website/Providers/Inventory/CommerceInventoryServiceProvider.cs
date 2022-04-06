using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Services.Inventory;
using System.Linq;

namespace HCA.Foundation.SitecoreCommerce.Providers.Inventory
{
    public class CommerceInventoryServiceProvider: InventoryServiceProvider
    {
        private readonly ProviderMapper mapper;

        public CommerceInventoryServiceProvider()
        { 
            mapper = new ProviderMapper();
        }

        public override GetStockInformationResult GetStockInformation(GetStockInformationRequest request)
        {
            var req = new GetStockInformationRequest(
                request.Shop?.Name,
                request.Products.Select(p => p is CommerceInventoryProduct product ? 
                    mapper.Map<CommerceInventoryProduct, Sitecore.Commerce.Engine.Connect.Entities.CommerceInventoryProduct>(product) : 
                    p)
            );
            var res = base.GetStockInformation(req);
            foreach (var si in res.StockInformation)
            {
                si.Product = si.Product is Sitecore.Commerce.Engine.Connect.Entities.CommerceInventoryProduct product ? 
                    mapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceInventoryProduct, CommerceInventoryProduct>(product) : 
                    si.Product;
            }
            return res;
        }
    }
}