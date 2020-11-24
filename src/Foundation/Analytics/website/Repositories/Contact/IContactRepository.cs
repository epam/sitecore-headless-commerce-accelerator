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

namespace HCA.Foundation.Analytics.Repositories.Contact
{
    using Models.Entities.Contact;
    using Sitecore.XConnect;

    /// <summary>
    /// Performs operation with contact
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Sets email as preferred in current contact
        /// </summary>
        /// <param name="key">Email key</param>
        /// <param name="email">Email</param>
        /// <param name="IdentifiedContactReference">Contact reference</param>
        void SetEmail(string key, string email, IdentifiedContactReference reference);

        /// <summary>
        /// Sets personal info in current contact
        /// </summary>
        /// <param name="info">Personal info</param>
        /// <param name="IdentifiedContactReference">Contact reference</param>
        void SetPersonalInfo(PersonalInfo info, IdentifiedContactReference reference);
    }
}