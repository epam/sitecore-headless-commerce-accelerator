//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Connect.Managers.Promotion
{
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Context.Storefront;

    using DependencyInjection;

    using Sitecore.Commerce.Engine;
    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Pipelines;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Services;
    using Sitecore.Diagnostics;

    [Service(typeof(IPromotionManager), Lifetime = Lifetime.Singleton)]
    public class PromotionManager : IPromotionManager
    {
        protected readonly ILogService<CommonLog> LogService;

        protected readonly IStorefrontContext storefrontContext;

        private Container container;

        public PromotionManager(IStorefrontContext storefrontContext, ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.LogService = logService;
            this.storefrontContext = storefrontContext;
        }

        private Container Container
        {
            get
            {
                if (this.container == null)
                {
                    this.container = EngineConnectUtility.GetShopsContainer(shopName: this.storefrontContext.ShopName);
                }

                return this.container;
            }
        }

        public GetEntityViewResult GetPromotion(string promotionName)
        {
            Assert.ArgumentNotNullOrEmpty(promotionName, nameof(promotionName));

            return this.Execute<GetEntityViewRequest, GetEntityViewResult>(
                new GetEntityViewRequest
                {
                    Container = this.Container,
                    EntityId = $"Entity-Promotion-Habitat_PromotionBook-{promotionName}",
                    ItemId = string.Empty,
                    ViewName = "Master",
                    ForAction = string.Empty
                },
                EngineConnectConstants.KnownPipelineNames.GetEntityView);
        }

        public GetEntityViewResult GetPromotions()
        {
            var getEntityViewRequest = this.Execute<GetEntityViewRequest, GetEntityViewResult>(
                new GetEntityViewRequest
                {
                    Container = this.Container,
                    EntityId = "Entity-PromotionBook-Habitat_PromotionBook",
                    ItemId = string.Empty,
                    ViewName = "PromotionBookPromotions",
                    ForAction = string.Empty
                },
                EngineConnectConstants.KnownPipelineNames.GetEntityView);

            if (getEntityViewRequest.Success)
            {
                var count = getEntityViewRequest.EntityView.Properties.First(prop => prop.Name == "Count");
                var top = getEntityViewRequest.EntityView.Properties.First(prop => prop.Name == "Top");
                top.Value = count.Value;

                var doActionResult = this.Execute<DoActionRequest, DoActionResult>(
                    new DoActionRequest
                    {
                        Container = this.Container,
                        EntityView = getEntityViewRequest.EntityView
                    },
                    EngineConnectConstants.KnownPipelineNames.DoAction);

                if (doActionResult.Success)
                {
                    var entity =
                        (EntityView)doActionResult.CommerceCommand.Models.First(
                            model => model.Name == "PromotionBookPromotions");

                    getEntityViewRequest.EntityView.ChildViews = entity.ChildViews;
                }
                else
                {
                    getEntityViewRequest.Success = doActionResult.Success;
                    foreach (var sm in doActionResult.SystemMessages)
                    {
                        getEntityViewRequest.SystemMessages.Add(sm);
                    }
                }
            }

            return getEntityViewRequest;
        }

        public TResult Execute<TRequest, TResult>(TRequest request, string pipelineName)
            where TRequest : ServiceProviderRequest
            where TResult : ServiceProviderResult, new()
        {
            var getEntityViewRequest =
                PipelineUtility.RunConnectPipeline<TRequest, TResult>(pipelineName, request);

            if (!getEntityViewRequest.Success)
            {
                foreach (var systemMessage in getEntityViewRequest.SystemMessages)
                {
                    this.LogService.Error(systemMessage.Message);
                }
            }

            return getEntityViewRequest;
        }
    }
}