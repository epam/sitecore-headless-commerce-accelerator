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

namespace HCA.Foundation.Commerce.Tests.Services.Cart
{
    using System;
    using System.Collections.Generic;

    using Base.Tests.Customization;

    using Commerce.Builders.Cart;
    using Commerce.Services.Cart;

    using Connect.Context.Catalog;
    using Connect.Context.Storefront;
    using Connect.Managers.Cart;

    using Context;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Services.Carts;

    using Xunit;

    using Connect = Sitecore.Commerce.Entities.Carts;

    public class CartServiceTests
    {
        private readonly ICartManager cartManager;

        private readonly CartResult cartResult;

        private readonly ICartService cartService;

        private readonly CommerceCart commerceCart;

        private readonly ICartBuilder<Connect.Cart> cartBuilder;

        private readonly IFixture fixture;

        private readonly IVisitorContext visitorContext;

        public CartServiceTests()
        {
            var storefrontContext = Substitute.For<IStorefrontContext>();
            var catalogContext = Substitute.For<ICatalogContext>();

            this.cartManager = Substitute.For<ICartManager>();
            this.cartBuilder = Substitute.For<ICartBuilder<Connect.Cart>>();
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());
            this.visitorContext = Substitute.For<IVisitorContext>();
            this.cartService = new CartService(
                this.cartManager,
                storefrontContext,
                catalogContext,
                this.visitorContext,
                this.cartBuilder);

