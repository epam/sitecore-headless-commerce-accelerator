using System.Collections.Generic;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Core.Common.Settings
{
    public class HcaTestsDataSettings
    {
        public IEnumerable<ProductTestsDataSettings> Products { get; set; }

        public HcaPagination Pagination { get; set; }

        public IEnumerable<HcaUserTestsDataSettings> Users { get; set; }

        public IEnumerable<HcaPromotionTestsDataSettings> Promotions { get; set; }
    }
}