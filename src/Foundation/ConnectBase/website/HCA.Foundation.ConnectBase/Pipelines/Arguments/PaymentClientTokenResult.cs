using Sitecore.Commerce.Services.Payments;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class PaymentClientTokenResult : PaymentResult
    {
        public string ClientToken { get; set; }
    }
}