using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Entities.Shipping;
using System;

namespace HCA.Foundation.ConnectBase.Entities
{
    [Serializable]
	public class CommerceShippingInfo : ShippingInfo
	{
		public ShippingOptionType ShippingOptionType
		{
			get
			{
				object propertyValue = GetPropertyValue(Constants.KnowPropertyNames.ShippingOptionType);
				if (propertyValue == null)
				{
					return ShippingOptionType.None;
				}
				return (ShippingOptionType)propertyValue;
			}
			set
			{
				SetPropertyValue(Constants.KnowPropertyNames.ShippingOptionType, value);
			}
		}

		public string ShippingMethodName
		{
			get
			{
				return GetPropertyValue(Constants.KnowPropertyNames.ShippingMethodName) as string;
			}
			set
			{
				SetPropertyValue(Constants.KnowPropertyNames.ShippingMethodName, value);
			}
		}

		public string ElectronicDeliveryEmail
		{
			get
			{
				return GetPropertyValue(Constants.KnowPropertyNames.ElectronicDeliveryEmail) as string;
			}
			set
			{
				SetPropertyValue(Constants.KnowPropertyNames.ElectronicDeliveryEmail, value);
			}
		}

		public string ElectronicDeliveryEmailContent
		{
			get
			{
				return GetPropertyValue(Constants.KnowPropertyNames.ElectronicDeliveryEmailContent) as string;
			}
			set
			{
				SetPropertyValue(Constants.KnowPropertyNames.ElectronicDeliveryEmailContent, value);
			}
		}

		public CommerceShippingInfo()
		{
			ShippingOptionType = ShippingOptionType.None;
			ShippingMethodName = string.Empty;
			ElectronicDeliveryEmail = string.Empty;
			ElectronicDeliveryEmailContent = string.Empty;
		}
	}
}