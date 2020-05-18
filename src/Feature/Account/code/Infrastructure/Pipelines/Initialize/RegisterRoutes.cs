//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Feature.Account.Infrastructure.Pipelines.Initialize
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Controllers;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    using Constants = Foundation.Commerce.Constants;

    [ExcludeFromCodeCoverage]
    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            this.RegisterHttpRoutes(RouteTable.Routes);
        }

        private void RegisterHttpRoutes(RouteCollection routeCollection)
        {
            Assert.ArgumentNotNull(routeCollection, nameof(routeCollection));
            
            const string AccountsControllerName = "Accounts";

            routeCollection.MapRoute(
                nameof(AccountsController),
                Foundation.Commerce.Constants.CommerceRoutePrefix + $"/{AccountsControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(AccountsController).Namespace },
                defaults: new
                {
                    controller = AccountsControllerName
                });

            const string AuthenticationControllerName = "Authentication";
            const string AuthenticationPrefix = "auth";

            routeCollection.MapRoute(
                nameof(AuthenticationController),
                Constants.CommerceRoutePrefix + $"/{AuthenticationPrefix}" + "/{action}",
                namespaces: new[] { typeof(AuthenticationController).Namespace },
                defaults: new
                {
                    controller = AuthenticationControllerName
                });
        }
    }
}