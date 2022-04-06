using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Services.Orders;
using System.Linq;

namespace HCA.Foundation.SitecoreCommerce.Providers.Orders
{
    public class CommerceOrderServiceProvider: OrderServiceProvider
    {
        private readonly ProviderMapper mapper;

        public CommerceOrderServiceProvider()
        { 
            mapper = new ProviderMapper();
        }

        public override GetVisitorOrderResult GetVisitorOrder(GetVisitorOrderRequest request)
        {
            var res = base.GetVisitorOrder(request);
            res.Order = res.Order is Sitecore.Commerce.Engine.Connect.Entities.CommerceOrder order ? order.Convert() : res.Order;
            return res;
        }

        public override GetVisitorOrdersResult GetVisitorOrders(GetVisitorOrdersRequest request)
        {
            var res  = base.GetVisitorOrders(request);
            res.OrderHeaders = res.OrderHeaders.Select(h => h is Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderHeader header ?
                mapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderHeader, CommerceOrderHeader>(header) : h).ToArray();
            return res;
        }

        public override SubmitVisitorOrderResult SubmitVisitorOrder(SubmitVisitorOrderRequest request)
        {
            var res = base.SubmitVisitorOrder(request);
            res.Order = res.Order is Sitecore.Commerce.Engine.Connect.Entities.CommerceOrder order ? order.Convert() : res.Order;
            return res;
        }
    }
}