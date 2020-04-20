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
    using System.Diagnostics.CodeAnalysis;

    using DependencyInjection;

    using Sitecore.Commerce;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ICommerceTrackingService), Lifetime = Lifetime.Transient)]
    public class CommerceTrackingService : ICommerceTrackingService
    {
        public void EndVisit(bool clearVisitor)
        {
            CommerceTracker.Current.EndVisit(clearVisitor);
        }

        public void IdentifyAs(string source, string userName, string shopName = null, bool syncUserFacet = true)
        {
            CommerceTracker.Current.IdentifyAs(source, userName, shopName, syncUserFacet);
        }
    }
}