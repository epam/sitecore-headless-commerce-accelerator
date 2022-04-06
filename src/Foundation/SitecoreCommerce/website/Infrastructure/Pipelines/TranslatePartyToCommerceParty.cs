using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Pipelines;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using AddPartiesRequest = Sitecore.Commerce.Services.Customers.AddPartiesRequest;
using UpdatePartiesRequest = Sitecore.Commerce.Services.Customers.UpdatePartiesRequest;

namespace HCA.Foundation.SitecoreCommerce.Infrastructure.Pipelines
{
    public class TranslatePartyToCommerceParty : PipelineProcessor<ServicePipelineArgs>
    {
        public override void Process(ServicePipelineArgs args)
        {
            Assert.ArgumentNotNull(args.Request, nameof(args.Request));
            if (args.Request is AddPartiesRequest) 
            {
                (args.Request as AddPartiesRequest).Parties = this.TranslateParties((args.Request as AddPartiesRequest).Parties);
            }
            if (args.Request is UpdatePartiesRequest)
            {
                (args.Request as UpdatePartiesRequest).Parties = this.TranslateParties((args.Request as UpdatePartiesRequest).Parties);
            }
            if (args.Request is CartRequestWithCart)
            {
                (args.Request as CartRequestWithCart).Cart.Parties = this.TranslateParties((args.Request as CartRequestWithCart).Cart.Parties);
                return;
            }
        }

        private List<Party> TranslateParties(IEnumerable<Party> parties) => parties.Select(party => party is CommerceParty ? party : new CommerceParty
        {
            ExternalId = party.ExternalId,
            PartyId = party.PartyId,
            FirstName = party.FirstName,
            LastName = party.LastName,
            Email = party.Email,
            Company = party.Company,
            Address1 = party.Address1,
            Address2 = party.Address2,
            ZipPostalCode = party.ZipPostalCode,
            City = party.City,
            State = party.State,
            Country = party.Country,
            PhoneNumber = party.PhoneNumber,
            Facet = party.Facet,
            RegionCode = party.State,
            UserProfileAddressId = party.ContainsKey(Constants.Party.UserProfileAddressId) ? (Guid)party.GetPropertyValue(Constants.Party.UserProfileAddressId) : default,
            CountryCode = party.ContainsKey(Constants.Party.CountryCode) ? party.GetPropertyValue(Constants.Party.CountryCode) as string : null,
            EveningPhoneNumber = party.ContainsKey(Constants.Party.EveningPhoneNumber) ? party.GetPropertyValue(Constants.Party.EveningPhoneNumber) as string : null,
            FaxNumber = party.ContainsKey(Constants.Party.FaxNumber) ? party.GetPropertyValue(Constants.Party.FaxNumber) as string : null,
            Name = party.ContainsKey(Constants.Party.Name) ? party.GetPropertyValue(Constants.Party.Name) as string : null,
            RegionName = party.ContainsKey(Constants.Party.RegionName) ? party.GetPropertyValue(Constants.Party.RegionName) as string : null,
        }).ToList();
    }
}