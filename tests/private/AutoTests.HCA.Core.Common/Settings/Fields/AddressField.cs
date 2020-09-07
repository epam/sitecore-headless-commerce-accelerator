using System.Runtime.Serialization;
using AutoTests.AutomationFramework.Shared.Extensions;

namespace AutoTests.HCA.Core.Common.Settings.Fields
{
    public enum AddressField
    {
        [EnumMember(Value = "First Name")] FirstName,
        [EnumMember(Value = "Last Name")] LastName,
        [EnumMember(Value = "Address Line 1")] AddressLine1,
        [EnumMember(Value = "City")] City,
        [EnumMember(Value = "Country")] Country,
        [EnumMember(Value = "Province")] Province,
        [EnumMember(Value = "Postal Code")] PostalCode,
        [EnumMember(Value = "Email Address")] EmailAddress,
    }

    public static class FieldExtensions
    {
        public static string GetValue(this AddressField addressField)
        {
            return addressField.GetAttribute<EnumMemberAttribute>().Value;
        }
    }
}