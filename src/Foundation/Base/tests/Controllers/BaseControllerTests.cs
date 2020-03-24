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

namespace Wooli.Foundation.Base.Tests.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Base.Controllers;

    using Extensions.Models;

    using Models;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class BaseControllerTests
    {
        private readonly BaseController controller;

        private readonly IFixture fixture;

        public BaseControllerTests()
        {
            this.fixture = new Fixture();

            var httpContext = Substitute.For<HttpContextBase>();
            httpContext.Response.Returns(Substitute.For<HttpResponseBase>());

            this.controller = new BaseController();
            this.controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), this.controller);
        }

        [Fact]
        public void Execute_IfExceptionIsThrownInAction_ShouldReturnErrorResponse()
        {
            // act
            var jsonResult = this.controller.Execute<object>(() => throw new NotImplementedException()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void Execute_IfActionResultIsUnsuccessful_ShouldReturnErrorResponse()
        {
            // arrange
            var result = new Result<object>
            {
                Success = false
            };

            // act
            var jsonResult = this.controller.Execute(() => result) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void Execute_IfModelIsInvalid_ShouldReturnErrorResponse()
        {
            // arrange
            this.controller.ModelState.AddModelError(this.fixture.Create<string>(), this.fixture.Create<string>());

            // act
            var jsonResult = this.controller.Execute(() => new Result<object>()) as JsonResult;
            var errorResult = jsonResult?.Data as ErrorJsonResultModel;

            // assert
            Assert.NotNull(errorResult);
        }

        [Fact]
        public void Execute_IfServiceResultIsSuccessful_ShouldReturnSuccessResponse()
        {
            // act
            var jsonResult = this.controller.Execute(() => new Result<object>()) as JsonResult;
            var okResult = jsonResult?.Data as OkJsonResultModel<object>;

            // assert
            Assert.NotNull(okResult);
        }
    }
}