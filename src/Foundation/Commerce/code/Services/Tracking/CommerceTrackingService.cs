namespace Wooli.Foundation.Commerce.Services.Tracking
{
    using System.Diagnostics.CodeAnalysis;
    using DependencyInjection;
    using Sitecore.Commerce;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ICommerceTrackingService), Lifetime = Lifetime.Transient)]
    public class CommerceTrackingService : ICommerceTrackingService
    {
        public void EndVisit(bool clearVisitor)
        {
            CommerceTracker.Current.EndVisit(clearVisitor);
        }

        public void IdentifyAs(string source, string userName, string shopName = null, bool syncUserFacet = true)
        {
            CommerceTracker.Current.IdentifyAs(source, userName, shopName, syncUserFacet);
        }
    }
}