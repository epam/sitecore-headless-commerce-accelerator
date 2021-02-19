﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.SubmitVisitorOrder
{
    using System.Linq;

    using Analytics.Mappers.Contact;
    using Analytics.Models.Entities.Contact;
    using Analytics.Services.Contact;

    using Context;

    using Sitecore.Commerce.Engine.Connect.Pipelines;
    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Pipelines;
    using Sitecore.Commerce.Services.Orders;
    using Sitecore.Diagnostics;

    public class SetAnonymousInfoProcessor : PipelineProcessor
    {
        private const string EmailKey = "Confirmaion";

        private readonly IContactService contactService;

        private readonly IVisitorContext visitorContext;

        private readonly IContactMapper contactMapper;

        public SetAnonymousInfoProcessor(IContactService contactService, IVisitorContext visitorContext, IContactMapper contactMapper)
        {
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));
            Assert.ArgumentNotNull(contactMapper, nameof(contactMapper));
            Assert.ArgumentNotNull(contactService, nameof(contactService));

            this.visitorContext = visitorContext;
            this.contactMapper = contactMapper;
            this.contactService = contactService;
        }

        public override void Process(ServicePipelineArgs args)
        {
            if (this.visitorContext.CurrentUser?.Email != null)
            {
                return;
            }

            var cart = this.GetCartFromArgs(args);

            if (cart?.Total == null)
            {
                return;
            }

            this.contactService.SetEmail(EmailKey, cart.Email);

            var personalInfo = this.GetPersonalInfo(cart);

            if (personalInfo == null)
            {
                return;
            }

            this.contactService.SetPersonalInfo(personalInfo);
        }

        private Cart GetCartFromArgs(ServicePipelineArgs args)
        {
            if (!(args?.Request is SubmitVisitorOrderRequest request))
            {
                return null;
            }

            return request.Cart;
        }

        private PersonalInfo GetPersonalInfo(Cart cart)
        {
            var party = cart.Parties.LastOrDefault();

            return party != null ? this.contactMapper.Map<Party, PersonalInfo>(party) : null;
        }
    }
}