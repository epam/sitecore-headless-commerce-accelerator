using Sitecore.Commerce;
using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities;
using Sitecore.Commerce.Pipelines;
using Sitecore.Commerce.Services.Carts;
using Sitecore.Commerce.Services.Customers;
using System.Collections.Generic;
using System.Linq;
using AddPartiesResult = Sitecore.Commerce.Services.Carts.AddPartiesResult;

namespace HCA.Foundation.SitecoreCommerce.Infrastructure.Pipelines
{
    public class TranslateCommercePartyToParty : PipelineProcessor<ServicePipelineArgs>
    {
        public override void Process(ServicePipelineArgs args)
        {
            Assert.ArgumentNotNull(args.Result, nameof(args.Result));
            
            if (args.Result is AddPartiesResult)
            {
                (args.Result as AddPartiesResult).Parties = this.TranslateParties((args.Result as AddPartiesResult).Parties);                
            }
            if (args.Result is UpdatePartiesResult)
            {
                (args.Result as UpdatePartiesResult).Parties = this.TranslateParties((args.Result as UpdatePartiesResult).Parties);
            }
            if (args.Result is GetPartiesResult)
            {
                (args.Result as GetPartiesResult).Parties = this.TranslateParties((args.Result as GetPartiesResult).Parties);
            }
            if (args.Result is CartResult)
            {
                (args.Result as CartResult).Cart.Parties = this.TranslateParties((args.Result as CartResult).Cart.Parties);
                return;
            }

        }

        private List<Party> TranslateParties(IReadOnlyCollection<Party> parties) => parties.Select(party => {
            if (!(party is CommerceParty))
                return party;

            var commerceParty = party as CommerceParty;
            if (commerceParty.UserProfileAddressId != default) party.SetPropertyValue(Constants.Party.UserProfileAddressId, commerceParty.UserProfileAddressId);
            if (!string.IsNullOrEmpty(commerceParty.CountryCode)) party.SetPropertyValue(Constants.Party.CountryCode, commerceParty.CountryCode);
            if (!string.IsNullOrEmpty(commerceParty.EveningPhoneNumber)) party.SetPropertyValue(Constants.Party.EveningPhoneNumber, commerceParty.EveningPhoneNumber);
            if (!string.IsNullOrEmpty(commerceParty.FaxNumber)) party.SetPropertyValue(Constants.Party.FaxNumber, commerceParty.FaxNumber);
            if (!string.IsNullOrEmpty(commerceParty.Name)) party.SetPropertyValue(Constants.Party.Name, commerceParty.Name);
            if (!string.IsNullOrEmpty(commerceParty.RegionCode)) party.SetPropertyValue(Constants.Party.RegionCode, party.State = commerceParty.RegionCode);
            if (!string.IsNullOrEmpty(commerceParty.RegionName)) party.SetPropertyValue(Constants.Party.RegionName, commerceParty.RegionName);
            return party;
        }).ToList();
    }
}