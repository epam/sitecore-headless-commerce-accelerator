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

using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities.Prices;
using Sitecore.Diagnostics;
using TypeLite;
using Wooli.Foundation.Commerce.Providers;

namespace Wooli.Foundation.Commerce.Models.Checkout
{
    [TsClass]
    public class CartPriceModel
    {
        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? Total { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? HandlingTotal { get; set; }

        public decimal? ShippingTotal { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal? TotalSavings { get; set; }


        [TsIgnore] public object Temp { get; set; }

        public void Initialize(Total model, ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(model, nameof(model));
            Assert.ArgumentNotNull(model, nameof(currencyProvider));

            CurrencySymbol = currencyProvider.GetCurrencySymbolByCode(model.CurrencyCode);

            CurrencyCode = model.CurrencyCode;
            Total = model.Amount;
            TaxTotal = model.TaxTotal?.Amount ?? 0;

            var commerceModel = model as CommerceTotal;
            Subtotal = commerceModel?.Subtotal;
            ShippingTotal = commerceModel?.ShippingTotal;
            HandlingTotal = commerceModel?.HandlingTotal;
            TotalSavings = commerceModel?.LineItemDiscountAmount + commerceModel?.OrderLevelDiscountAmount;

            Temp = model;
        }
    }
}