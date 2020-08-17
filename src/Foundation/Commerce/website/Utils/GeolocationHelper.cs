//    Copyright 2020 EPAM Systems, Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace HCA.Foundation.Commerce.Utils
{
    using System;

    using HCA.Foundation.Commerce.Models.Entities.Geolocation;

    public static class GeolocationHelper
    {
        public static bool InRadius(
            this GeolocationInfo item,
            double centerLatitude,
            double centerLongitude,
            double searchRadius,
            double earthRadius)
        {
            return HaversineDistance(
                centerLatitude,
                centerLongitude,
                item.Latitude,
                item.Longitude,
                searchRadius,
                earthRadius);
        }

        // Haversine formula
        public static bool HaversineDistance(
            double latitudeFrom,
            double longitudeFrom,
            double latitudeTo,
            double longitudeTo,
            double searchRadius,
            double earthRadius)
        {
            var lat = ToRadians((latitudeTo - latitudeFrom));
            
            var lng = ToRadians((longitudeTo - longitudeFrom));
            
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(ToRadians(latitudeFrom)) * Math.Cos(ToRadians(latitudeTo)) *
                     Math.Sin(lng / 2) * Math.Sin(lng / 2);
            
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            
            var distance = earthRadius * h2;

            return distance <= searchRadius;
        }

        public static double ToRadians(double angle)
        {
            return (Math.PI * angle) / 180.0;
        }
    }
}