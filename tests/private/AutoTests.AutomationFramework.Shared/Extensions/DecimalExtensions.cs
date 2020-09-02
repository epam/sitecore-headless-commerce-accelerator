using System;

namespace AutoTests.AutomationFramework.Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal RoundUpMoney(this decimal number)
        {
            return decimal.Round(number, 2, MidpointRounding.ToNegativeInfinity);
        }
    }
}