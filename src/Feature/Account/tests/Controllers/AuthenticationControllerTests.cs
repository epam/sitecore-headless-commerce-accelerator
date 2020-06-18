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
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Account.Controllers;

    using Foundation.Account.Models.Authentication;
    using Foundation.Account.Services.Authentication;
    using Foundation.Base.Models.Result;

    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class AuthenticationControllerTests
    {
        private readonly IAuthenticationService authenticationService;

        private readonly AuthenticationController controller;

        private readonly AuthenticationController controllerSubstitute;

        private readonly IFixture fixture;

        private readonly LoginRequest loginRequest;

        public AuthenticationControllerTests()
        {
            this.authenticationService = Substitute.For<IAuthenticationService>();
            this.fixture = new Fixture();

            var httpContext = Substitute.For<HttpContextBase>();
            httpContext.Response.Returns(Substitute.For<HttpResponseBase>());

            this.controller = new AuthenticationController(this.authenticationService);
            this.controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), this.controller);

            this.controllerSubstitute = Substitute.For<AuthenticationController>(this.authenticationService);

            this.loginRequest = this.fixture.Create<LoginRequest>();
        }

        [Fact]
        public void Login_IfAuthenticationServiceLoginIsNotSuccessAndCredentialsAreNotValid_ShouldReturnBadRequest()
        {
            // arrange
            var loginResult = this.fixture.Build<LoginResult>()
                .With(res => res.IsInvalidCredentials, true)
                .Create();

            var failResult = new Result<LoginResult>(loginResult)
            {
                Success = false
            };
            this.authenticationService
                .Login(this.loginRequest.Email, this.loginRequest.Password)
                .Returns(failResult);

            // act
            var jsonResult = this.controller.Login(this.loginRequest) as CamelCasePropertyJsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
            Assert.Equal(HttpStatusCode.BadRequest, jsonResult?.StatusCode);
        }

        [Fact]
        public void
            Login_IfAuthenticationServiceLoginIsNotSuccessAndCredentialsAreValid_ShouldReturnInternalServerError()
        {
            // arrange
            var loginResult = this.fixture.Build<LoginResult>()
                .With(res => res.IsInvalidCredentials, false)
                .Create();

            var result = new Result<LoginResult>(loginResult)
            {
                Success = false
            };
            this.authenticationService
                .Login(this.loginRequest.Email, this.loginRequest.Password)
                .Returns(result);

            // act
            var jsonResult = this.controller.Login(this.loginRequest) as CamelCasePropertyJsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
            Assert.Equal(HttpStatusCode.InternalServerError, jsonResult?.StatusCode);
        }

        [Fact]
        public void Login_IfAuthenticationServiceLoginIsSuccess_ShouldReturnOK()
        {
            this.authenticationService
                .Login(this.loginRequest.Email, this.loginRequest.Password)
                .Returns(new Result<LoginResult>(this.fixture.Create<LoginResult>()));

            // act
            var jsonResult = this.controller.Login(this.loginRequest) as CamelCasePropertyJsonResult;

            // assert
            Assert.Equal(HttpStatusCode.OK, jsonResult?.StatusCode);
            this.authenticationService.Received(1).Login(this.loginRequest.Email, this.loginRequest.Password);
        }

        [Fact]
        public void Login_ShouldCallExecuteMethod()
        {
            // act
            this.controllerSubstitute.Login(this.loginRequest);

            // assert
            this.controllerSubstitute.Received(1)
                .Execute(
                    Arg.Any<Func<Result<LoginResult>>>(),
                    Arg.Any<Func<Result<LoginResult>, ActionResult>>());
        }

        [Fact]
        public void Logout_ShouldCallExecuteMethod()
        {
            // act
            this.controllerSubstitute.Logout();

            // assert
            this.controllerSubstitute.Received(1).Execute(Arg.Any<Func<Result<VoidResult>>>());
        }
    }
}