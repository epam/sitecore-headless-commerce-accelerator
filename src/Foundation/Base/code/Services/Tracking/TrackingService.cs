namespace Wooli.Foundation.Base.Services.Tracking
{
    using System.Diagnostics.CodeAnalysis;
    using DependencyInjection;
    using Sitecore.Analytics;

    [ExcludeFromCodeCoverage]
    [Service(typeof(ITrackingService), Lifetime = Lifetime.Transient)]
    public class TrackingService : ITrackingService
    {
        public void EnsureTracker()
        {
            if (!Tracker.IsActive) Tracker.StartTracking();
        }
    }
}
