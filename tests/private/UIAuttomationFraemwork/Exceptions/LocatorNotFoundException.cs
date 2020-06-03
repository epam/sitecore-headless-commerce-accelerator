using System;

namespace UIAutomationFramework.Exceptions
{
    public class LocatorNotFoundException : Exception
    {
        public LocatorNotFoundException(string message) : base(message)
        {
        }
    }
}