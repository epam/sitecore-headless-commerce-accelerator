using HCA.Foundation.ConnectBase.Pipelines.Arguments;
using Sitecore.Commerce.Services;
using Sitecore.Commerce.Services.Payments;
using System;

namespace HCA.Foundation.ConnectBase.Providers
{
    public class PaymentServiceProviderBase: PaymentServiceProvider
    {
        public virtual PaymentClientTokenResult GetPaymentClientToken(ServiceProviderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}