using System.Runtime.Serialization;
using AutoTests.AutomationFramework.Shared.Extensions;

namespace AutoTests.HCA.Core.Common.Entities.ConstantsAndEnums.Checkout
{
    public enum AddressOption
    {
        [EnumMember(Value = "A New Address")] NewAddress,

        [EnumMember(Value = "A Saved Address")]
        SavedAddress,

        [EnumMember(Value = "Also use for billing address")]
        AlsoUseForBillingAddress,

        [EnumMember(Value = "Same As Shipping Address")]
        SameAsShippingAddress,

        [EnumMember(Value = "Save this address to")]
        SaveAddressToAccount
    }

    public static class AddressOptionExtensions
    {
        public static string GetValue(this AddressOption addressOption)
        {
            return addressOption.GetAttribute<EnumMemberAttribute>().Value;
        }
    }
}