// Copyright 2020 EPAM Systems, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace HCA.Foundation.Analytics.Repositories.Contact
{
    using System;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Mappers.Contact;

    using Models.Entities.Contact;

    using Sitecore.Diagnostics;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IContactRepository), Lifetime = Lifetime.Singleton)]
    public class ContactRepository : IContactRepository
    {
        private readonly ILogService<CommonLog> logService;
        private readonly IContactMapper contactMapper;

        public ContactRepository(IContactMapper contactMapper, ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(contactMapper, nameof(contactMapper));
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.contactMapper = contactMapper;
            this.logService = logService;
        }

        public void SetEmail(string key, string email, IdentifiedContactReference reference)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                //Get() Method is obsolete
                //var contact = client.Get(reference, new ExpandOptions(EmailAddressList.DefaultFacetKey));

                var contact = client.Get(reference, new ContactExecutionOptions(new ContactExpandOptions(EmailAddressList.DefaultFacetKey)));

                var emailAddress = new EmailAddress(email, true);
                var emailAddressList = contact.GetFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey);
                if (emailAddressList == null)
                {
                    emailAddressList = new EmailAddressList(emailAddress, key);
                }
                else
                {
                    emailAddressList.PreferredKey = key;
                    emailAddressList.PreferredEmail = emailAddress;
                }

                client.SetFacet(contact, EmailAddressList.DefaultFacetKey, emailAddressList);

                client.Submit();
            }
        }

        public void SetPersonalInfo(PersonalInfo info, IdentifiedContactReference reference)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                //Get() Method is obsolete
                //var contact = client.Get(reference, new ContactExpandOptions(PersonalInformation.DefaultFacetKey));

                var contact = client.Get(reference, new ContactExecutionOptions(new ContactExpandOptions(PersonalInformation.DefaultFacetKey)));

                var personalInfoFacet = contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
                personalInfoFacet = personalInfoFacet == null
                ? this.contactMapper.Map<PersonalInfo, PersonalInformation>(info)
                : this.contactMapper.MapToPersonalInformation(info, personalInfoFacet);

                client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfoFacet);

                client.Submit();
            }
        }
    }
}