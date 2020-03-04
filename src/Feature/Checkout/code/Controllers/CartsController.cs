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

namespace Wooli.Feature.Checkout.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Entities;
    using Foundation.Commerce.Services.Cart;
    using Foundation.Commerce.Utils;
    using Foundation.Extensions.Extensions;
    using Models;
    using Sitecore.Diagnostics;

    [RoutePrefix(Constants.CommerceRoutePrefix + "/carts")]
    public class CartsController : ApiController
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            Assert.ArgumentNotNull(cartService, nameof(cartService));
            this.cartService = cartService;
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetCart()
        {
            return this.Execute(this.cartService.GetCart);
        }

        [HttpPost, Route("cartLines")]
        public IHttpActionResult AddCartLine(CartLine cartLine)
        {
            return this.Execute(
                () =>
                {
                    var result = this.cartService.AddCartLine(new Cart(), cartLine);
                    return result;
                });
        }

        [HttpPut, Route("cartLines")]
        public IHttpActionResult UpdateCartLine(CartLine cartLine)
        {
            return this.Execute(
                () =>
                {
                    var result = this.cartService.UpdateCartLine(new Cart(), cartLine);
                    return result;
                });
        }

        [HttpDelete, Route("cartLines")]
        public IHttpActionResult RemoveCartLine(CartLine cartLine)
        {
            return this.Execute(
                () =>
                {
                    var result = this.cartService.RemoveCartLine(new Cart(), cartLine);
                    return result;
                });
        }

        [HttpPost, Route("promoCodes")]
        public IHttpActionResult AddPromoCode(PromoCodeDto data)
        {
            return this.Execute(
                () =>
                {
                    var result = this.cartService.AddPromoCode(new Cart(), data.PromoCode);
                    return result;
                });
        }

        [HttpDelete, Route("promoCodes")]
        public IHttpActionResult RemovePromoCode(PromoCodeDto data)
        {
            return this.Execute(
                () =>
                {
                    var result = this.cartService.RemovePromoCode(new Cart(), data.PromoCode);
                    return result;
                });
        }

        private IHttpActionResult Execute(Func<Result<Cart>> action)
        {
            try
            {
                var result = action.Invoke();
                return result.Success
                    ? this.JsonOk(result.Data)
                    : this.JsonError(
                        result.Errors?.ToArray(),
                        HttpStatusCode.InternalServerError,
                        tempData: result.Data);
            }
            catch (Exception exception)
            {
                return this.JsonError(exception.Message, HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}