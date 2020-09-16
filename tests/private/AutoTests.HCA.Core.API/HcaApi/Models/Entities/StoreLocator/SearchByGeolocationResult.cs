using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.StoreLocator
{
    public class SearchByGeolocationResult
    {
        public IEnumerable<GeolocationInfo> Locations { get; set; }
    }
}
