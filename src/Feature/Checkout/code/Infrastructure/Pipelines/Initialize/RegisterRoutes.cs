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

namespace Wooli.Feature.Checkout.Infrastructure.Pipelines.Initialize
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Sitecore.Diagnostics;
    using Sitecore.Pipelines;

    using Wooli.Feature.Checkout.Controllers;
    using Wooli.Foundation.Commerce.Utils;

    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            this.RegisterHttpRoutes(RouteTable.Routes);
        }

        private void RegisterHttpRoutes(RouteCollection routeCollection)
        {
            Assert.ArgumentNotNull(routeCollection, nameof(routeCollection));

            const string CheckoutControllerName = "Checkout";

            routeCollection.MapRoute(
                name: nameof(CheckoutController),
                url: Constants.CommerceRoutePrefix + $"/{CheckoutControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(CheckoutController).Namespace },
                defaults: new { controller = CheckoutControllerName });

            const string CartControllerName = "Cart";

            routeCollection.MapRoute(
                name: nameof(CartController),
                url: Constants.CommerceRoutePrefix + $"/{CartControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(CheckoutController).Namespace },
                defaults: new { controller = CartControllerName });
        }
    }
}
