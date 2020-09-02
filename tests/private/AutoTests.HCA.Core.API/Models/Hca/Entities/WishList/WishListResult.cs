using System;
using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.Models.Hca.Entities.WishList
{
    public class WishListResult
    {
        public string Id { get; set; }

        public IEnumerable<WishListLine> Lines { get; set; }

        public string ShopName { get; set; }

        public string Name { get; set; }

        public string CustomerId { get; set; }

        public Guid CustomerIdFacet { get; set; }

        public string UserId { get; set; }

        public Guid UserIdFacet { get; set; }
    }
}