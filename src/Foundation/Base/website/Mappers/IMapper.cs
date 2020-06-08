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

namespace HCA.Foundation.Base.Mappers
{
    /// <summary>
    /// Performs mapping operations
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps source object to object of TResult type
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result object</typeparam>
        /// <param name="source">The source object to map</param>
        /// <returns>Mapping result object</returns>
        TResult Map<TSource, TResult>(TSource source);
    }
}