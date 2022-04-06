using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceOrderForm : Entity
	{
		private ReadOnlyCollection<ShippingInfo> _shippingList;

		private ReadOnlyCollection<PaymentInfo> _paymentList;

		public string BillingAddressId { get; set; }

		public DateTime Created { get; set; }

		public DateTime LastModified { get; set; }

		public ReadOnlyCollection<CommerceCartLine> CartLines { get; }

		public string ModifiedBy { get; set; }

		public string Name { get; set; }

		public Guid OrderFormId { get; set; }

		public string PromoUserIdentity { get; set; }

		public string Status { get; set; }

		public CommerceTotal Total { get; set; }

		public ReadOnlyCollection<ShippingInfo> Shipping
		{
			get
			{
				if (_shippingList == null)
				{
					_shippingList = new List<ShippingInfo>().AsReadOnly();
				}
				return _shippingList;
			}
		}

		public ReadOnlyCollection<PaymentInfo> Payment
		{
			get
			{
				if (_paymentList == null)
				{
					_paymentList = new List<PaymentInfo>().AsReadOnly();
				}
				return _paymentList;
			}
		}
	}
}