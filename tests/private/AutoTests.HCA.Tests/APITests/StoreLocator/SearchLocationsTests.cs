using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Extensions;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.StoreLocator;
using AutoTests.HCA.Core.Common.Entities.Models;
using AutoTests.HCA.Core.Common.Helpers;
using AutoTests.HCA.Core.Common.Settings.StoreLocators;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.StoreLocator
{
    public class SearchLocationsTests : BaseStoreLocatorTest
    {
        public static readonly HcaStore DefaultStore = TestsData.GetDefaultStore();

        [Test(Description = "The test checks the response model.")]
        public void T1_GETSearchLocationsRequest_ValidParameters_VerifyResponse()
        {
            // Arrange
            var coordinates = new StoreLocationRequest
            {
                Lat = DefaultStore.Coordinates.Latitude,
                Lng = DefaultStore.Coordinates.Longitude,
                Radius = 300
            };

            // Act
            var result = ApiContext.StoreLocator.SearchLocations(coordinates);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                CheckStoresModel(result.OkResponseData.Data.Locations);
            });
        }

        [Test(Description = "The test checks if the stores found are within the specified radius.")]
        public void T2_GETSearchLocationsRequest_ValidParameters_VerifyDistance([Values(100, 300, 1000)] int radius)
        {
            // Arrange
            var coordinates = new StoreLocationRequest
            {
                Lat = DefaultStore.Coordinates.Latitude,
                Lng = DefaultStore.Coordinates.Longitude,
                Radius = radius
            };

            // Act
            var result = ApiContext.StoreLocator.SearchLocations(coordinates);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();

                var locations = result.OkResponseData.Data.Locations;
                ExtendedAssert.NotNullOrEmpty(locations, nameof(locations));
                foreach (var location in locations)
                {
                    var foundCoordinate = new Coordinates(location.Latitude, location.Longitude);
                    var distance = DistanceHelper.GetDistanceBetweenTwoPointsEarthSurface(DefaultStore.Coordinates, foundCoordinate);
                    ExtendedAssert.LessOrEqual(distance, coordinates.Radius.Value, "Distance between shops");
                }
            });
        }

        [Test(Description = "The test checks the response model.")]
        public void T3_GETSearchLocationsRequest_OceanCoordinates_EmptyStoresList()
        {
            // Arrange
            var coordinates = new StoreLocationRequest
            {
                Lat = 31.952162238024975,
                Lng = -46.23046875,
                Radius = 5
            };

            // Act
            var result = ApiContext.StoreLocator.SearchLocations(coordinates);

            // Assert
            result.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyOkResponseData();
                ExtendedAssert.NullOrEmpty(result.OkResponseData.Data.Locations, nameof(result.OkResponseData.Data.Locations));
            });
        }

        [Test(Description = "The test checks if the server returns the correct error for unrecognized required fields.")]
        public void T4_GETSearchLocationsRequest_CoordinatesAreNULL_BadRequest()
        {
            // Arrange
            var coordinates = new StoreLocationRequest();
            var expMessages = new List<string>
            {
                $"The {nameof(coordinates.Lat)} field is required.",
                $"The {nameof(coordinates.Lng)} field is required.",
                $"The {nameof(coordinates.Radius)} field is required."
            };

            // Act
            var result = ApiContext.StoreLocator.SearchLocations(coordinates);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors(expMessages);
            });
        }

        [Test(Description = "The test checks if the server returns the correct error if the fields are incorrect.")]
        public void T5_GETSearchLocationsRequest_InvalidParameters_BadRequest()
        {
            // Arrange
            var coordinate = new StoreLocationRequest
            {
                Lat = double.PositiveInfinity,
                Lng = double.NegativeInfinity,
                Radius = int.MinValue
            };
            var expMessages = new List<string>
            {
                $"The value '{coordinate.Lat}' is not valid for {nameof(coordinate.Lat)}.",
                $"The value '{coordinate.Lng}' is not valid for {nameof(coordinate.Lng)}.",
                $"The field {nameof(coordinate.Radius)} can't be a negative"
            };

            // Act
            var result = ApiContext.StoreLocator.SearchLocations(coordinate);

            // Assert
            result.CheckUnSuccessfulResponse();
            Assert.Multiple(() =>
            {
                result.VerifyErrors(expMessages);
            });
        }
    }
}
