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

namespace Wooli.Foundation.Connect.Tests.Managers.Cart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers.Contracts;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Engine.Connect.Pipelines.Arguments;
    using Sitecore.Commerce.Engine.Connect.Services.Carts;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;

    using Xunit;

    using AddShippingInfoRequest = Sitecore.Commerce.Engine.Connect.Services.Carts.AddShippingInfoRequest;

    public class CartManagerV2Tests
    {
        private readonly ICartManagerV2 cartManager;
        private readonly ILogService<CommonLog> logService;
        private readonly IFixture fixture;

        private readonly CartResult cartResult;
        private readonly AddShippingInfoResult addShippingInfoResult;
        private readonly AddPromoCodeResult addPromoCodeResult;
        private readonly RemovePromoCodeResult removePromoCodeResult;

        public static IEnumerable<object[]> CartLinesParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, Enumerable.Empty<CartLine>() },
                new object[] { new Cart(), null },
            };

        public static IEnumerable<object[]> MergeCartsParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, new Cart() },
                new object[] { new Cart(), null },
            };

        public static IEnumerable<object[]> PromoCodeParameters =>
            new List<object[]>
            {
                new object[] { null, null },
                new object[] { null, "1" },
                new object[] { new CommerceCart(), null },
            };

        public CartManagerV2Tests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var cartServiceProvider = Substitute.For<CommerceCartServiceProvider>();

            connectServiceProvider.GetCommerceCartServiceProvider().Returns(cartServiceProvider);
            this.fixture = this.CreateOmitOnRecursionFixture();
            this.cartResult = this.fixture.Build<CartResult>().With(res => res.Success, true).Create();
            this.addShippingInfoResult = this.fixture.Build<AddShippingInfoResult>()
                .With(res => res.Success, true)
                .With(res => res.ShippingInfo, null)
                .Create();
            this.addPromoCodeResult = this.fixture.Build<AddPromoCodeResult>().With(res => res.Success, true).Create();
            this.removePromoCodeResult = this.fixture.Build<RemovePromoCodeResult>().With(res => res.Success, true).Create();
            this.cartResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.addPromoCodeResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.addShippingInfoResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.removePromoCodeResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.cartManager = new CartManagerV2(this.logService, connectServiceProvider);

            cartServiceProvider.LoadCart(Arg.Any<LoadCartRequest>()).Returns(this.cartResult);
            cartServiceProvider.CreateOrResumeCart(Arg.Any<CreateOrResumeCartRequest>()).Returns(this.cartResult);
            cartServiceProvider.AddCartLines(Arg.Any<AddCartLinesRequest>()).Returns(this.cartResult);
            cartServiceProvider.AddShippingInfo(Arg.Any<AddShippingInfoRequest>()).Returns(this.addShippingInfoResult);
            cartServiceProvider.UpdateCartLines(Arg.Any<UpdateCartLinesRequest>()).Returns(this.cartResult);
            cartServiceProvider.RemoveCartLines(Arg.Any<RemoveCartLinesRequest>()).Returns(this.cartResult);
            cartServiceProvider.MergeCart(Arg.Any<MergeCartRequest>()).Returns(this.cartResult);
            cartServiceProvider.AddPromoCode(Arg.Any<AddPromoCodeRequest>()).Returns(this.addPromoCodeResult);
            cartServiceProvider.RemovePromoCode(Arg.Any<RemovePromoCodeRequest>()).Returns(this.removePromoCodeResult);
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

        [Fact]
        public void LoadCart_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.LoadCart(this.fixture.Create<string>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void LoadCart_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.LoadCart(this.fixture.Create<string>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
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

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "", "1")]
        [InlineData("", "1", "")]
        [InlineData("", "1", "1")]
        [InlineData("1", "", "")]
        [InlineData("1", "", "1")]
        [InlineData("1", "1", "")]
        public void CreateOrResumeCart_IfParameterIsEmpty_ShouldThrowArgumentException(string shopName, string userId, string customerId)
        {
            // act & assert
            Assert.Throws<ArgumentException>(
                () => this.cartManager.CreateOrResumeCart(
                    shopName,
                    userId,
                    customerId));
        }

        [Fact]
        public void CreateOrResumeCart_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.CreateOrResumeCart(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void CreateOrResumeCart_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.CreateOrResumeCart(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void AddCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(Cart cart, IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.AddCartLines(cart, cartLines));
        }

        [Fact]
        public void AddCartLines_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.AddCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void AddCartLines_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.AddCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
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
        public void AddShippingInfo_IfAddShippingInfoResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.AddShippingInfo(
                this.fixture.Create<Cart>(),
                this.fixture.Create<ShippingOptionType>(),
                this.fixture.Create<List<ShippingInfo>>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void AddShippingInfo_IfAddShippingInfoResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.addShippingInfoResult.Success = false;

            // act
            this.cartManager.AddShippingInfo(
                this.fixture.Create<Cart>(),
                this.fixture.Create<ShippingOptionType>(),
                this.fixture.Create<List<ShippingInfo>>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void UpdateCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(Cart cart, IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.UpdateCartLines(cart, cartLines));
        }

        [Fact]
        public void UpdateCartLines_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.UpdateCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void UpdateCartLines_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.UpdateCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Theory]
        [MemberData(nameof(CartLinesParameters))]
        public void RemoveCartLines_IfParameterIsNull_ShouldThrowArgumentNullException(Cart cart, IEnumerable<CartLine> cartLines)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.RemoveCartLines(cart, cartLines));
        }

        [Fact]
        public void RemoveCartLines_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.RemoveCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void RemoveCartLines_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.RemoveCartLines(this.fixture.Create<Cart>(), this.fixture.Create<IEnumerable<CartLine>>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Theory]
        [MemberData(nameof(MergeCartsParameters))]
        public void MergeCarts_IfParameterIsNull_ShouldThrowArgumentNullException(Cart fromCart, Cart toCart)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.cartManager.MergeCarts(fromCart, toCart));
        }

        [Fact]
        public void MergeCarts_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.MergeCarts(this.fixture.Create<Cart>(), this.fixture.Create<Cart>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void MergeCarts_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.cartResult.Success = false;

            // act
            this.cartManager.MergeCarts(this.fixture.Create<Cart>(), this.fixture.Create<Cart>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
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
        public void AddPromoCode_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.AddPromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void AddPromoCode_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.addPromoCodeResult.Success = false;

            // act
            this.cartManager.AddPromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Theory]
        [MemberData(nameof(PromoCodeParameters))]
        public void RemovePromoCode_IfParameterIsNull_ShouldThrowArgumentNullException(CommerceCart cart, string promoCode)
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
        public void RemovePromoCode_IfCartResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.cartManager.RemovePromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void RemovePromoCode_IfCartResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.removePromoCodeResult.Success = false;

            // act
            this.cartManager.RemovePromoCode(this.fixture.Create<CommerceCart>(), this.fixture.Create<string>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        //TODO: Refactor duplication of CreateOmitOnRecursionFixture
        /// <summary>
        /// Creates OmitOnRecursionBehavior as opposite to ThrowingRecursionBehavior with default recursion depth of 1.
        /// </summary>
        /// <returns></returns>
        private IFixture CreateOmitOnRecursionFixture()
        {
            var result = new Fixture();

            result.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => result.Behaviors.Remove(b));
            result.Behaviors.Add(new OmitOnRecursionBehavior());

            return result;
        }
    }
}