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

namespace HCA.Foundation.Connect.Tests.Managers.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;
    using Base.Tests.Customization;

    using Connect.Managers.Cart;
    using Connect.Mappers.Cart;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services.Carts;

    using Xunit;

    using AddShippingInfoRequest = Sitecore.Commerce.Engine.Connect.Services.Carts.AddShippingInfoRequest;

    public class CartManagerTests
    {
        private readonly CartManager cartManager;

        private readonly CommerceCartServiceProvider cartServiceProvider;

        private readonly IFixture fixture;

        public CartManagerTests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var logService = Substitute.For<ILogService<CommonLog>>();
            var cartMapper = Substitute.For<ICartMapper>();

            this.cartServiceProvider = Substitute.For<CommerceCartServiceProvider>();

            connectServiceProvider.GetCommerceCartServiceProvider().Returns(this.cartServiceProvider);
            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.cartManager = Substitute.For<CartManager>(logService, connectServiceProvider, cartMapper);
        }

        public static IEnumerable<object[]> CartLinesParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, Enumerable.Empty<CartLine>() },
                new object[] { new Cart(), null }
            };

        public static IEnumerable<object[]> MergeCartsParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, new Cart() },
                new object[] { new Cart(), null }
            };

        public static IEnumerable<object[]> PromoCodeParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, "1" },
                new object[] { new CommerceCart(), null }
            };

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void AddCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(
            Cart cart,
            IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.AddCartLines(cart, cartLines));
        }

        [Fact]
        public void AddCartLines_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.AddCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.cartManager.Received(1).Execute(Arg.Any<AddCartLinesRequest>(), this.cartServiceProvider.AddCartLines);
        }

        [Fact]
        public void AddPaymentInfo_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddPaymentInfo(
                    null,
                    this.fixture.Create<Party>(),
                    this.fixture.Create<FederatedPaymentInfo>()));
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddPaymentInfo(
                    this.fixture.Create<Cart>(),
                    null,
                    this.fixture.Create<FederatedPaymentInfo>()));
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddPaymentInfo(
                    this.fixture.Create<Cart>(),
                    this.fixture.Create<Party>(),
                    null));
        }

        [Fact]
        public void AddPaymentInfo_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.AddPaymentInfo(
                this.fixture.Create<Cart>(),
                this.fixture.Create<Party>(),
                this.fixture.Create<FederatedPaymentInfo>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<AddPaymentInfoRequest>(), this.cartServiceProvider.AddPaymentInfo);
        }

        [Theory]
        [MemberData(nameof(PromoCodeParameters))]
        public void AddPromoCode_IfParameterIsNull_ShouldThrowArgumentNullException(CommerceCart cart, string promoCode)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.AddPromoCode(cart, promoCode));
        }

        [Fact]
        public void AddPromoCode_IfPromoCodeIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.cartManager.AddPromoCode(this.fixture.Create<CommerceCart>(), string.Empty));
        }

        [Fact]
        public void AddPromoCode_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.AddPromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1).Execute(Arg.Any<AddPromoCodeRequest>(), this.cartServiceProvider.AddPromoCode);
        }

        [Fact]
        public void AddShippingInfo_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddShippingInfo(
                    this.fixture.Create<Cart>(),
                    this.fixture.Create<ShippingOptionType>(),
                    null));
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddShippingInfo(
                    this.fixture.Create<Cart>(),
                    null,
                    this.fixture.Create<List<ShippingInfo>>()));
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.AddShippingInfo(
                    null,
                    this.fixture.Create<ShippingOptionType>(),
                    this.fixture.Create<List<ShippingInfo>>()));
        }

        [Fact]
        public void AddShippingInfo_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.AddShippingInfo(
                this.fixture.Create<Cart>(),
                this.fixture.Create<ShippingOptionType>(),
                this.fixture.Create<List<ShippingInfo>>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<AddShippingInfoRequest>(), this.cartServiceProvider.AddShippingInfo);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "", "1")]
        [InlineData("", "1", "")]
        [InlineData("", "1", "1")]
        [InlineData("1", "", "")]
        [InlineData("1", "", "1")]
        [InlineData("1", "1", "")]
        public void CreateOrResumeCart_IfParameterIsEmpty_ShouldThrowArgumentException(
            string shopName,
            string userId,
            string customerId)
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.cartManager.CreateOrResumeCart(
                    shopName,
                    userId,
                    customerId));
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, null, "1")]
        [InlineData(null, "1", null)]
        [InlineData(null, "1", "1")]
        [InlineData("1", null, null)]
        [InlineData("1", null, "1")]
        [InlineData("1", "1", null)]
        public void CreateOrResumeCart_IfParameterIsNull_ShouldThrowArgumentNullException(
            string shopName,
            string userId,
            string customerId)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.CreateOrResumeCart(
                    shopName,
                    userId,
                    customerId));
        }

        [Fact]
        public void CreateOrResumeCart_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.CreateOrResumeCart(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<CreateOrResumeCartRequest>(), this.cartServiceProvider.CreateOrResumeCart);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "1")]
        [InlineData("1", "")]
        public void LoadCart_IfParameterIsEmpty_ShouldThrowArgumentException(string shopName, string customerId)
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.cartManager.LoadCart(
                    shopName,
                    customerId));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "1")]
        [InlineData("1", null)]
        public void LoadCart_IfParameterIsNull_ShouldThrowArgumentNullException(string shopName, string customerId)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.LoadCart(
                    shopName,
                    customerId));
        }

        [Fact]
        public void LoadCart_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.LoadCart(this.fixture.Create<string>(), this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1).Execute(Arg.Any<LoadCartByNameRequest>(), this.cartServiceProvider.LoadCart);
        }

        [Theory]
        [MemberData(nameof(MergeCartsParameters))]
        public void MergeCarts_IfParameterIsNull_ShouldThrowArgumentNullException(Cart fromCart, Cart toCart)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.MergeCarts(fromCart, toCart));
        }

        [Fact]
        public void MergeCarts_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.MergeCarts(this.fixture.Create<Cart>(), this.fixture.Create<Cart>());

            // assert
            this.cartManager.Received(1).Execute(Arg.Any<MergeCartRequest>(), this.cartServiceProvider.MergeCart);
        }

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void RemoveCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(
            Cart cart,
            IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.RemoveCartLines(cart, cartLines));
        }

        [Fact]
        public void RemoveCartLines_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.RemoveCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<RemoveCartLinesRequest>(), this.cartServiceProvider.RemoveCartLines);
        }

        [Fact]
        public void RemovePaymentInfo_IfCartIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.RemovePaymentInfo(null));
        }

        [Fact]
        public void RemovePaymentInfo_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.RemovePaymentInfo(this.fixture.Create<Cart>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<RemovePaymentInfoRequest>(), this.cartServiceProvider.RemovePaymentInfo);
        }

        [Theory]
        [MemberData(nameof(PromoCodeParameters))]
        public void RemovePromoCode_IfParameterIsNull_ShouldThrowArgumentNullException(
            CommerceCart cart,
            string promoCode)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.RemovePromoCode(cart, promoCode));
        }

        [Fact]
        public void RemovePromoCode_IfPromoCodeIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.cartManager.RemovePromoCode(this.fixture.Create<CommerceCart>(), string.Empty));
        }

        [Fact]
        public void RemovePromoCode_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.RemovePromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<RemovePromoCodeRequest>(), this.cartServiceProvider.RemovePromoCode);
        }

        [Fact]
        public void UpdateCart_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.cartManager.UpdateCart(null, this.fixture.Create<CartBase>()));
            Assert.Throws<ArgumentNullException>(() => this.cartManager.UpdateCart(this.fixture.Create<Cart>(), null));
        }

        [Fact]
        public void UpdateCart_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.UpdateCart(this.fixture.Create<Cart>(), this.fixture.Create<CartBase>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<UpdateCartRequest>(), this.cartServiceProvider.UpdateCart);
        }

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void UpdateCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(
            Cart cart,
            IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.UpdateCartLines(cart, cartLines));
        }

        [Fact]
        public void UpdateCartLines_ShouldCallExecuteMethod()
        {
            // act
            this.cartManager.UpdateCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.cartManager.Received(1)
                .Execute(Arg.Any<UpdateCartLinesRequest>(), this.cartServiceProvider.UpdateCartLines);
        }
    }
}