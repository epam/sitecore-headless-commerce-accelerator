using System;
using AutoTests.HCA.Core.Common.Entities.Models;

namespace AutoTests.HCA.Core.Common.Helpers
{
    public static class DistanceHelper
    {
        private const double R = 6371;  // Earth's radius

        public static double GetDistanceBetweenTwoPointsEarthSurface(Coordinates coordinate1, Coordinates coordinate2)
        {
            var lat1 = GetCoordinateInRadian(coordinate1.Latitude);
            var lon1 = GetCoordinateInRadian(coordinate1.Longitude);
            var lat2 = GetCoordinateInRadian(coordinate2.Latitude);
            var lon2 = GetCoordinateInRadian(coordinate2.Longitude);

            var sin1 = Math.Sin((lat2 - lat1) / 2);
            var sin2 = Math.Sin((lon2 - lon1) / 2);
            return 2 * R * Math.Asin(Math.Sqrt(sin1 * sin1 + Math.Cos(lat1) * Math.Cos(lat2) * sin2 * sin2));
        }

        private static double GetCoordinateInRadian(double value)
        {
            return value / (180 / Math.PI);
        }
    }
}
