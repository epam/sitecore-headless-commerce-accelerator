//    Copyright 2021 EPAM Systems, Inc.
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

namespace HCA.Foundation.Search.Models.Entities.Product
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Common;

    [ExcludeFromCodeCoverage]
    public class ProductSearchOptions : SearchOptions
    {
        public Guid CategoryId { get; set; }

        public string ProductName { get; set; }
    }
}