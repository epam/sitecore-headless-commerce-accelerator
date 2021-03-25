﻿namespace HCA.Foundation.Base.Tests.Common.Utils
{
    using NSubstitute;
    using System.IO;
    using System.Security.Principal;
    using System.Web;

    /// <summary>
    /// Provides methods to fake HttpContext
    /// </summary>
    public class HttpContextFaker
    {
        /// <summary>
        /// Fills up current HttpContext with a fake context
        /// </summary>
        /// <returns></returns>
        public static void FakeGenericPrincipalContext()
        {
            var identity = Substitute.For<IIdentity>();
            HttpContext.Current = new HttpContext(new HttpRequest("", "http://fakeweb.org", ""), new HttpResponse(new StringWriter()))
            {
                User = new GenericPrincipal(identity, null)
            };
        }
    }
}