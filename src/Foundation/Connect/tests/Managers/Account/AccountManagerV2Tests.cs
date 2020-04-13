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

    using Providers;

    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;

    using Xunit;

    public class AccountManagerV2Tests
    {
        private readonly AccountManagerV2 accountManager;

        private readonly CustomerServiceProvider customerServiceProvider;

        private readonly IFixture fixture;

        private readonly GetUserResult getUserResult;

        private readonly ILogService<CommonLog> logService;

        public AccountManagerV2Tests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();

            this.customerServiceProvider = Substitute.For<CustomerServiceProvider>();
            connectServiceProvider.GetCustomerServiceProvider().Returns(this.customerServiceProvider);
            this.fixture = new Fixture();
            this.getUserResult = this.fixture.Build<GetUserResult>()
                .With(res => res.Success, true)
                .Create();
            this.logService = Substitute.For<ILogService<CommonLog>>();

            this.accountManager = Substitute.For<AccountManagerV2>(connectServiceProvider, this.logService);

            this.accountManager.Execute(Arg.Any<GetUserRequest>(), this.customerServiceProvider.GetUser)
                .Returns(this.getUserResult);
        }

        [Fact]
        public void GetCustomerParties_IfGetUserResultIsSuccessful_ShouldCallExecuteMethodWithGetPartiesMethod()
        {
            // act
            this.accountManager.GetCustomerParties(this.fixture.Create<string>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<GetPartiesRequest>(), this.customerServiceProvider.GetParties);
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
        public void GetCustomerParties_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.GetCustomerParties(string.Empty));
        }

        [Fact]
        public void GetCustomerParties_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetCustomerParties(null));
        }

        [Fact]
        public void GetParties_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetParties(null));
        }

        [Fact]
        public void GetParties_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.GetParties(this.fixture.Create<CommerceCustomer>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<GetPartiesRequest>(), this.customerServiceProvider.GetParties);
        }

        [Fact]
        public void GetUser_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.GetUser(string.Empty));
        }

        [Fact]
        public void GetUser_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetUser(null));
        }

        [Fact]
        public void GetUser_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.GetUser(this.fixture.Create<string>());

            // assert
            this.accountManager.Received(1).Execute(Arg.Any<GetUserRequest>(), this.customerServiceProvider.GetUser);
        }
    }
}