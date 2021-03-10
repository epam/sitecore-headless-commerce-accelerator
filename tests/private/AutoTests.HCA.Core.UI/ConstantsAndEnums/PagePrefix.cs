using System.Runtime.Serialization;
using AutoTests.AutomationFramework.Shared.Extensions;

namespace AutoTests.HCA.Core.UI.ConstantsAndEnums
{
    public enum PagePrefix
    {
        [EnumMember(Value = "")] Home,

        [EnumMember(Value = "Search")] Search,

        [EnumMember(Value = "Cart")] Cart,

        [EnumMember(Value = "login-register?form=login")] Login,

        [EnumMember(Value = "login-register?form=register")] Register,

        [EnumMember(Value = "Shop")] Shop,

        [EnumMember(Value = "Shop/Phone")] PhoneShop,

        [EnumMember(Value = "Product")] Product,

        [EnumMember(Value = "Checkout/Shipping")]
        CheckoutShipping,

        [EnumMember(Value = "Checkout/Billing")]
        CheckoutBilling,

        [EnumMember(Value = "Checkout/Payment")]
        CheckoutPayment,

        [EnumMember(Value = "Checkout/Confirmation")]
        CheckoutConfirmation,

        [EnumMember(Value = "Account")] Account,

        [EnumMember(Value = "Account/Order-history")]
        AccountOrderHistory,

        [EnumMember(Value = "storelocator")]
        StoreLocator
    }

    public static class PagePrefixExtensions
    {
        public static string GetPrefix(this PagePrefix prefix)
        {
            return prefix.GetAttribute<EnumMemberAttribute>().Value;
        }
    }
}