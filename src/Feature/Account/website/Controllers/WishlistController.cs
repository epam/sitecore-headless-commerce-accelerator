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

namespace HCA.Feature.Account.Controllers
{
    using System.Web.Mvc;

    using Foundation.Base.Controllers;
    using Foundation.Commerce.Services.Wishlist;

    using Mappers.Wishlist;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class WishlistController : BaseController
    {
        private readonly IWishlistMapper mapper;

        private readonly IWishlistService wishlistService;

        public WishlistController(IWishlistMapper wishlistMapper, IWishlistService wishlistService)
        {
            Assert.ArgumentNotNull(wishlistMapper, nameof(wishlistMapper));
            Assert.ArgumentNotNull(wishlistService, nameof(wishlistService));

            this.mapper = wishlistMapper;
            this.wishlistService = wishlistService;
        }

        [HttpPost]
        [ActionName("wishlist")]
        public ActionResult AddWishlistLine(VariantRequest request)
        {
            return this.Execute(() => this.wishlistService.AddWishlistLine(this.mapper.Map(request)));
        }

        [HttpGet]
        [ActionName("wishlist")]
        public ActionResult GetWishlist()
        {
            return this.Execute(this.wishlistService.GetWishlist);
        }

        [HttpDelete]
        [ActionName("wishlist")]
        public ActionResult RemoveWishlistLine(string variantId)
        {
            return this.Execute(() => this.wishlistService.RemoveWishlistLine(variantId));
        }
    }
}