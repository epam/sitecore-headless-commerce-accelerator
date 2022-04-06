using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Entities.Shipping;
using Sitecore.Diagnostics;
using System.Collections.Generic;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class AddShippingInfoRequest : Sitecore.Commerce.Services.Carts.AddShippingInfoRequest
	{
		public ShippingOptionType OrderShippingOptionType { get; set; }

		public AddShippingInfoRequest(Cart cart, List<ShippingInfo> shippings, ShippingOptionType orderShippingOptionType)
			: base(cart, shippings)
		{
			Assert.IsNotNull(orderShippingOptionType, "orderShippingOptionType");
			OrderShippingOptionType = orderShippingOptionType;
		}
	}
}