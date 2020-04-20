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

namespace HCA.Foundation.Commerce.Builders.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using DependencyInjection;

    using Mappers.Cart;

    using Models.Entities.Cart;

    using Services.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Diagnostics;

    using Connect = Sitecore.Commerce.Entities.Carts;

    [Service(typeof(ICartBuilder<Connect.Cart>), Lifetime = Lifetime.Singleton)]
    public class CartBuilder : ICartBuilder<Connect.Cart>
    {
        private readonly ICartMapper cartMapper;

        private readonly ICatalogService catalogService;

        public CartBuilder(ICatalogService catalogService, ICartMapper cartMapper)
        {
            Assert.ArgumentNotNull(catalogService, nameof(catalogService));
            Assert.ArgumentNotNull(cartMapper, nameof(cartMapper));

            this.catalogService = catalogService;
            this.cartMapper = cartMapper;
        }

        public Cart Build(Connect.Cart source)
        {
            if (source == null)
            {
                return null;
            }

            var cart = this.cartMapper.Map<Connect.Cart, Cart>(source);
            cart.CartLines = this.BuildCartLines(source.Lines);

            return cart;
        }

        private List<CartLine> BuildCartLines(List<Connect.CartLine> sourceCartLines)
        {
            var cartLines = new List<CartLine>();

            foreach (var sourceCartLine in sourceCartLines)
            {
                var cartLine = this.cartMapper.Map<Connect.CartLine, CartLine>(sourceCartLine);
                this.FillProductData(sourceCartLine, cartLine);
                cartLines.Add(cartLine);
            }

            return cartLines;
        }

        private void FillProductData(Connect.CartLine source, CartLine destination)
        {
            var result = this.catalogService.GetProduct(source.Product.ProductId);
            if (result.Success && result.Data != null)
            {
                destination.Product = result.Data;
                destination.Variant = result.Data.Variants?.FirstOrDefault(
                    x => x.VariantId == (source.Product as CommerceCartProduct)?.ProductVariantId);
            }
        }
    }
}