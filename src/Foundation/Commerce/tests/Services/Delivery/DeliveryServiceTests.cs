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

namespace HCA.Foundation.Commerce.Tests.Services.Delivery
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Base.Tests.Customization;

    using Commerce.Mappers.Shipping;
    using Commerce.Services.Delivery;

    using Connect.Context.Storefront;
    using Connect.Managers.Account;
    using Connect.Managers.Cart;
    using Connect.Managers.Shipping;

    using Context;

    using Models.Entities.Addresses;

    using NSubstitute;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Shipping;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Carts;
    using Sitecore.Commerce.Services.Customers;
    using Sitecore.Commerce.Services.Shipping;

    using Xunit;

    using ShippingMethod = Models.Entities.Shipping.ShippingMethod;

    public class DeliveryServiceTests
    {
        private readonly IAccountManager accountManager;

        private readonly ICartManager cartManager;

        private readonly IDeliveryService deliveryService;
        private readonly IFixture fixture;

        private readonly IShippingManager shippingManager;

        private readonly IShippingMapper shippingMapper;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        private CartResult cartResult;

        public DeliveryServiceTests()
        {
            this.accountManager = Substitute.For<IAccountManager>();
            this.cartManager = Substitute.For<ICartManager>();
            this.shippingManager = Substitute.For<IShippingManager>();
            this.shippingMapper = Substitute.For<IShippingMapper>();
            this.visitorContext = Substitute.For<IVisitorContext>();
            this.storefrontContext = Substitute.For<IStorefrontContext>();

            this.fixture = new Fixture().Customize(new OmitOnRecursionCustomization());

            this.deliveryService = new DeliveryService(
                this.accountManager,
                this.cartManager,
                this.storefrontContext,
                this.visitorContext,
                this.shippingMapper,
                this.shippingManager);
        }

        [Fact]
        public void GetDeliveryInfo_IfCartIsEmpty_ShouldReturnNotNullResult()
        {
            // arrange
            this.ArrageGetDeliveryInfo();
            this.cartResult.Cart.Lines.Clear();

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            this.cartManager.Received(1).LoadCart(Arg.Any<string>(), Arg.Any<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDeliveryInfo_IfCartIsNotEmpty_ShouldAddShippingOptions()
        {
            // arrange
            this.ArrageGetDeliveryInfo();

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            this.shippingMapper.Received(1)
                .Map<IReadOnlyCollection<ShippingOption>, List<Models.Entities.Shipping.ShippingOption>>(
                    Arg.Any<ReadOnlyCollection<ShippingOption>>());
            Assert.NotEmpty(result.Data.ShippingOptions);
        }

        [Fact]
        public void GetDeliveryInfo_IfCartIsNotEmpty_ShouldAddUserInfo()
        {
            // arrange
            this.ArrageGetDeliveryInfo();

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            Assert.NotEmpty(result.Data.UserAddresses);
        }

        [Fact]
        public void GetDeliveryInfo_IfGetCustomerPartiesWasUnsuccessful_ShouldSetErrors()
        {
            // arrange
            this.ArrageGetDeliveryInfo(isGetCustomerPartiesFailed: true);

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetDeliveryInfo_IfGetShippingOptionsWasUnsuccessful_ShouldSetErrors()
        {
            // arrange
            this.ArrageGetDeliveryInfo(true);

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetDeliveryInfo_IfLoadCartWasUnsuccessful_ShouldSetErrors()
        {
            // arrange
            this.ArrageGetDeliveryInfo();
            this.cartResult.Success = false;
            this.cartResult.SystemMessages.Add(new SystemMessage("Test message"));

            // act
            var result = this.deliveryService.GetDeliveryInfo();

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetShippingInfo_IfLoadCartWasUnsuccessful_ShouldSetErrors()
        {
            // arrange
            this.ArrangeGetShippingInfo();
            this.cartResult.Success = false;
            this.cartResult.SystemMessages.Add(new SystemMessage("Test message"));

            // act
            var result = this.deliveryService.GetShippingInfo();

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetShippingInfo_SetShippingOptions_ShouldSetErrors()
        {
            // arrange
            this.ArrangeSetShippingOptions();
            this.cartResult.Success = false;
            this.cartResult.SystemMessages.Add(new SystemMessage("Test message"));

            // act
            var result = this.deliveryService.SetShippingOptions(
                "testShippingPreferenceType",
                new List<Address>(),
                new List<ShippingMethod>());

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void GetShippingInfo_ShouldGetShippingMethods()
        {
            // arrange
            this.ArrangeGetShippingInfo();

            // act
            var result = this.deliveryService.GetShippingInfo();

            // assert
            this.shippingManager.Received(1).GetShippingMethods(Arg.Any<Cart>(), Arg.Any<ShippingOptionType>());
            this.shippingMapper.Received(1)
                .Map<IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>, List<ShippingMethod>>(
                    Arg.Any<ReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>>());
            Assert.NotEmpty(result.Data.ShippingMethods);
        }

        [Fact]
        public void GetShippingInfo_ShouldMapShippingMethods()
        {
            // arrange
            this.ArrangeGetShippingInfo(true);

            // act
            var result = this.deliveryService.GetShippingInfo();

            // assert
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void SetShippingOptions_ShouldAddShippingInfo()
        {
            // arrange
            this.ArrangeSetShippingOptions();

            // act
            var result = this.deliveryService.SetShippingOptions(
                "testShippingPreferenceType",
                new List<Address>(),
                new List<ShippingMethod>());

            // assert
            this.cartManager.Received(1)
                .AddShippingInfo(Arg.Any<Cart>(), Arg.Any<ShippingOptionType>(), Arg.Any<List<ShippingInfo>>());
        }

        private void ArrageGetDeliveryInfo(
            bool isGetShippingOptionsFailed = false,
            bool isGetCustomerPartiesFailed = false)
        {
            this.cartResult = this.fixture.Create<CartResult>();
            this.cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(this.cartResult);

            var shippingOptionsResult = this.fixture.Create<GetShippingOptionsResult>();
            this.shippingManager.GetShippingOptions(Arg.Any<Cart>()).Returns(shippingOptionsResult);

            var mappedShippingOptions = this.fixture.Create<List<Models.Entities.Shipping.ShippingOption>>();
            this.shippingMapper
                .Map<IReadOnlyCollection<ShippingOption>, List<Models.Entities.Shipping.ShippingOption>>(
                    shippingOptionsResult.ShippingOptions)
                .Returns(mappedShippingOptions);

            this.fixture.Customizations.Add(new TypeRelay(typeof(IReadOnlyCollection<Party>), typeof(List<Party>)));
            var partiesResult = this.fixture.Create<GetPartiesResult>();
            this.accountManager.GetCustomerParties(Arg.Any<string>()).Returns(partiesResult);

            if (isGetShippingOptionsFailed)
            {
                shippingOptionsResult.Success = false;
                shippingOptionsResult.SystemMessages.Add(new SystemMessage("Test message"));
            }

            if (isGetCustomerPartiesFailed)
            {
                partiesResult.Success = false;
                partiesResult.SystemMessages.Add(new SystemMessage("Test message"));
            }
        }

        private void ArrangeGetShippingInfo(bool isGetShippingMethodsFailed = false)
        {
            this.cartResult = this.fixture.Create<CartResult>();
            this.cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(this.cartResult);

            var shippingMethodsResult = this.fixture.Create<GetShippingMethodsResult>();
            this.shippingManager.GetShippingMethods(Arg.Any<Cart>(), Arg.Any<ShippingOptionType>())
                .Returns(shippingMethodsResult);

            var mappedShippingMethods = this.fixture.Create<List<ShippingMethod>>();
            this.fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>),
                    typeof(List<ShippingMethod>)));
            this.shippingMapper
                .Map<IReadOnlyCollection<Sitecore.Commerce.Entities.Shipping.ShippingMethod>, List<ShippingMethod>>(
                    shippingMethodsResult.ShippingMethods)
                .Returns(mappedShippingMethods);

            if (isGetShippingMethodsFailed)
            {
                shippingMethodsResult.Success = false;
                shippingMethodsResult.SystemMessages.Add(new SystemMessage("Test message"));
            }
        }

        private void ArrangeSetShippingOptions()
        {
            this.cartResult = this.fixture.Create<CartResult>();
            this.cartResult.Success = true;
            this.cartManager.LoadCart(Arg.Any<string>(), Arg.Any<string>()).Returns(this.cartResult);

            var commerceParties = this.fixture.Create<List<CommerceParty>>();
            var shippingOptionType = this.fixture.Create<ShippingOptionType>();
            this.shippingMapper.Map<List<Address>, List<CommerceParty>>(Arg.Any<List<Address>>())
                .Returns(commerceParties);
            this.shippingMapper.Map<string, ShippingOptionType>(Arg.Any<string>()).Returns(shippingOptionType);

            this.fixture.Customizations.Add(
                new TypeRelay(typeof(IReadOnlyCollection<ShippingInfo>), typeof(List<ShippingInfo>)));
            var addShippingInfoResult = this.fixture.Create<AddShippingInfoResult>();
            this.cartManager.AddShippingInfo(
                    Arg.Any<Cart>(),
                    Arg.Any<ShippingOptionType>(),
                    Arg.Any<List<ShippingInfo>>())
                .Returns(addShippingInfoResult);
        }
    }
}