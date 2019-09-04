//    Copyright 2019 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using Sitecore.Diagnostics;
using Wooli.Feature.Account.Controllers;
using Wooli.Foundation.Commerce.Utils;

namespace Wooli.Feature.Account.Infrastructure.Pipelines.Initialize
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            this.RegisterHttpRoutes(RouteTable.Routes);
        }

        private void RegisterHttpRoutes(RouteCollection routeCollection)
        {
            Assert.ArgumentNotNull(routeCollection, nameof(routeCollection));

            const string AccountControllerName = "Account";

            routeCollection.MapRoute(
                name: nameof(AccountController),
                url: Constants.CommerceRoutePrefix + $"/{AccountControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(AccountController).Namespace },
                defaults: new { controller = AccountControllerName });

            const string AuthenticationControllerName = "Authentication";
            const string AuthenticationPrefix = "auth";

            routeCollection.MapRoute(
                name: nameof(AuthenticationController),
                url: Constants.CommerceRoutePrefix + $"/{AuthenticationPrefix}" + "/{action}",
                namespaces: new[] { typeof(AuthenticationController).Namespace },
                defaults: new { controller = AuthenticationControllerName });
        }
    }
}
