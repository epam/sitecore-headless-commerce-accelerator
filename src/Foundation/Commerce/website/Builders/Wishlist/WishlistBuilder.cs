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

namespace HCA.Foundation.Commerce.Builders.Wishlist
{
    using System.Linq;

    using DependencyInjection;

    using Mappers.Wishlist;

    using Models.Entities.Wishlist;

    using Services.Catalog;

    using Sitecore.Commerce.Entities.WishLists;
    using Sitecore.Diagnostics;

    [Service(typeof(IWishlistBuilder), Lifetime = Lifetime.Singleton)]
    public class WishlistBuilder : IWishlistBuilder
    {
        private readonly ICatalogService catalogService;

        private readonly IWishlistMapper mapper;

        public WishlistBuilder(ICatalogService catalogService, IWishlistMapper wishlistMapper)
        {
            Assert.ArgumentNotNull(catalogService, nameof(catalogService));
            Assert.ArgumentNotNull(wishlistMapper, nameof(wishlistMapper));

            this.catalogService = catalogService;
            this.mapper = wishlistMapper;
        }

        public Wishlist Build(WishList source)
        {
            if (source == null)
            {
                return null;
            }

            var wishlist = this.mapper.Map(source);
            wishlist.Lines = source.Lines.Select(
                    line =>
                    {
                        var wishlistLine = this.mapper.Map(line);
                        var product = this.catalogService.GetProduct(line.Product.ProductId);
                        if (product.Success)
                        {
                            wishlistLine.Product = product.Data;
                        }

                        return wishlistLine;
                    })
                .ToList();

            return wishlist;
        }
    }
}