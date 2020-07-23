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

namespace HCA.Foundation.Commerce.Tests.Services.Promotion
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Base.Tests.Customization;

    using Commerce.Builders.Promotion;
    using Commerce.Services.Promotion;

    using Connect.Managers.Promotion;

    using Models.Entities.Promotion;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Services;

    using Xunit;

    public class PromotionServiceTests
    {
        private readonly IPromotionBuilder builder;

        private readonly IFixture fixture;

        private readonly IPromotionManager promotionManager;

        private readonly PromotionService service;

        public PromotionServiceTests()
        {
            this.builder = Substitute.For<IPromotionBuilder>();
            this.promotionManager = Substitute.For<IPromotionManager>();

            this.service = Substitute.For<PromotionService>(
                this.promotionManager,
                this.builder);

            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());
        }

        [Fact]
        public void GetFreeShippingSubtotal_IfGetPromotionResultNotSuccess_ShouldReturnNotSuccessResult()
        {
            // arrange
            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, false)
                .Create();
            getPromotionResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.promotionManager.GetPromotion(Constants.Promotion.FreeShippingPromotion).Returns(getPromotionResult);

            // act
            var result = this.service.GetFreeShippingSubtotal();

            // assert
            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetFreeShippingSubtotal_IfGetPromotionResultSuccess_ShouldReturnSuccessResult()
        {
            // arrange
            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, true)
                .Create();
            this.promotionManager.GetPromotion(Constants.Promotion.FreeShippingPromotion).Returns(getPromotionResult);

            var promotionResult = this.fixture
                .Build<Promotion>()
                .With(
                    p => p.Qualifications,
                    this.fixture
                        .Build<Qualification>()
                        .With(q => q.Subtotal, this.fixture.Create<int>().ToString())
                        .CreateMany(1))
                .Create();
            this.builder.BuildPromotion(getPromotionResult.EntityView).Returns(promotionResult);

            // act 
            var result = this.service.GetFreeShippingSubtotal();

            // assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
            Assert.Equal(int.Parse(promotionResult.Qualifications.First().Subtotal), result.Data.Subtotal);
        }

        [Fact]
        public void GetPromotion_IfArgumentEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.service.GetPromotion(string.Empty));
        }

        [Fact]
        public void GetPromotion_IfArgumentNull_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.service.GetPromotion(null));
        }

        [Fact]
        public void GetPromotion_IfManagerResultNotSuccess_ShouldReturnNotSuccessResult()
        {
            // arrange
            var name = this.fixture.Create<string>();
            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, false)
                .Create();
            getPromotionResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.promotionManager.GetPromotion(name).Returns(getPromotionResult);

            // act 
            var result = this.service.GetPromotion(name);

            // assert
            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
            this.builder.Received(0).BuildPromotion(getPromotionResult.EntityView);
        }

        [Fact]
        public void GetPromotion_IfManagerResultSuccess_ShouldReturnSuccessResult()
        {
            // arrange
            var name = this.fixture.Create<string>();
            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, true)
                .Create();
            this.promotionManager.GetPromotion(name).Returns(getPromotionResult);

            // act 
            var result = this.service.GetPromotion(name);

            // assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
            this.builder.Received(1).BuildPromotion(getPromotionResult.EntityView);
        }

        [Fact]
        public void GetPromotionByDisplayName_IfArgumentNull_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.service.GetPromotionByDisplayName(null));
        }

        [Fact]
        public void GetPromotionByDisplayName_IfGetPromotionResultNotSuccess_ShouldReturnNotSuccessResult()
        {
            // arrange
            var displayName = this.fixture.Create<string>();
            var name = this.fixture.Create<string>();

            var getPromotionsResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, true)
                .Create();
            getPromotionsResult.EntityView = this.CreateEntityView(displayName, name);
            this.promotionManager.GetPromotions().Returns(getPromotionsResult);

            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, false)
                .Create();
            getPromotionResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.promotionManager.GetPromotion(name).Returns(getPromotionResult);

            // act 
            var result = this.service.GetPromotionByDisplayName(displayName);

            // assert
            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
            this.builder.Received(0).BuildPromotion(getPromotionsResult.EntityView);
        }

        [Fact]
        public void GetPromotionByDisplayName_IfGetPromotionResultSuccess_ShouldReturnSuccessResult()
        {
            // arrange
            var displayName = this.fixture.Create<string>();
            var name = this.fixture.Create<string>();

            var getPromotionsResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, true)
                .Create();
            getPromotionsResult.EntityView = this.CreateEntityView(displayName, name);
            this.promotionManager.GetPromotions().Returns(getPromotionsResult);

            var getPromotionResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, true)
                .Create();
            this.promotionManager.GetPromotion(name).Returns(getPromotionResult);

            // act 
            var result = this.service.GetPromotionByDisplayName(displayName);

            // assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
            this.builder.Received(1).BuildPromotion(getPromotionResult.EntityView);
        }

        [Fact]
        public void GetPromotionByDisplayName_IfGetPromotionsResultNotSuccess_ShouldReturnNotSuccessResult()
        {
            // arrange
            var displayName = this.fixture.Create<string>();
            var getPromotionsResult = this.fixture
                .Build<GetEntityViewResult>()
                .With(ev => ev.Success, false)
                .Create();
            getPromotionsResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.promotionManager.GetPromotions().Returns(getPromotionsResult);

            // act 
            var result = this.service.GetPromotionByDisplayName(displayName);

            // assert
            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
        }

        private EntityView CreateEntityView(string displayName, string name)
        {
            return this.fixture
                .Build<EntityView>()
                .With(prop => prop.Name, "Master")
                .With(
                    s => s.ChildViews,
                    new ObservableCollection<Model>(
                        new List<EntityView>
                        {
                            this.fixture
                                .Build<EntityView>()
                                .With(prop => prop.Name, "Details")
                                .With(
                                    s => s.Properties,
                                    new ObservableCollection<ViewProperty>(
                                        new List<ViewProperty>
                                        {
                                            this.fixture
                                                .Build<ViewProperty>()
                                                .With(prop => prop.Name, "DisplayName")
                                                .With(prop => prop.Value, displayName)
                                                .Create(),
                                            this.fixture
                                                .Build<ViewProperty>()
                                                .With(prop => prop.Name, "Name")
                                                .With(prop => prop.Value, name)
                                                .Create()
                                        }))
                                .Create()
                        }))
                .Create();
        }
    }
}