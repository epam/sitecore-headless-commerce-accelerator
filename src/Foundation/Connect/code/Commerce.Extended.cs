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

// ReSharper disable CheckNamespace

#pragma warning disable 1591
#pragma warning disable 0108

namespace Wooli.Foundation.Connect.Models
{
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;

    public partial interface ICommerceProductModel
    {
        [SitecoreChildren(InferType = true)] IEnumerable<ICommerceProductVariantModel> Variants { get; set; }
    }

    public partial class CommerceProductModel
    {
        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<ICommerceProductVariantModel> Variants { get; set; }
    }

    public partial interface ICountryRegionModel
    {
        [SitecoreChildren(InferType = true)] IEnumerable<ISubdivisionModel> Subdivisions { get; set; }
    }

    public partial class CountryRegionModel
    {
        [SitecoreChildren(InferType = true)] public IEnumerable<ISubdivisionModel> Subdivisions { get; set; }
    }

    public partial interface IStorefrontModel
    {
        [SitecoreQuery("./#Country-Region configuration#", IsRelative = true)]
        ICountryRegionConfigurationModel CountriesRegionsConfiguration { get; set; }
    }

    public partial class StorefrontModel
    {
        //TODO Fix to relative query
        [SitecoreQuery("./#Country-Region configuration#", IsRelative = true)]
        public ICountryRegionConfigurationModel CountriesRegionsConfiguration { get; set; }
    }

    public partial interface ICountryRegionConfigurationModel
    {
        [SitecoreField("Countries-Regions")] IEnumerable<ICountryRegionModel> CountriesRegionsModel { get; set; }
    }

    public partial class CountryRegionConfigurationModel
    {
        [SitecoreField("Countries-Regions")] public IEnumerable<ICountryRegionModel> CountriesRegionsModel { get; set; }
    }
}