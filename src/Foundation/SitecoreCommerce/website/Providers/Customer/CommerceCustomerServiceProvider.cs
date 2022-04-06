using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Services.Customers;
using System.Linq;

namespace HCA.Foundation.SitecoreCommerce.Providers.Customer
{
    public class CommerceCustomerServiceProvider: CustomerServiceProvider
    {
        private readonly ProviderMapper mapper;

        public CommerceCustomerServiceProvider()
        {
            mapper = new ProviderMapper();
        }
        public override AddPartiesResult AddParties(AddPartiesRequest request)
        {
            var req = new AddPartiesRequest(request.CommerceCustomer, request.Parties.Select(p => p is CommerceParty party ? mapper.Map<CommerceParty, Sitecore.Commerce.Engine.Connect.Entities.CommerceParty>(party): p).ToList());
            var res = base.AddParties(req);
            res.Parties = res.Parties.Select(p => p is Sitecore.Commerce.Engine.Connect.Entities.CommerceParty party ? mapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceParty, CommerceParty>(party) : p).ToArray();            
            return res;
        }

        public override GetPartiesResult GetParties(GetPartiesRequest request)
        {
            var res = base.GetParties(request);
            res.Parties = res.Parties.Select(p => p is Sitecore.Commerce.Engine.Connect.Entities.CommerceParty party ? mapper.Map<Sitecore.Commerce.Engine.Connect.Entities.CommerceParty, CommerceParty>(party) : p).ToArray();
            return res;
        }

        public override CustomerResult UpdateParties(UpdatePartiesRequest request)
        {
            var req = new UpdatePartiesRequest(request.CommerceCustomer, request.Parties.Select(p => p is CommerceParty party ? mapper.Map<CommerceParty, Sitecore.Commerce.Engine.Connect.Entities.CommerceParty>(party) : p).ToList());
            return base.UpdateParties(req);
        }
    }
}