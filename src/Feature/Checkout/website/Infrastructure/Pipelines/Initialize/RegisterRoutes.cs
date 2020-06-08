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

namespace HCA.Feature.Checkout.Infrastructure.Pipelines.Initialize
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Controllers;

    using Foundation.Commerce.Utils;

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

            const string CheckoutControllerName = "Checkout";

            routeCollection.MapRoute(
                nameof(CheckoutController),
                Foundation.Commerce.Constants.CommerceRoutePrefix + $"/{CheckoutControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(CheckoutController).Namespace },
                defaults: new
                {
                    controller = CheckoutControllerName
                });

            const string CartsControllerName = "Carts";

            routeCollection.MapRoute(
                nameof(CartsController),
                Foundation.Commerce.Constants.CommerceRoutePrefix + $"/{CartsControllerName.ToLowerInvariant()}" + "/{action}",
                namespaces: new[] { typeof(CartsController).Namespace },
                defaults: new
                {
                    controller = CartsControllerName
                });

            const string OrdersControllerName = "Orders";

            routeCollection.MapRoute(
                nameof(OrdersController),
                Foundation.Commerce.Constants.CommerceRoutePrefix + $"/{OrdersControllerName.ToLowerInvariant()}",
                namespaces: new[] { typeof(OrdersController).Namespace },
                defaults: new
                {
                    controller = OrdersControllerName,
                    action = "orders"
                });

            routeCollection.MapRoute(
                $"{nameof(OrdersController)}.order",
                Constants.CommerceRoutePrefix + $"/{OrdersControllerName.ToLowerInvariant()}" + "/{id}",
                namespaces: new[] { typeof(OrdersController).Namespace },
                defaults: new
                {
                    controller = OrdersControllerName,
                    action = "order"
                });
        }
    }
}