﻿//    Copyright 2021 EPAM Systems, Inc.
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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Glass.Mapper.Sc.Configuration.Attributes;

    [ExcludeFromCodeCoverage]
    public partial class CountryRegionConfigurationModel
    {
        [SitecoreField(CountriesRegionsFieldName)]
        public virtual IEnumerable<CountryRegionModel> CountryRegionsValues { get; set; }
    }
}