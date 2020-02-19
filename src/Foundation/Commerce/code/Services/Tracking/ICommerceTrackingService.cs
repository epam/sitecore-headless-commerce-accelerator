namespace Wooli.Foundation.Commerce.Services.Tracking
{
    /// <summary>
    /// Proxy service for static CommerceTracker
    /// </summary>
    public interface ICommerceTrackingService
    {
        /// <summary>Ends the visit.</summary>
        /// <param name="clearVisitor">if set to <c>true</c> visitor is cleared.</param>
        void EndVisit(bool clearVisitor);

        /// <summary>
        /// Identifies current user as given source and user name.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="userName">The username.</param>
        /// <param name="shopName">Name of the shop.</param>
        /// <param name="syncUserFacet">if set to <c>true</c> the contact facet will be synchronized with user information.</param>
        void IdentifyAs(string source, string userName, string shopName = null, bool syncUserFacet = true);
    }
}