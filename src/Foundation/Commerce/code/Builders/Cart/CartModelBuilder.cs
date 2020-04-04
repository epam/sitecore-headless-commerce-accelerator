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

namespace Wooli.Foundation.Commerce.Builders.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using DependencyInjection;

    using Mappers;

    using Models.Checkout;

    using Providers;

    using Services.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Diagnostics;

    [Service(typeof(ICartModelBuilder))]
    public class CartModelBuilder : ICartModelBuilder
    {
        private readonly ICatalogService catalogService;

        private readonly ICurrencyProvider currencyProvider;

        private readonly IEntityMapper entityMapper;

        public CartModelBuilder(
            ICatalogService catalogService,
            IEntityMapper entityMapper,
            ICurrencyProvider currencyProvider)
        {
            this.catalogService = catalogService;
            this.entityMapper = entityMapper;
            this.currencyProvider = currencyProvider;
        }

        public CartModel Initialize(Cart model)
        {
            return this.Initialize<CartModel>(model);
        }

        public TResult Initialize<TResult>(Cart model)
            where TResult : CartModel, new()
        {
            Assert.ArgumentNotNull(model, nameof(model));

            var result = new TResult();
            result.Id = model.ExternalId;

            result.CartLines = model.Lines.Select(this.Initialize).ToList();
            result.Adjustments = model.Adjustments?.Select(a => a.Description).ToList() ?? new List<string>();
            result.Addresses = model.Parties?.Select(x => this.entityMapper.MapToAddress(x)).ToList()
                ?? new List<AddressModel>();
            result.Payments = model.Payment?.Select(x => this.entityMapper.MapToFederatedPayment(x)).ToList()
                ?? new List<FederatedPaymentModel>();
            result.Shippings = model.Shipping?.Select(x => this.entityMapper.MapToShippingMethodModel(x)).ToList()
                ?? new List<ShippingMethodModel>();

            var price = new CartPriceModel();
            price.Initialize(model.Total, this.currencyProvider);
            result.Price = price;

            result.Email = model.Email;

            result.Temp = model;

            return result;
        }

        public CartLineModel Initialize(CartLine model)
        {
            Assert.ArgumentNotNull(model, nameof(model));

            var result = new CartLineModel();

            result.Id = model.ExternalCartLineId;

            var commerceCartProduct = model.Product as CommerceCartProduct;

            var productResult = this.catalogService.GetProduct(model.Product.ProductId);
            if (productResult.Success && productResult.Data != null)
            {
                result.Product = productResult.Data;
                result.Variant =
                    productResult.Data.Variants?.FirstOrDefault(
                        x => x.VariantId == commerceCartProduct?.ProductVariantId);
                result.Quantity = model.Quantity;
            }

            var price = new CartPriceModel();
            price.Initialize(model.Total, this.currencyProvider);
            result.Price = price;

            result.Temp = model;

            return result;
        }
    }
}