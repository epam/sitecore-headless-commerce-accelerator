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

namespace HCA.Foundation.Commerce.Mappers.Contact
{
    using Base.Mappers;

    using Models.Entities.Contact;

    using Sitecore.XConnect.Collection.Model;

    /// <summary>
    /// Performs mapping for contact information
    /// </summary>
    public interface IContactMapper : IMapper
    {
        /// <summary>
        /// Maps PersonalInfo to PersonalInformation
        /// </summary>
        /// <param name="info">Source</param>
        /// <param name="information">Destination base object</param>
        /// <returns>Personal information</returns>
        PersonalInformation MapToPersonalInformation(PersonalInfo info, PersonalInformation information);
    }
}