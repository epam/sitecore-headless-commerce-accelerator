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

namespace HCA.Foundation.Commerce.Tests.Builders.Cart
{
    using Base.Models.Result;
    using Base.Tests.Customization;

    using Commerce.Builders.Cart;
    using Commerce.Mappers.Cart;
    using Commerce.Services.Catalog;

    using Models.Entities.Cart;
    using Models.Entities.Catalog;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    using Connect = Sitecore.Commerce.Entities.Carts;

    public class CartBuilderTests
    {
        private readonly CartBuilder cartBuilder;

        private readonly ICatalogService catalogService;
        private readonly ICartMapper cartMapper;

        private readonly IFixture fixture;

        public CartBuilderTests()
        {
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.catalogService = Substitute.For<ICatalogService>();
            this.cartMapper = Substitute.For<ICartMapper>();

            this.cartBuilder = new CartBuilder(this.catalogService, this.cartMapper);
        }

        [Fact]
        public void Build_IfSourceCurtIsNull_ShouldReturnNull()
        {
            // act
            var cart = this.cartBuilder.Build(null);

            // assert
            Assert.Null(cart);
        }

        [Fact]
        public void Build_IfSourceCartIsValid_ShouldReturnValidCart()
        {
            // arrange
            var source = this.fixture.Create<Connect.Cart>();
            var destination = this.fixture.Create<Cart>();
            this.cartMapper.Map<Connect.Cart, Cart>(source).Returns(destination);
            var cartLine = this.fixture.Create<CartLine>();
            this.cartMapper.Map<Connect.CartLine, CartLine>(Arg.Any<Connect.CartLine>()).Returns(cartLine);
            var successResult = new Result<Product>(this.fixture.Create<Product>());
            this.catalogService.GetProduct(Arg.Any<string>()).Returns(successResult);

            // act
            var result = this.cartBuilder.Build(source);

            // assert
            Assert.Equal(destination, result);
            this.cartMapper.Received(1).Map<Connect.Cart, Cart>(source);
            this.cartMapper.Received(source.Lines.Count).Map<Connect.CartLine, CartLine>(Arg.Any<Connect.CartLine>());
            this.catalogService.Received(source.Lines.Count).GetProduct(Arg.Any<string>());
        }
    }
}