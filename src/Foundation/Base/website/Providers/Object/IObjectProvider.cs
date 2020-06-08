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

namespace HCA.Foundation.Base.Providers.Object
{
    /// <summary>
    /// Provides objects from configuration
    /// </summary>
    public interface IObjectProvider
    {
        /// <summary>
        /// Gets instance of specified type from configuration
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="configPath">Config path to object node</param>
        /// <returns>Instance of T type</returns>
        T GetObject<T>(string configPath) where T : class;
    }
}