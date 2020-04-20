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

namespace HCA.Foundation.Commerce.Services.Tracking
{
    /// <summary>
    /// Proxy service for static CommerceTracker
    /// </summary>
    public interface ICommerceTrackingService
    {
        /// <summary>Ends the visit.</summary>
        /// <param name="clearVisitor">if set to <c>true</c> visitor is cleared.</param>
        void EndVisit(bool clearVisitor);

        /// <summary>
        /// Identifies current user as given source and user name.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="userName">The username.</param>
        /// <param name="shopName">Name of the shop.</param>
        /// <param name="syncUserFacet">if set to <c>true</c> the contact facet will be synchronized with user information.</param>
        void IdentifyAs(string source, string userName, string shopName = null, bool syncUserFacet = true);
    }
}