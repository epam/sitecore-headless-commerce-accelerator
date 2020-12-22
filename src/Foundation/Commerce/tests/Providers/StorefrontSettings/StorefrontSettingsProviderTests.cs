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

namespace HCA.Foundation.Commerce.Tests.Providers.StorefrontSettings
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoFixture;

    using Commerce.Providers.StorefrontSettings;

    using Connect.Context.Storefront;
    using Connect.Models;

    using NSubstitute;



    using Sitecore.FakeDb.AutoFixture;

    using Xunit;

    public class StorefrontSettingsProviderTests
    {
        private readonly StorefrontSettingsProvider storefrontSettingsProvider;

        private readonly IStorefrontContext storefrontContext;

        private readonly PaymentConfigurationModel paymentSettingsReturns;

        private readonly IFixture fixture;

        public StorefrontSettingsProviderTests()
        {
            this.fixture = new Fixture().Customize(new AutoDbCustomization());

            this.storefrontContext = Substitute.For<IStorefrontContext>();

            var storefrontConfiguration = Substitute.For<StorefrontModel>();
            this.paymentSettingsReturns = Substitute.For<PaymentConfigurationModel>();
            this.storefrontContext.StorefrontConfiguration.Returns(storefrontConfiguration);
            storefrontConfiguration.PaymentSettings.Returns(this.paymentSettingsReturns);

            this.storefrontSettingsProvider = new StorefrontSettingsProvider(this.storefrontContext);
        }

        [Fact]
        public void GetPaymentOptionId_IfPaymentOptionsIsNull_ShouldReturnEmptyString()
        {
            // act
            var optionId = this.storefrontSettingsProvider.GetPaymentOptionId(this.fixture.Create<string>());

            // assert
            Assert.Equal(string.Empty, optionId);
        }

        [Fact]
        public void GetPaymentOptionId_IfPaymentOptionsIsEmpty_ShouldReturnEmptyString()
        {
            // arrange
            this.paymentSettingsReturns.SelectedPaymentOptions.Returns(Enumerable.Empty<PaymentOptionModel>());

            // act
            var optionId = this.storefrontSettingsProvider.GetPaymentOptionId(this.fixture.Create<string>());

            // assert
            Assert.Equal(string.Empty, optionId);
        }

        [Fact]
        public void GetPaymentOptionId_IfTitleExistsInOptions_ShouldReturnId()
        {
            // arrange
            var paymentOptions = this.fixture.Create<List<PaymentOptionModel>>();
            this.paymentSettingsReturns.SelectedPaymentOptions.Returns(paymentOptions);
            var optionTitle = paymentOptions.First().Title;
            var expectedId = paymentOptions.First().Id.ToString("D");

            // act
            var optionId = this.storefrontSettingsProvider.GetPaymentOptionId(optionTitle);

            // assert
            Assert.Equal(expectedId, optionId);
        }

        [Fact]
        public void GetPaymentOptionId_IfTitleDoesNotExistInOptions_ShouldReturnEmptyString()
        {
            // arrange
            var paymentOptions = this.fixture.Create<List<PaymentOptionModel>>();
            this.paymentSettingsReturns.SelectedPaymentOptions.Returns(paymentOptions);

            // act
            var optionId = this.storefrontSettingsProvider.GetPaymentOptionId(this.fixture.Create<string>());

            // assert
            Assert.Equal(string.Empty, optionId);
        }
    }
}