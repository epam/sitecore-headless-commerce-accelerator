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

    /// <summary>
    /// Builds product entities from TSource
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IProductBuilder<in TSource>
        where TSource : class
    {
        /// <summary>
        /// Builds product entity from TSource
        /// </summary>
        /// <param name="source">Source model</param>
        /// <returns></returns>
        Product Build(TSource source);

        /// <summary>
        /// Builds enumerable of product entities without variants from TSource
        /// </summary>
        /// <param name="sources">Source model</param>
        /// <returns></returns>
        IEnumerable<Product> BuildWithoutVariants(IEnumerable<TSource> sources);
    }
}