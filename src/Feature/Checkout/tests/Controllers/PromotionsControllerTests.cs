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

namespace HCA.Feature.Checkout.Tests.Controllers
{
    using System;

    using Checkout.Controllers;

    using Foundation.Base.Models.Result;
    using Foundation.Commerce.Models.Entities.Promotion;
    using Foundation.Commerce.Services.Promotion;

    using NSubstitute;

    using Xunit;

    public class PromotionsControllerTests
    {
        private readonly PromotionsController controller;
        
        public PromotionsControllerTests()
        {
            var promotionService = Substitute.For<IPromotionService>();
            this.controller = Substitute.For<PromotionsController>(promotionService);
        }

        [Fact]
        public void AddPromoCode_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetFreeShippingSubtotal();

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<FreeShippingResult>>>());
        }
    }
}