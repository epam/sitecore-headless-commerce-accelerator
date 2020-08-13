using System.Collections.Generic;
using AutoTests.HCA.Common.Settings.Users;

namespace AutoTests.HCA.Common.Settings
{
    public class HcaTestsDataSettings
    {
        public IEnumerable<ProductTestsDataSettings> Products { get; set; }

        public PaginationTestsDataSettings Pagination { get; set; }

        public IEnumerable<HcaUser> Users { get; set; }
    }
}
