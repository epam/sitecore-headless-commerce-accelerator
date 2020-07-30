using System.Security.Cryptography;
using System.Text;

namespace AutoTests.AutomationFramework.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string CreateMd5(this string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes) sb.Append(hashByte.ToString("X2"));

            return sb.ToString();
        }
    }
}