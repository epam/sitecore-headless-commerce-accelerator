using NUnit.Framework;

namespace AutoTests.HCA.Tests
{
    public class UiTestAttribute : CategoryAttribute
    {
        public UiTestAttribute() : base("UITest")
        {
        }
    }

    public class ApiTestAttribute : CategoryAttribute
    {
        public ApiTestAttribute() : base("APITest")
        {
        }
    }

    public class HeaderFooterTestAttribute : CategoryAttribute
    {
        public HeaderFooterTestAttribute() : base("HeaderFooterTest")
        {
        }
    }

    public class CheckoutTestAttribute : CategoryAttribute
    {
        public CheckoutTestAttribute() : base("CheckoutTest")
        {
        }
    }

    public class MyAccountTestAttribute : CategoryAttribute
    {
        public MyAccountTestAttribute() : base("MyAccountTest")
        {
        }
    }

    public class SignInSignUpTestAttribute : CategoryAttribute
    {
        public SignInSignUpTestAttribute() : base("SignInSignUpTest")
        {
        }
    }

    public class EndToEndTestAttribute : CategoryAttribute
    {
        public EndToEndTestAttribute() : base("EndToEndTest")
        {
        }
    }

    public class CartTestAttribute : CategoryAttribute
    {
        public CartTestAttribute() : base("CartTest")
        {
        }
    }

    public class ProductTestAttribute : CategoryAttribute
    {
        public ProductTestAttribute() : base("ProductTest")
        {
        }
    }
}