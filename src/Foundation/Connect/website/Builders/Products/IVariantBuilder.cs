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

namespace HCA.Foundation.Connect.Builders.Products
{
    using System.Collections.Generic;

    using Models.Catalog;

    public interface IVariantBuilder<in TSource>
    {
        /// <summary>
        /// Builds enumerable of variant entities without prices and stock status from TSource
        /// </summary>
        /// <param name="sources">Source model</param>
        /// <returns></returns>
        IEnumerable<Variant> Build(IEnumerable<TSource> sources);
    }
}