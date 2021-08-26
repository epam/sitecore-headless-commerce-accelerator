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

namespace HCA.Foundation.Commerce.Tests.Converters.Order
{
    using Base.Tests.Customization;

    using Commerce.Converters.Cart;
    using Commerce.Converters.Order;
    using Commerce.Mappers.Order;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Orders;

    using Xunit;

    public class OrderConverterTests
    {
        private readonly ICartConverter<Cart> cartConverter;

        private readonly IFixture fixture;

        private readonly OrderConverter orderConverter;

        private readonly IOrderMapper orderMapper;

        public OrderConverterTests()
        {
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.cartConverter = Substitute.For<ICartConverter<Cart>>();
            this.orderMapper = Substitute.For<IOrderMapper>();

            this.orderConverter = new OrderConverter(this.cartConverter, this.orderMapper);
        }

        [Fact]
        public void Convert_IfSourceCurtIsNull_ShouldReturnNull()
        {
            // act
            var result = this.orderConverter.Build(null);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Convert_IfSourceOrderIsValid_ShouldReturnBuild()
        {
            // arrange
            var source = this.fixture.Create<Order>();
            this.InitBuild(source);

            // act
            var result = this.orderConverter.Build(source);

            // assert
            this.cartConverter.Received(1).Convert(source);
            this.orderMapper.Received(1).Map(Arg.Any<Models.Entities.Cart.Cart>());
            this.orderMapper.Received(1).Map(Arg.Any<Order>(), Arg.Any<Models.Entities.Order.Order>());
        }

        private void InitBuild(Order source)
        {
            var cart = this.fixture.Create<Models.Entities.Cart.Cart>();

            this.cartConverter
                .Convert(source)
                .Returns(cart);

            this.orderMapper
                .Map<Models.Entities.Cart.Cart, Models.Entities.Order.Order>(cart)
                .Returns(this.fixture.Create<Models.Entities.Order.Order>());
        }
    }
}