using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.HcaApi.Context;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.StoreLocator;
using AutoTests.HCA.Core.BaseTests;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.StoreLocator
{
    [ApiTest]
    [Parallelizable(ParallelScope.None)]
    public class BaseStoreLocatorTest : BaseHcaApiTest
    {
        protected IHcaApiContext ApiContext { get; private set; }

        [SetUp]
        public void SetUp()
        {
            ApiContext = TestsHelper.CreateHcaApiContext();
        }

        public void CheckStoresModel(IEnumerable<GeolocationInfo> stores)
        {
            ExtendedAssert.NotNullOrEmpty(stores, "locations");
            foreach (var store in stores)
            {
                ExtendedAssert.NotNullOrWhiteSpace(store.Title, nameof(store.Title));
                ExtendedAssert.NotNullOrWhiteSpace(store.Description, nameof(store.Description));
                ExtendedAssert.InRange(store.Latitude, -90, 90, nameof(store.Latitude));
                ExtendedAssert.InRange(store.Longitude, -180, 180, nameof(store.Longitude));
            }
        }
    }
}
