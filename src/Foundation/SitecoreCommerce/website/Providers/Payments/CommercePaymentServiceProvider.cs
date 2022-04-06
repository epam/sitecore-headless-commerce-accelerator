using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.ConnectBase.Pipelines.Arguments;
using HCA.Foundation.ConnectBase.Providers;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Services;
using Sitecore.Commerce.Services.Payments;

namespace HCA.Foundation.SitecoreCommerce.Providers.Payments
{
    public class CommercePaymentServiceProvider: PaymentServiceProviderBase
    {
        private readonly ProviderMapper mapper;

        public CommercePaymentServiceProvider()
        {
            mapper = new ProviderMapper();
        }

        public override PaymentClientTokenResult GetPaymentClientToken(ServiceProviderRequest request)
        {
            var res = this.RunPipeline<ServiceProviderRequest, Sitecore.Commerce.Engine.Connect.Pipelines.Arguments.PaymentClientTokenResult>("commerce.payments.getClientToken", request);
            var result = new PaymentClientTokenResult 
            {
                ClientToken = res.ClientToken,
                Success = res.Success
            };
            foreach (var msg in res.SystemMessages) result.SystemMessages.Add(msg);
            return result;
        }

        public override GetPaymentMethodsResult GetPaymentMethods(Sitecore.Commerce.Services.Payments.GetPaymentMethodsRequest request)
        {
            var req = request is ConnectBase.Pipelines.Arguments.GetPaymentMethodsRequest r ? 
                new Sitecore.Commerce.Engine.Connect.Services.Payments.GetPaymentMethodsRequest(mapper.Map<CommerceCart, Sitecore.Commerce.Engine.Connect.Entities.CommerceCart>(r.Cart), request.PaymentOption, request.Shop?.Name) : 
                request;
            return base.GetPaymentMethods(req);
        }
    }
}