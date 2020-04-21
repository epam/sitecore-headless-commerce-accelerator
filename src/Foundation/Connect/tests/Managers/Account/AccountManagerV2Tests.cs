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

namespace HCA.Foundation.Connect.Tests.Managers.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Connect.Managers.Account;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Providers;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Services.Customers;

    using Xunit;

    public class AccountManagerV2Tests
    {
        private readonly AccountManagerV2 accountManager;

        private readonly CustomerServiceProvider customerServiceProvider;

        private readonly IFixture fixture;

        public AccountManagerV2Tests()
        {
            var connectServiceProvider = Substitute.For<IConnectServiceProvider>();
            var logService = Substitute.For<ILogService<CommonLog>>();

            this.customerServiceProvider = Substitute.For<CustomerServiceProvider>();
            connectServiceProvider.GetCustomerServiceProvider().Returns(this.customerServiceProvider);

            this.accountManager = Substitute.For<AccountManagerV2>(connectServiceProvider, logService);

            this.fixture = new Fixture();
        }

        public static IEnumerable<object[]> PartiesParameters =>
            new List<object[]>
            {
                new object[] { null, Enumerable.Empty<Party>() },
                new object[] { new CommerceCustomer(), null }
            };

        #region GetUsers

        [Fact]
        public void GetUsers_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.GetUsers(this.fixture.Create<UserSearchCriteria>());

            // assert
            this.accountManager.Received(1).Execute(Arg.Any<GetUsersRequest>(), this.customerServiceProvider.GetUsers);
        }

        #endregion

        #region AddParties

        [Theory]
        [MemberData(nameof(PartiesParameters))]
        public void AddParties_IfParameterIsNull_ShouldThrowArgumentNullException(
            CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.AddParties(customer, parties));
        }

        [Fact]
        public void AddParties_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.AddParties(this.fixture.Create<CommerceCustomer>(), this.fixture.Create<List<Party>>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<AddPartiesRequest>(), this.customerServiceProvider.AddParties);
        }

        #endregion

        #region CreateUser

        [Theory]
        [InlineData("", "1", "1", "1")]
        [InlineData("1", "", "1", "1")]
        [InlineData("1", "1", "", "1")]
        [InlineData("1", "1", "1", "")]
        public void CreateUser_IfParameterIsEmpty_ShouldThrowArgumentException(
            string userName,
            string email,
            string password,
            string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.CreateUser(userName, email, password, shopName));
        }

        [Theory]
        [InlineData(null, "1", "1", "1")]
        [InlineData("1", null, "1", "1")]
        [InlineData("1", "1", null, "1")]
        [InlineData("1", "1", "1", null)]
        public void CreateUser_IfParameterIsNull_ShouldThrowArgumentNullException(
            string userName,
            string email,
            string password,
            string shopName)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => this.accountManager.CreateUser(userName, email, password, shopName));
        }

        [Fact]
        public void CreateUser_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.CreateUser(
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>(),
                this.fixture.Create<string>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<CreateUserRequest>(), this.customerServiceProvider.CreateUser);
        }

        #endregion

        #region EnableUser

        [Fact]
        public void EnableUser_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.EnableUser(null));
        }

        [Fact]
        public void EnableUser_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.EnableUser(this.fixture.Create<CommerceUser>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<EnableUserRequest>(), this.customerServiceProvider.EnableUser);
        }

        #endregion

        #region GetCustomer

        [Fact]
        public void GetCustomer_IfParameterIsEmpty_ShouldThrowArgumentException()
        {
            // act & assert
            Assert.Throws<ArgumentException>(() => this.accountManager.GetCustomer(string.Empty));
        }

        [Fact]
        public void GetCustomer_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.GetCustomer(null));
        }

        [Fact]
        public void GetCustomer_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.GetCustomer(this.fixture.Create<string>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<GetCustomerRequest>(), this.customerServiceProvider.GetCustomer);
        }

        #endregion

        #region GetCustomerParties

        [Fact]
        public void GetCustomerParties_IfGetUserResultIsSuccessful_ShouldCallExecuteMethodWithGetPartiesMethod()
        {
            // arrange
            this.InitGetCustomerParties(true);

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
            this.InitGetCustomerParties(false);

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

        private void InitGetCustomerParties(bool getUserSuccess)
        {
            var getUserResult = this.fixture.Build<GetUserResult>()
                .With(res => res.Success, getUserSuccess)
                .Create();

            this.accountManager.Execute(Arg.Any<GetUserRequest>(), this.customerServiceProvider.GetUser)
                .Returns(getUserResult);
        }

        #endregion

        #region GetParties

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

        #endregion

        #region GetUser

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

        #endregion

        #region RemoveParties

        [Theory]
        [MemberData(nameof(PartiesParameters))]
        public void RemoveParties_IfParameterIsNull_ShouldThrowArgumentNullException(
            CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.RemoveParties(customer, parties));
        }

        [Fact]
        public void RemoveParties_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.RemoveParties(
                this.fixture.Create<CommerceCustomer>(),
                this.fixture.Create<List<Party>>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<RemovePartiesRequest>(), this.customerServiceProvider.RemoveParties);
        }

        #endregion

        #region UpdateParties

        [Theory]
        [MemberData(nameof(PartiesParameters))]
        public void UpdateParties_IfParameterIsNull_ShouldThrowArgumentNullException(
            CommerceCustomer customer,
            IEnumerable<Party> parties)
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.UpdateParties(customer, parties));
        }

        [Fact]
        public void UpdateParties_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.UpdateParties(
                this.fixture.Create<CommerceCustomer>(),
                this.fixture.Create<List<Party>>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<UpdatePartiesRequest>(), this.customerServiceProvider.UpdateParties);
        }

        #endregion

        #region UpdateUser

        [Fact]
        public void UpdateUser_IfParameterIsNull_ShouldThrowArgumentNullException()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => this.accountManager.UpdateUser(null));
        }

        [Fact]
        public void UpdateUser_ShouldCallExecuteMethod()
        {
            // act
            this.accountManager.UpdateUser(this.fixture.Create<CommerceUser>());

            // assert
            this.accountManager.Received(1)
                .Execute(Arg.Any<UpdateUserRequest>(), this.customerServiceProvider.UpdateUser);
        }

        #endregion
    }
}