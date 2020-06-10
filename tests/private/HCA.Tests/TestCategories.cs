using System;
using System.Collections.Generic;
using System.Text;
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
}
