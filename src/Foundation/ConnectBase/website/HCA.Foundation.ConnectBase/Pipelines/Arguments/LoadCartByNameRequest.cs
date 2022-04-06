using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using System;

namespace HCA.Foundation.ConnectBase.Pipelines.Arguments
{
    public class LoadCartByNameRequest : LoadCartRequest
	{
		public string CartName { get; set; }

		public LoadCartByNameRequest(string shopName, string cartName, string userId): base(shopName, Guid.Empty.ToString(), userId)
		{
			Assert.ArgumentNotNullOrEmpty(cartName, "cartName");
			Assert.ArgumentNotNullOrEmpty(userId, "userId");
			CartName = cartName;
		}

		public LoadCartByNameRequest(string shopName, string cartName, string userId, bool recalculate) : base(shopName, Guid.Empty.ToString(), userId, recalculate)
		{
			Assert.ArgumentNotNullOrEmpty(cartName, "cartName");
			Assert.ArgumentNotNullOrEmpty(userId, "userId");
			CartName = cartName;
		}
	}
}