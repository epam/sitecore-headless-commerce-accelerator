using HCA.Foundation.ConnectBase.Entities;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Shipping;
using Sitecore.Diagnostics;
using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class GetShippingMethodsRequest : Sitecore.Commerce.Services.Shipping.GetShippingMethodsRequest
	{
		public CommerceCart Cart { get; set; }

		public List<CommerceCartLine> Lines { get; set; }

		public GetShippingMethodsRequest(ShippingOption shippingOption, Party party, CommerceCart cart)
			: base(shippingOption, party)
		{
			Assert.IsNotNull(cart, "cart");
			Cart = cart;
		}
	}
}