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

namespace HCA.Feature.Account.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Account.Controllers;
    using Account.Mappers;

    using Foundation.Base.Models.Result;
    using Foundation.Base.Services.Tracking;
    using Foundation.Commerce.Context;
    using Foundation.Commerce.Models.Entities.Account;
    using Foundation.Commerce.Models.Entities.Addresses;
    using Foundation.Commerce.Models.Entities.Users;
    using Foundation.Commerce.Services.Account;
    using HCA.Foundation.Account.Services.Authentication;
    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class AccountsControllerTests
    {
        private readonly AccountsController controller;

        private readonly Fixture fixture;

        public AccountsControllerTests()
        {
            var accountService = Substitute.For<IAccountService>();
            var mapper = Substitute.For<IAccountMapper>();
            var visitorContext = Substitute.For<IVisitorContext>();
            var trackingService = Substitute.For<ITrackingService>();
            var authenticationService = Substitute.For<IAuthenticationService>();

            this.controller = Substitute.For<AccountsController>(
                accountService,
                mapper,
                authenticationService,
                visitorContext,
                trackingService);

            this.fixture = new Fixture();
        }

        [Fact]
        public void AddAddress_ShouldCallExecuteMethod()
        {
            // act
            this.controller.AddAddress(new AddressRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<IEnumerable<Address>>>>());
        }

        [Fact]
        public void ChangePassword_ShouldCallExecuteMethod()
        {
            // act
            this.controller.ChangePassword(new ChangePasswordRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>(), Arg.Any<Func<Result<VoidResult>, ActionResult>>());
        }

        [Fact]
        public void CreateAccount_ShouldCallExecuteMethod()
        {
            // act
            this.controller.CreateAccount(new CreateAccountRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<User>>>(), Arg.Any<Func<Result<User>, ActionResult>>());
        }

        [Fact]
        public void DeleteAccount_ShouldCallExecuteMethod()
        {
            // act
            this.controller.DeleteAccount(new DeleteAccountRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>(), Arg.Any<Func<Result<VoidResult>, ActionResult>>());
        }

        [Fact]
        public void GetAddressList_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetAddresses();

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<IEnumerable<Address>>>>());
        }

        [Fact]
        public void RemoveAddress_ShouldCallExecuteMethod()
        {
            // act
            this.controller.RemoveAddress(this.fixture.Create<string>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<IEnumerable<Address>>>>());
        }

        [Fact]
        public void UpdateAccount_ShouldCallExecuteMethod()
        {
            // act
            this.controller.UpdateAccount(new UpdateAccountRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>());
        }

        [Fact]
        public void UpdateAddress_ShouldCallExecuteMethod()
        {
            // act
            this.controller.UpdateAddress(new AddressRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<IEnumerable<Address>>>>());
        }

        [Fact]
        public void ValidateAccount_ShouldCallExecuteMethod()
        {
            // act
            this.controller.ValidateEmail(new ValidateEmailRequest());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<ValidateEmailResult>>>());
        }
    }
}