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

namespace HCA.Foundation.Commerce.Managers.Contact
{
    using System;
    using System.Linq;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using DependencyInjection;

    using Mappers.Contact;

    using Models.Entities.Contact;

    using Sitecore.Analytics;
    using Sitecore.Diagnostics;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IContactManager), Lifetime = Lifetime.Singleton)]
    public class ContactManager : IContactManager
    {
        private const string IdentificationSource = "ContactManager";

        private readonly ILogService<CommonLog> logService;

        private readonly IContactMapper contactMapper;

        public ContactManager(IContactMapper contactMapper, ILogService<CommonLog> logService)
        {
            Assert.ArgumentNotNull(contactMapper, nameof(contactMapper));
            Assert.ArgumentNotNull(logService, nameof(logService));

            this.contactMapper = contactMapper;
            this.logService = logService;
        }

        public bool SetEmail(string key, string email)
        {
            Assert.ArgumentNotNull(key, nameof(key));
            Assert.ArgumentNotNull(email, nameof(email));

            return this.Execute(() => this.SetPreferredEmail(key, email));
        }

        public bool SetPersonalInfo(PersonalInfo info)
        {
            Assert.ArgumentNotNull(info, nameof(info));

            return this.Execute(() => this.UpdatePersonalInformation(info));
        }

        private IdentifiedContactReference GetCurrentContactReference()
        {
            if (!Tracker.Current.Contact.Identifiers.Any())
            {
                var identifier = Guid.NewGuid().ToString();
                Tracker.Current.Session.IdentifyAs(IdentificationSource, identifier);
                return new IdentifiedContactReference(IdentificationSource, identifier);
            }
            else
            {
                var identifier = Tracker.Current.Contact.Identifiers.First();
                return new IdentifiedContactReference(identifier.Source, identifier.Identifier);
            }
        }

        private void SetPreferredEmail(string key, string email)
        {
            var reference = this.GetCurrentContactReference();

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = client.Get(reference, new ExpandOptions(EmailAddressList.DefaultFacetKey));

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

        private void UpdatePersonalInformation(PersonalInfo info)
        {
            var reference = this.GetCurrentContactReference();

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = client.Get(reference, new ContactExpandOptions(PersonalInformation.DefaultFacetKey));

                var personalInfoFacet = contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
                personalInfoFacet = personalInfoFacet == null
                    ? this.contactMapper.Map<PersonalInfo, PersonalInformation>(info)
                    : this.contactMapper.MapToPersonalInformation(info, personalInfoFacet);

                client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfoFacet);

                client.Submit();
            }
        }

        private bool Execute(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception e)
            {
                this.logService.Error(e.Message);
                return false;
            }
        }
    }
}