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

namespace HCA.Feature.Checkout.Controllers
{
    using System.Web.Mvc;

    using Foundation.Base.Controllers;
    using Foundation.Commerce.Services.Cart;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class CartsController : BaseController
    {
        private readonly ICartService cartService;

        public CartsController(ICartService cartService)
        {
            Assert.ArgumentNotNull(cartService, nameof(cartService));
            this.cartService = cartService;
        }

        [HttpPost]
        [ActionName("cartLines")]
        public ActionResult AddCartLine(AddCartLineRequest request)
        {
            return this.Execute(
                () => this.cartService.AddCartLine(request.ProductId, request.VariantId, request.Quantity));
        }
        
        [HttpGet]
        [ActionName("cart")]
        public ActionResult GetCart()
        {
            return this.Execute(this.cartService.GetCart);
        }

        [HttpDelete]
        [ActionName("cartLines")]
        public ActionResult RemoveCartLine(string productId, string variantId)
        {
            Assert.ArgumentNotNull(productId, nameof(productId));
            Assert.ArgumentNotNull(variantId, nameof(variantId));
            return this.Execute(() => this.cartService.RemoveCartLine(productId, variantId));
        }

        [HttpDelete]
        [ActionName("cleanCart")]
        public ActionResult RemoveAllCartLine()
        {
            return this.Execute(() => this.cartService.CleanCartLines());
        }

        [HttpPut]
        [ActionName("cartLines")]
        public ActionResult UpdateCartLine(UpdateCartLineRequest request)
        {
            return this.Execute(
                () => this.cartService.UpdateCartLine(request.ProductId, request.VariantId, request.Quantity));
        }
    }
}