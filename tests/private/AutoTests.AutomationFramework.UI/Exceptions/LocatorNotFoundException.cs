using System;

namespace AutoTests.AutomationFramework.UI.Exceptions
{
    public class LocatorNotFoundException : Exception
    {
        public LocatorNotFoundException(string message) : base(message)
        {
        }
    }
}