using NUnit.Framework;

namespace HCA.Tests
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

    public class HeaderTestAttribute : CategoryAttribute
    {
        public HeaderTestAttribute() : base("HeaderTest")
        {
        }
    }
}