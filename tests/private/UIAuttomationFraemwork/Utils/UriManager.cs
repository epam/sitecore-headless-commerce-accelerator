using System;

namespace UIAutomationFramework.Utils
{
    public static class UriManager
    {
        public static Uri AddPostfix(Uri uri, string urlPostfix)
        {
            var final = new UriBuilder(uri)
            {
                Path = new UriBuilder(uri).Path + urlPostfix
            };
            return final.Uri;
        }
    }
}