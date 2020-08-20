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

namespace HCA.Foundation.Commerce.Services.Wishlist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base.Models.Result;

    using Builders.Wishlist;

    using Connect.Context.Storefront;
    using Connect.Managers.Wishlist;

    using Context;

    using DependencyInjection;

    using Mappers.Catalog;

    using Models.Entities.Catalog;
    using Models.Entities.Wishlist;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.WishLists;
    using Sitecore.Diagnostics;

    [Service(typeof(IWishlistService), Lifetime = Lifetime.Transient)]
    public class WishlistService : IWishlistService
    {
        private readonly ICatalogMapper catalogMapper;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        private readonly IWishlistBuilder wishlistBuilder;

        private readonly IWishlistManager wishlistManager;

        public WishlistService(
            IWishlistBuilder wishlistBuilder,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            IWishlistManager wishlistManager,
            ICatalogMapper catalogMapper)
        {
            Assert.ArgumentNotNull(wishlistBuilder, nameof(wishlistBuilder));
            Assert.ArgumentNotNull(storefrontContext, nameof(storefrontContext));
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));
            Assert.ArgumentNotNull(wishlistManager, nameof(wishlistManager));
            Assert.ArgumentNotNull(catalogMapper, nameof(catalogMapper));

            this.wishlistBuilder = wishlistBuilder;
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
            this.wishlistManager = wishlistManager;
            this.catalogMapper = catalogMapper;
        }

        public Result<Wishlist> AddWishlistLine(Variant product)
        {
            return this.ExecuteWithWishlist(
                (result, wishlist) =>
                {
                    var addLinesToWishListResult = this.wishlistManager.AddLinesToWishlist(
                        wishlist,
                        new List<WishListLine>
                        {
                            new WishListLine
                            {
                                Product = this.catalogMapper.Map<Variant, CommerceCartProduct>(product),
                                Quantity = 1m
                            }
                        });

                    if (addLinesToWishListResult.Success)
                    {
                        result.SetResult(this.wishlistBuilder.Build(addLinesToWishListResult.WishList));
                    }
                    else
                    {
                        result.SetErrors(addLinesToWishListResult.SystemMessages.Select(sm => sm.Message).ToList());
                    }

                    return result;
                });
        }

        public Result<Wishlist> GetWishlist()
        {
            return this.ExecuteWithWishlist(
                (result, wishlist) =>
                {
                    result.SetResult(this.wishlistBuilder.Build(wishlist));
                    return result;
                });
        }

        public Result<Wishlist> RemoveWishlistLine(string variantId)
        {
            return this.ExecuteWithWishlist(
                (result, wishlist) =>
                {
                    var removeWishListLinesResult = this.wishlistManager.RemoveWishlistLines(
                        wishlist,
                        new List<string>
                        {
                            variantId
                        });

                    if (removeWishListLinesResult.Success)
                    {
                        result.SetResult(this.wishlistBuilder.Build(removeWishListLinesResult.WishList));
                    }
                    else
                    {
                        result.SetErrors(removeWishListLinesResult.SystemMessages.Select(sm => sm.Message).ToList());
                    }

                    return result;
                });
        }

        private Result<Wishlist> ExecuteWithWishlist(Func<Result<Wishlist>, WishList, Result<Wishlist>> action)
        {
            var result = new Result<Wishlist>();

            var getWishListResult = this.wishlistManager.GetWishlist(
                this.visitorContext.ContactId,
                this.storefrontContext.ShopName);

            if (getWishListResult.Success)
            {
                return action(result, getWishListResult.WishList);
            }

            result.SetErrors(getWishListResult.SystemMessages.Select(sm => sm.Message).ToList());
            return result;
        }
    }
}