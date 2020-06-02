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

// ReSharper disable CheckNamespace

namespace HCA.Foundation.Connect.Models
{
    using System.Diagnostics.CodeAnalysis;

    using Glass.Mapper.Sc.Configuration.Attributes;

    [ExcludeFromCodeCoverage]
    public partial class StorefrontModel
    {
        private const string CountryRegionConfigurationQuery =
            "./*[@@templatename='" + CountryRegionConfigurationModel.TemplateName + "']";

        private const string CurrenciesDisplayAdjustmentsQuery =
            "./*[@@templatename='" + CurrenciesDisplayAdjustmentsModel.TemplateName + "']";

        private const string PaymentConfigurationQuery =
            "./*[@@templatename='" + PaymentConfigurationModel.TemplateName + "']";

        [SitecoreQuery(CountryRegionConfigurationQuery, IsRelative = true)]
        public virtual CountryRegionConfigurationModel CountriesRegionsSettings { get; set; }

        // TODO: Use it in CurrencyProvider
        [SitecoreQuery(CurrenciesDisplayAdjustmentsQuery, IsRelative = true)]
        public virtual CurrenciesDisplayAdjustmentsModel CurrenciesDisplaySettings { get; set; }

        [SitecoreQuery(PaymentConfigurationQuery, IsRelative = true)]
        public virtual PaymentConfigurationModel PaymentSettings { get; set; }
    }
}