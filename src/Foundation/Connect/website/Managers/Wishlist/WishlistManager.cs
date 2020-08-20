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

namespace HCA.Foundation.Connect.Managers.Wishlist
{
    using System.Collections.Generic;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Providers;

    using Sitecore.Commerce.Entities.WishLists;
    using Sitecore.Commerce.Services.WishLists;
    using Sitecore.Diagnostics;

    [Service(typeof(IWishlistManager), Lifetime = Lifetime.Singleton)]
    public class WishlistManager : BaseManager, IWishlistManager
    {
        private readonly WishListServiceProvider wishlistServiceProvider;

        public WishlistManager(
            IConnectServiceProvider connectServiceProvider,
            ILogService<CommonLog> logService) : base(logService)
        {
            Assert.ArgumentNotNull(connectServiceProvider, nameof(connectServiceProvider));

            this.wishlistServiceProvider = connectServiceProvider.GetWishListServiceProvider();
        }

        public AddLinesToWishListResult AddLinesToWishlist(WishList wishlist, IEnumerable<WishListLine> lines)
        {
            Assert.ArgumentNotNull(wishlist, nameof(wishlist));
            Assert.ArgumentNotNull(lines, nameof(lines));

            return this.Execute(
                new AddLinesToWishListRequest(wishlist, lines),
                this.wishlistServiceProvider.AddLinesToWishList);
        }

        public GetWishListResult GetWishlist(string userId, string shopName)
        {
            Assert.ArgumentNotNullOrEmpty(userId, nameof(userId));
            Assert.ArgumentNotNullOrEmpty(shopName, nameof(shopName));

            return this.Execute(
                new GetWishListRequest(userId, Constants.DefaultWishlistName, shopName),
                this.wishlistServiceProvider.GetWishList);
        }

        public RemoveWishListLinesResult RemoveWishlistLines(WishList wishlist, IEnumerable<string> lineIds)
        {
            Assert.ArgumentNotNull(wishlist, nameof(wishlist));
            Assert.ArgumentNotNull(lineIds, nameof(lineIds));

            return this.Execute(
                new RemoveWishListLinesRequest(wishlist, lineIds),
                this.wishlistServiceProvider.RemoveWishListLines);
        }
    }
}