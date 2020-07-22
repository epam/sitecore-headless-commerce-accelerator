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

namespace HCA.Foundation.Commerce.Services.Promotion
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Builders.Promotion;

    using Connect.Managers.Promotion;

    using DependencyInjection;

    using Models.Entities.Promotion;

    using Sitecore.Diagnostics;

    [Service(typeof(IPromotionService), Lifetime = Lifetime.Singleton)]
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionBuilder builder;

        private readonly IPromotionManager promotionManager;

        public PromotionService(IPromotionManager promotionManager, IPromotionBuilder promotionBuilder)
        {
            Assert.ArgumentNotNull(promotionManager, nameof(promotionManager));
            Assert.ArgumentNotNull(promotionBuilder, nameof(promotionBuilder));

            this.promotionManager = promotionManager;
            this.builder = promotionBuilder;
        }

        public Result<Promotion> GetPromotion(string promotionName)
        {
            Assert.ArgumentNotNullOrEmpty(promotionName, nameof(promotionName));

            var result = new Result<Promotion>();

            var getEntityViewResult = this.promotionManager.GetPromotion(promotionName);

            if (getEntityViewResult.Success)
            {
                result.SetResult(this.builder.BuildPromotion(getEntityViewResult.EntityView));
            }
            else
            {
                result.SetErrors(getEntityViewResult.SystemMessages.Select(sm => sm.Message).ToList());
            }

            return result;
        }

        public Result<FreeShippingResult> GetFreeShippingSubtotal()
        {
            var result = new Result<FreeShippingResult>();

            var getPromotionsResult = this.GetPromotion(Constants.Promotion.FreeShippingPromotion);

            if (getPromotionsResult.Success)
            {
                var subtotal = getPromotionsResult.Data.Qualifications.First(q => q.Subtotal != string.Empty).Subtotal;

                result.SetResult(
                    new FreeShippingResult
                    {
                        Subtotal = int.Parse(subtotal)
                    });
            }
            else
            {
                result.SetErrors(getPromotionsResult.Errors);
            }

            return result;
        }
    }
}