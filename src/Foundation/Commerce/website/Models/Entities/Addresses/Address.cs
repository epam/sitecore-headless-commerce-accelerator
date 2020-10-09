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

namespace HCA.Foundation.Commerce.Models.Entities.Addresses
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using TypeLite;

    [TsClass]
    [ExcludeFromCodeCoverage]
    public class Address
    {
        public string Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]    
        public string LastName { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]     
        public string Country { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipPostalCode { get; set; }

        public string ExternalId { get; set; }

        public string PartyId { get; set; }

        public bool IsPrimary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}