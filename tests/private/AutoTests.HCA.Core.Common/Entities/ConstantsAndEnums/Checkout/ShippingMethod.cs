using System.Runtime.Serialization;
using AutoTests.AutomationFramework.Shared.Extensions;

namespace AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout
{
    public enum ShippingMethod
    {
        [EnumMember(Value = "Select Option")] SelectOption,
        [EnumMember(Value = "Next Day Air")] NextDayAir,
        [EnumMember(Value = "Ground")] Ground,
        [EnumMember(Value = "Standard")] Standard,

        [EnumMember(Value = "Standard Overnight")]
        StandardOvernight
    }

    public static class ShippingMethodExtensions
    {
        public static string GetValue(this ShippingMethod shippingMethod)
        {
            return shippingMethod.GetAttribute<EnumMemberAttribute>().Value;
        }
    }
}