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
    using AutoFixture;

    using Checkout.Controllers;
    using Checkout.Utils;

    using Glass.Mapper.Sc.Web.Mvc;

    using Models;

    using NSubstitute;



    using Sitecore.FakeDb.AutoFixture;

    using Utils;

    using Xunit;

    public class ExmOrdersRenderingsControllerTests
    {
        private readonly ExmOrdersRenderingsController controller;

        private readonly IMvcContext mvcContext;
        private readonly IOrderEmailUtils orderEmailUtils;

        private readonly IFixture fixture;

        public ExmOrdersRenderingsControllerTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.mvcContext = Substitute.For<IMvcContext>();
            this.orderEmailUtils = Substitute.For<IOrderEmailUtils>();

            this.controller = new ExmOrdersRenderingsController(this.mvcContext, this.orderEmailUtils);
        }

        [Fact]
        public void OrderConfirmation_IfDataSourceItemIsNull_ShouldNotThrowException()
        {
            // arrange
            this.mvcContext.GetDataSourceItem<OrderConfirmation>().Returns((OrderConfirmation)null);

            // act
            var exception = Record.Exception(() => this.controller.OrderConfirmation());

            // assert
            Assert.Null(exception);
        }

        [Fact]
        public void OrderConfirmation_IfDataSourceItemIsNotNull_ShouldCallResolveTokens()
        {
            // arrange
            var messageBody = this.fixture.Create<string>();
            var orderConfirmation = this.fixture.Build<OrderConfirmation>()
                .With(x => x.MessageBody, messageBody)
                .Create();
            this.mvcContext.GetDataSourceItem<OrderConfirmation>().Returns(orderConfirmation);

            // act
            this.controller.OrderConfirmation();

            // assert
            this.orderEmailUtils.Received(1).ResolveTokens(messageBody);
        }

        [Fact]
        public void OrderConfirmation_IfMessageBodyNotNull_ShouldNotCallResolveTokens()
        {
            // arrange
            var orderConfirmation = this.fixture.Build<OrderConfirmation>()
                .With(x => x.MessageBody, null)
                .Create();
            this.mvcContext.GetDataSourceItem<OrderConfirmation>().Returns(orderConfirmation);

            // act
            this.controller.OrderConfirmation();

            // assert
            this.orderEmailUtils.DidNotReceive().ResolveTokens(Arg.Any<string>());
        }
    }
}