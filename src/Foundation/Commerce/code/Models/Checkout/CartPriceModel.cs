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

namespace Wooli.Foundation.Commerce.Models.Checkout
{
    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Diagnostics;

    using TypeLite;

    [TsClass]
    public class CartPriceModel
    {
        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? HandlingTotal { get; set; }

        public decimal? ShippingTotal { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal TaxTotal { get; set; }

        [TsIgnore]
        public object Temp { get; set; }

        public decimal? Total { get; set; }

        public decimal? TotalSavings { get; set; }

        public void Initialize(Total model, ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(model, nameof(model));
            Assert.ArgumentNotNull(model, nameof(currencyProvider));

            this.CurrencySymbol = currencyProvider.GetCurrencySymbolByCode(model.CurrencyCode);

            this.CurrencyCode = model.CurrencyCode;
            this.Total = model.Amount;
            this.TaxTotal = model.TaxTotal?.Amount ?? 0;

            var commerceModel = model as CommerceTotal;
            this.Subtotal = commerceModel?.Subtotal;
            this.ShippingTotal = commerceModel?.ShippingTotal;
            this.HandlingTotal = commerceModel?.HandlingTotal;
            this.TotalSavings = commerceModel?.LineItemDiscountAmount + commerceModel?.OrderLevelDiscountAmount;

            this.Temp = model;
        }
    }
}