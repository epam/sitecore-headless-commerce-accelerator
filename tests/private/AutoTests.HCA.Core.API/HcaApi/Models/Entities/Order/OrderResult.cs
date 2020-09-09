using System;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Order
{
    public class OrderResult : CartResult
    {
        public string OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public string TrackingNumber { get; set; }

        public bool? IsOfflineOrder { get; set; }
    }
}