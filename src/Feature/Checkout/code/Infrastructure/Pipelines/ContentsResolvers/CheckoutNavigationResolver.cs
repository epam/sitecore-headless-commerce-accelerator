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

using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering.ContentsResolvers;
using Sitecore.Mvc.Presentation;
using Wooli.Feature.Checkout.Models;
using Wooli.Foundation.DependencyInjection;

namespace Wooli.Feature.Checkout.Infrastructure.Pipelines.ContentsResolvers
{
    [Service(Lifetime = Lifetime.Transient)]
    public class CheckoutNavigationResolver : RenderingContentsResolver
    {
        private readonly ISitecoreContext sitecoreContext;

        public CheckoutNavigationResolver(ISitecoreContext sitecoreContext)
        {
            this.sitecoreContext = sitecoreContext;
        }

        protected override JObject ProcessItem(Item item,
            Rendering rendering,
            IRenderingConfiguration renderingConfig)
        {
            JObject processedItem = base.ProcessItem(item, rendering, renderingConfig);
            if (!item.DescendsFrom(new ID(CheckoutNavigation.TemplateId))) return processedItem;

            var checkoutNavigation = sitecoreContext.Cast<ICheckoutNavigation>(item);
            IEnumerable<Guid> checkoutSteps = checkoutNavigation?.CheckoutSteps;
            if (checkoutSteps == null || !checkoutSteps.Any()) return processedItem;

            var firstStep = sitecoreContext.GetItem<ICheckoutStep>(checkoutSteps.ElementAt(0));
            processedItem.Add("url", firstStep.Url);

            return processedItem;
        }
    }
}