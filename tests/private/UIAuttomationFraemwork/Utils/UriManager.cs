using System;
using System.Linq;

namespace UIAutomationFramework.Utils
{
    public static class UriManager
    {
        public static Uri AddPostfix(Uri uri, string urlPostfix)
        {
            const char slash = '/';
            var uriCheck = uri.AbsolutePath.EndsWith(slash);
            var urlPostfixCheck = urlPostfix.StartsWith(slash);

            if (uriCheck && urlPostfixCheck) return new Uri(uri.AbsoluteUri + urlPostfix.Skip(1));
            if (uriCheck || urlPostfixCheck) return new Uri(uri.AbsoluteUri + urlPostfix);

            return new Uri(uri.AbsoluteUri + slash + urlPostfix);
        }
    }
}