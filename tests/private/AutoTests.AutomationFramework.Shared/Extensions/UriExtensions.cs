using System;
using System.Linq;

namespace AutoTests.AutomationFramework.Shared.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddPostfix(this Uri uri, string postfix)
        {
            const string slash = "/";
            var uriCheck = uri.AbsolutePath.EndsWith(slash);
            var urlPostfixCheck = postfix.StartsWith(slash);

            if (uriCheck && urlPostfixCheck) return new Uri(uri.AbsoluteUri + postfix.Skip(1));
            if (uriCheck || urlPostfixCheck) return new Uri(uri.AbsoluteUri + postfix);

            return new Uri(uri.AbsoluteUri + slash + postfix);
        }
    }
}