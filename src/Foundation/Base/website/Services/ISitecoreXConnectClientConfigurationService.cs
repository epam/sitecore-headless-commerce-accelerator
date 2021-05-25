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

namespace HCA.Foundation.Base.Services
{
    using Sitecore.XConnect.Client;

    /// <summary>
    /// Proxy interface for static SitecoreXConnectClientConfiguration class
    /// </summary>
    public interface ISitecoreXConnectClientConfigurationService
    {
        /// <summary>
        /// Get an instance of the xConnect Client API in a Sitecore context
        /// </summary>
        XConnectClient GetClient();
    }
}
