using HCA.Foundation.ConnectBase.Entities;
using Sitecore.Commerce.Entities.Payments;
using Sitecore.Diagnostics;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class GetPaymentMethodsRequest : Sitecore.Commerce.Services.Payments.GetPaymentMethodsRequest
	{
		public CommerceCart Cart { get; set; }

		public GetPaymentMethodsRequest(CommerceCart cart, PaymentOption paymentOption, string shopName = null)
			: base(paymentOption, shopName)
		{
			Assert.IsNotNull(cart, "cart");
			Cart = cart;
		}
	}
}