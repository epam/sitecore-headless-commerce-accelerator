using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ui.AutomationFramework.Utils
{
    public static class StringUtils
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

        public static string CreateMd5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes) sb.Append(hashByte.ToString("X2"));

                return sb.ToString();
            }
        }

        public static string ConvertNoNull(string s)
        {
            return s ?? string.Empty;
        }
    }
}