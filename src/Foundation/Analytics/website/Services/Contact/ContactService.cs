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

namespace HCA.Foundation.Analytics.Services.Contact
{
    using System;

    using DependencyInjection;

    using Base.Services.Tracking;

    using Analytics.Models.Entities.Contact;
    using Analytics.Repositories.Contact;

    using Base.Models.Logging;
    using Base.Services.Logging;

    using Sitecore.Diagnostics;

    [Service(typeof(IContactService), Lifetime = Lifetime.Singleton)]
    public class ContactService : IContactService
    {
        private readonly ITrackingService trackingService;
        private readonly ILogService<CommonLog> logService;
        private readonly IContactRepository contactRepository;

        public ContactService(ITrackingService trackingService, ILogService<CommonLog> logService, IContactRepository contactRepository)
        {
            Assert.ArgumentNotNull(trackingService, nameof(trackingService));
            Assert.ArgumentNotNull(logService, nameof(logService));
            Assert.ArgumentNotNull(contactRepository, nameof(contactRepository));

            this.trackingService = trackingService;
            this.logService = logService;
            this.contactRepository = contactRepository;
        }

        public bool SetEmail(string key, string email)
        {
            Assert.ArgumentNotNull(key, nameof(key));
            Assert.ArgumentNotNull(email, nameof(email));

            return this.Execute(
                () =>
                {
                    var reference = this.trackingService.GetCurrentContactReference();
                    this.contactRepository.SetEmail(key, email, reference);
                });
        }

        public bool SetPersonalInfo(PersonalInfo info)
        {
            Assert.ArgumentNotNull(info, nameof(info));

            return this.Execute(
                () =>
                {
                    var reference = this.trackingService.GetCurrentContactReference();
                    this.contactRepository.SetPersonalInfo(info, reference);
                });
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