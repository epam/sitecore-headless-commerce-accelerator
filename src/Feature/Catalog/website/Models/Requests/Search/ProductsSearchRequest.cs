﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Feature.Catalog.Models.Requests.Search
{
    using DTO;
    using Foundation.Search.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class ProductsSearchRequest
    {
        public string SearchKeyword { get; set; }

        public IEnumerable<string> ProductIds { get; set; }

        public IEnumerable<FacetDto> Facets { get; set; }

        public Guid CategoryId { get; set; }

        public string SortField { get; set; }

        [EnumDataType(typeof(SortDirection))]
        public SortDirection? SortDirection { get; set; }

        [Range(0, int.MaxValue)]
        public int? PageNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }
    }
}