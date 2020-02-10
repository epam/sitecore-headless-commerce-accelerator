//    Copyright 2019 EPAM Systems, Inc.
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
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Mvc;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Models.Checkout;
    using Foundation.Commerce.Repositories;
    using Foundation.Extensions.Extensions;
    using Models;

    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        [System.Web.Mvc.ActionName("get")]
        public ActionResult GetCart()
        {
            var model = cartRepository.GetCurrentCart();

            return this.JsonOk(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("add")]
        public ActionResult AddProductVariant([FromBody] CartItemDto cartItem)
        {
            var result =
                cartRepository.AddProductVariantToCart(cartItem.ProductId, cartItem.VariantId, cartItem.Quantity);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("update")]
        public ActionResult Update([FromBody] CartItemDto cartItem)
        {
            var result =
                cartRepository.UpdateProductVariantQuantity(cartItem.ProductId, cartItem.VariantId, cartItem.Quantity);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("addpromo")]
        public ActionResult AddPromoCode([FromBody] PromoCodeDto data)
        {
            var result = cartRepository.AddPromoCode(data.PromoCode);

            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }

        [System.Web.Mvc.HttpDelete]
        [System.Web.Mvc.ActionName("remove")]
        public ActionResult DeleteVariant(string cartLineId)
        {
            var result = cartRepository.RemoveProductVariantFromCart(cartLineId);
            if (result.Success) return this.JsonOk(result.Data);

            return this.JsonError(result.Errors?.ToArray(), HttpStatusCode.InternalServerError, tempData: result.Data);
        }
    }
}