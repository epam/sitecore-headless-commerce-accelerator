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

namespace Wooli.Foundation.Connect.Tests.Managers.Account
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Account;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers.Contracts;

    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services;
    using Sitecore.Commerce.Services.Customers;

    using Xunit;

    public class AccountManagerV2Tests
    {
        private readonly IAccountManagerV2 accountManager;
        private readonly ILogService<CommonLog> logService;
        private readonly CustomerServiceProvider customerServiceProvider;

        private readonly IFixture fixture;

        private readonly GetPartiesResult getPartiesResult;
        private readonly GetUserResult getUserResult;

        public AccountManagerV2Tests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();

            this.customerServiceProvider = Substitute.For<CustomerServiceProvider>();
            connectServiceProvider.GetCustomerServiceProvider().Returns(this.customerServiceProvider);
            this.fixture = new Fixture();
            this.getPartiesResult = this.fixture.Build<GetPartiesResult>()
                .With(res => res.Success, true)
                .With(res => res.Parties, null)
                .Create();
            this.getUserResult = this.fixture.Build<GetUserResult>()
                .With(res => res.Success, true)
                .Create();
            this.getPartiesResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.getUserResult.SystemMessages.Add(this.fixture.Create<SystemMessage>());
            this.logService = Substitute.For<ILogService<CommonLog>>();
            this.accountManager = new AccountManagerV2(connectServiceProvider, this.logService);

            this.customerServiceProvider.GetParties(Arg.Any<GetPartiesRequest>()).Returns(this.getPartiesResult);
            this.customerServiceProvider.GetUser(Arg.Any<GetUserRequest>()).Returns(this.getUserResult);
        }

        [Fact]
        public void GetCustomerParties_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetCustomerParties(null));
        }

        [Fact]
        public void GetCustomerParties_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.GetCustomerParties(string.Empty));
        }

        [Fact]
        public void GetCustomerParties_IfGetUserResultIsUnsuccessful_ShouldReturnEmptyGetPartiesResult()
        {
            // arrange
            this.getUserResult.Success = false;

            // act
            var actualGetPartiesResult = this.accountManager.GetCustomerParties(this.fixture.Create<string>());

            // assert
            Assert.Empty(actualGetPartiesResult.Parties);
        }

        [Fact]
        public void GetCustomerParties_IfGetUserResultIsSuccessful_ShouldCallGetPartiesMethod()
        {
            // act
            this.accountManager.GetCustomerParties(this.fixture.Create<string>());

            // assert
            this.customerServiceProvider.Received(1).GetParties(Arg.Any<GetPartiesRequest>());
        }

        [Fact]
        public void GetParties_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetParties(null));
        }

        [Fact]
        public void GetParties_IfGetPartiesResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.accountManager.GetParties(this.fixture.Create<CommerceCustomer>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetParties_IfGetPartiesResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.getPartiesResult.Success = false;

            // act
            this.accountManager.GetParties(this.fixture.Create<CommerceCustomer>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetUser_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetUser(null));
        }

        [Fact]
        public void GetUser_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.GetUser(string.Empty));
        }

        [Fact]
        public void GetUser_IfGetUserResultIsSuccessful_ShouldNotCallLogService()
        {
            // act
            this.accountManager.GetUser(this.fixture.Create<string>());

            // assert
            this.logService.Received(0).Error(Arg.Any<string>());
        }

        [Fact]
        public void GetUser_IfGetUserResultIsUnsuccessful_ShouldCallLogService()
        {
            // arrange
            this.getUserResult.Success = false;

            // act
            this.accountManager.GetUser(this.fixture.Create<string>());

            // assert
            this.logService.Received(1).Error(Arg.Any<string>());
        }
    }
}