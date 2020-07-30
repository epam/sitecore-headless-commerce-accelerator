using System;
using System.Linq;

namespace AutoTests.AutomationFramework.Shared.Helpers
{
    public static class StringHelpers
    {
        public static string GetRandomAddressString()
        {
            var rnd = new Random();
            return RandomString(rnd.Next(5, 12)) + " " + rnd.Next(1, 100);
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}