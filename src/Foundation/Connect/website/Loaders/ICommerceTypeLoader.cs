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

namespace HCA.Foundation.Connect.Loaders
{
    /// <summary>
    /// Proxy for static CommerceTypeLoader
    /// </summary>
    public interface ICommerceTypeLoader
    {
        /// <summary>Creates the instance.</summary>
        /// <typeparam name="T">Interface type to be loaded.</typeparam>
        /// <returns>An instance of type T.</returns>
        T CreateInstance<T>();
    }
}