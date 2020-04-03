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

namespace Wooli.Feature.Checkout.Tests.Controllers
{
    using System;

    using Checkout.Controllers;

    using Foundation.Base.Models;
    using Foundation.Commerce.Models.Entities.Cart;
    using Foundation.Commerce.Services.Cart;

    using Models.Requests;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class CartsControllerTests
    {
        private readonly ICartService cartService;

        private readonly CartsController controller;

        private readonly IFixture fixture;

        public CartsControllerTests()
        {
            this.cartService = Substitute.For<ICartService>();
            this.controller = Substitute.For<CartsController>(this.cartService);
            this.fixture = new Fixture();
        }

        [Fact]
        public void AddPromoCode_ShouldCallExecuteMethod()
        {
            // act
            this.controller.AddPromoCode(this.fixture.Create<PromoCodeRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<Cart>>>());
        }

        [Fact]
        public void GetCart_ShouldCallExecuteMethod()
        {
            // act
            this.controller.GetCart();

            // assert
            this.controller.Received(1).Execute(this.cartService.GetCart);
        }

        // TODO: Tests are valid. Should be uncommented after fixing issue with Quantity validation
        //[Fact]
        //public void AddCartLine_ShouldCallExecuteMethod()
        //{
        //    // act
        //    this.controller.AddCartLine(this.fixture.Create<AddCartLineRequest>());

        //    // assert
        //    this.controller.Received(1).Execute(Arg.Any<Func<Result<Cart>>>());
        //}

        //[Fact]
        //public void UpdateCartLine_ShouldCallExecuteMethod()
        //{
        //    // act
        //    this.controller.UpdateCartLine(this.fixture.Create<UpdateCartLineRequest>());

        //    // assert
        //    this.controller.Received(1).Execute(Arg.Any<Func<Result<Cart>>>());
        //}

        [Fact]
        public void RemoveCartLine_ShouldCallExecuteMethod()
        {
            // act
            this.controller.RemoveCartLine(this.fixture.Create<RemoveCartLineRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<Cart>>>());
        }

        [Fact]
        public void RemovePromoCode_ShouldCallExecuteMethod()
        {
            // act
            this.controller.RemovePromoCode(this.fixture.Create<PromoCodeRequest>());

            // assert
            this.controller.Received(1).Execute(Arg.Any<Func<Result<Cart>>>());
        }
    }
}