            this.cartResult = this.fixture.Create<CartResult>();
            this.commerceCart = this.fixture.Create<CommerceCart>();
            this.cartResult.Cart = this.commerceCart;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(this.cartResult);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void AddCartLine_IfParameterIsNull_ShouldThrowArgumentNullException(string productId, string variantId)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartService.AddCartLine(productId, variantId, this.fixture.Create<decimal>()));
        }

        [Fact]
        public void AddCartLine_IfParametersAreCorrect_ShouldCallMapWithCartResult()
        {
            // arrange
            var productId = this.fixture.Create<string>();
            var variantId = this.fixture.Create<string>();
            var quantity = this.fixture.Create<decimal>();
            var addCartLineResult = this.fixture.Create<CartResult>();

            this.cartManager.AddCartLines(
                    Arg.Any<Connect.Cart>(),
                    Arg.Any<IEnumerable<Connect.CartLine>>())
                .Returns(addCartLineResult);

            // act
            this.cartService.AddCartLine(productId, variantId, quantity);

            // assert
            this.cartBuilder.Received(1).Build(addCartLineResult.Cart);
        }

        [Fact]
        public void AddPromoCode_IfPromoCodeIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartService.AddPromoCode(null));
        }

        [Fact]
        public void AddPromoCode_IfValidPromoCodeWasPassed_ShouldCallAddPromoCodeMethod()
        {
            // act
            this.cartService.AddPromoCode(this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1).AddPromoCode(this.commerceCart, Arg.Any<string>());
        }

        [Fact]
        public void GetCart_ShouldCallMapWithCartManagerResult()
        {
            // act
            this.cartService.GetCart();

            // assert
            this.cartBuilder.Received(1).Build(this.cartResult.Cart);
        }

        [Fact]
        public void MergeCarts_IfAnonymousContactIdIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            string anonymousContactId = null;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartService.MergeCarts(anonymousContactId));
        }

        [Fact]
        public void MergeCarts_IfSrcCartResultIsSuccessfulAndDestCartResultIsSuccessful_ShouldCallMapWithMergedCart()
        {
            // arrange
            var arrange = this.ArrangeForMergeCarts(true, true);

            // act
            this.cartService.MergeCarts(this.fixture.Create<string>());

            // assert
            this.cartBuilder.Received(1).Build(arrange.MergedCartResult.Cart);
        }

        [Fact]
        public void MergeCarts_IfSrcCartResultIsSuccessfulAndDestCartResultIsUnsuccessful_ShouldCallMapWithDestCart()
        {
            // arrange
            var arrange = this.ArrangeForMergeCarts(true);

            // act
            this.cartService.MergeCarts(this.fixture.Create<string>());

            // assert
            this.cartBuilder.Received(1).Build(arrange.DestCartResult.Cart);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void MergeCarts_IfSrcCartResultIsUnsuccessful_ShouldCallMapWithSrcCart(bool destCartSuccess)
        {
            // arrange
            var arrange = this.ArrangeForMergeCarts(destCartSuccess: destCartSuccess);

            // act
            this.cartService.MergeCarts(this.fixture.Create<string>());

            // assert
            this.cartBuilder.Received(1).Build(arrange.SrcCartResult.Cart);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void RemoveCartLine_IfParameterIsNull_ShouldThrowArgumentNullException(
            string productId,
            string variantId)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartService.RemoveCartLine(productId, variantId));
        }

        [Fact]
        public void RemoveCartLine_IfValidProductIdAndVariantIdWerePassed_ShouldCallRemoveCartLineMethod()
        {
            // arrange
            this.cartResult.Cart.Lines = new List<Connect.CartLine>();

            // act
            this.cartService.RemoveCartLine(this.fixture.Create<string>(), this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1)
                .RemoveCartLines(this.cartResult.Cart, Arg.Any<IEnumerable<Connect.CartLine>>());
        }

        [Fact]
        public void RemovePromoCode_IfPromoCodeIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartService.RemovePromoCode(null));
        }

        [Fact]
        public void RemovePromoCode_IfValidPromoCodeWasPassed_ShouldCallRemovePromoCodeMethod()
        {
            // act
            this.cartService.RemovePromoCode(this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1).RemovePromoCode(this.commerceCart, Arg.Any<string>());
        }

        [Fact]
        public void UpdateCartLine_IfNonExistingProductIdAndVariantIdPassed_ShouldCallAddCartLinesMethod()
        {
            // arrange
            var arrange = this.ArrangeForUpdateCartLine();

            // act
            this.cartService.UpdateCartLine(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<decimal>());

            // assert
            this.cartManager.Received(1)
                .AddCartLines(arrange.LoadCartResult.Cart, Arg.Any<IEnumerable<Connect.CartLine>>());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void UpdateCartLine_IfParameterIsNull_ShouldThrowArgumentNullException(
            string productId,
            string variantId)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartService.UpdateCartLine(productId, variantId, this.fixture.Create<decimal>()));
        }

        [Fact]
        public void
            UpdateCartLine_IfQuantityIsNonZeroAndExistingProductIdAndVariantIdPassed_ShouldCallUpdateCartLinesMethod()
        {
            // arrange
            var arrange = this.ArrangeForUpdateCartLine();

            // act
            this.cartService.UpdateCartLine(arrange.ProductId, arrange.VariantId, 2);

            // assert
            this.cartManager.Received(1)
                .UpdateCartLines(arrange.LoadCartResult.Cart, Arg.Any<IEnumerable<Connect.CartLine>>());
        }

        [Fact]
        public void
            UpdateCartLine_IfQuantityIsZeroAndExistingProductIdAndVariantIdPassed_ShouldCallRemoveCartLinesMethod()
        {
            // arrange
            var arrange = this.ArrangeForUpdateCartLine();

            // act
            this.cartService.UpdateCartLine(arrange.ProductId, arrange.VariantId, 0);

            // assert
            this.cartManager.Received(1)
                .RemoveCartLines(arrange.LoadCartResult.Cart, Arg.Any<IEnumerable<Connect.CartLine>>());
        }

        private (CartResult SrcCartResult, CartResult DestCartResult, CartResult MergedCartResult) ArrangeForMergeCarts(
            bool srcCartSuccess = false,
            bool destCartSuccess = false)
        {
            var managerSrcCartResult = this.fixture.Create<CartResult>();
            var managerDestCartResult = this.fixture.Create<CartResult>();
            var mergedCartResult = this.fixture.Create<CartResult>();

            managerSrcCartResult.Success = srcCartSuccess;
            managerDestCartResult.Success = destCartSuccess;
            this.visitorContext.ContactId.Returns(this.fixture.Create<string>());
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(managerSrcCartResult);
            this.cartManager.LoadCart(Arg.Any<string>(), this.visitorContext.ContactId).Returns(managerDestCartResult);
            this.cartManager.MergeCarts(
                    Arg.Any<Connect.Cart>(),
                    Arg.Any<Connect.Cart>())
                .Returns(mergedCartResult);

            return (managerSrcCartResult, managerDestCartResult, mergedCartResult);
        }

        private (CartResult LoadCartResult, string ProductId, string VariantId) ArrangeForUpdateCartLine()
        {
            var loadCartResult = this.fixture.Create<CartResult>();
            var productId = this.fixture.Create<string>();
            var variantId = this.fixture.Create<string>();
            var cartLine = this.fixture.Create<CommerceCartLine>();
            var cartProduct = this.fixture.Build<CommerceCartProduct>()
                .With(e => e.ProductVariantId, variantId)
                .With(e => e.ProductId, productId)
                .Create();

            cartLine.Product = cartProduct;
            loadCartResult.Cart.Lines = new List<Connect.CartLine>
            {
                cartLine
            };
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(loadCartResult);

            return (loadCartResult, productId, variantId);
        }
    }
}