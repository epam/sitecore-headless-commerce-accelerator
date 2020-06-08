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

namespace HCA.Foundation.Commerce.Models.Entities.Shipping
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using TypeLite;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class ShippingMethod
    {
        public string Description { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public List<string> LineIds { get; set; }

        public string PartyId { get; set; }

        public string ShippingPreferenceType { get; set; }
    }
}