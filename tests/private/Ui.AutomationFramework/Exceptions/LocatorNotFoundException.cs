using System;

namespace Ui.AutomationFramework.Exceptions
{
    public class LocatorNotFoundException : Exception
    {
        public LocatorNotFoundException(string message) : base(message)
        {
        }
    }
}