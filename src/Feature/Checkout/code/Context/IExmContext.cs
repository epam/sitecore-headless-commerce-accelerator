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

namespace HCA.Feature.Checkout.Context
{
    /// <summary>
    /// Proxy for static ExmContext
    /// </summary>
    public interface IExmContext
    {
        /// <summary>
        /// Is current request is render request
        /// </summary>
        bool IsRenderRequest { get; }

        /// <summary>
        /// Gets value from query string
        /// </summary>
        /// <param name="key">Value key</param>
        /// <returns>Value</returns>
        string GetValue(string key);
    }
}