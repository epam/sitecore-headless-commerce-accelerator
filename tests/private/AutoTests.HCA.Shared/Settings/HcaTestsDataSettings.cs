using System.Collections.Generic;
using AutoTests.HCA.Core.Common.Settings.Product;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Core.Common.Settings
{
    public class HcaTestsDataSettings
    {
        public IEnumerable<ProductTestsDataSettings> Products { get; set; }

        public PaginationTestsDataSettings Pagination { get; set; }

        public IEnumerable<HcaUser> Users { get; set; }

        public IEnumerable<HcaDiscount> Discounts { get; set; }
    }
}