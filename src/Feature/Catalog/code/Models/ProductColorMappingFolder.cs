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

#pragma warning disable 1591
#pragma warning disable 0108

namespace HCA.Feature.Catalog.Models
{
    using System.Collections.Generic;

    using Glass.Mapper.Sc.Configuration.Attributes;

    public partial interface IProductColorMappingFolder
    {
        [SitecoreChildren(InferType = true)]
        IEnumerable<IProductColorMapping> Mappings { get; set; }
    }

    public partial class ProductColorMappingFolder
    {
        [SitecoreChildren(InferType = true)]
        public IEnumerable<IProductColorMapping> Mappings { get; set; }
    }
}