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

namespace HCA.Foundation.Connect.Tests.Managers.Promotion
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Context.Storefront;
    using Connect.Managers.Promotion;

    using NSubstitute;

    using Xunit;

    public class PromotionManagerTests
    {
        private readonly PromotionManager manager;

        public PromotionManagerTests()
        {
            var storefrontContext = Substitute.For<IStorefrontContext>();
            var logService = Substitute.For<ILogService<CommonLog>>();

            this.manager = new PromotionManager(storefrontContext, logService);
        }

        [Fact]
        public void GetPromotion_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.manager.GetPromotion(string.Empty));
        }

        [Fact]
        public void GetPromotion_IfParameterIsNull_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.manager.GetPromotion(null));
        }
    }
}