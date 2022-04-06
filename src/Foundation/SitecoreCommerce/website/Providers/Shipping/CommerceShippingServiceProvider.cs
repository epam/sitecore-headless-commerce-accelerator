using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Services.Shipping;

namespace HCA.Foundation.SitecoreCommerce.Providers.Shipping
{
    public class CommerceShippingServiceProvider: ShippingServiceProvider
    {
        private readonly ProviderMapper mapper;

        public CommerceShippingServiceProvider()
        { 
            mapper = new ProviderMapper();
        }
        public override GetShippingMethodsResult GetShippingMethods(GetShippingMethodsRequest request)
        {
            var req = request is ConnectBase.Pipelines.Arguments.GetShippingMethodsRequest r ?
                new Sitecore.Commerce.Engine.Connect.Services.Shipping.GetShippingMethodsRequest(
                    r.ShippingOption,
                    null,
                    mapper.Map<CommerceCart, Sitecore.Commerce.Engine.Connect.Entities.CommerceCart>(r.Cart)
                    )
                : request;
            return base.GetShippingMethods(req);
        }
    }
}