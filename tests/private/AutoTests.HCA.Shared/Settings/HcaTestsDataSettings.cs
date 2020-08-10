using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Models;

namespace AutoTests.HCA.Common.Settings
{
    public class HcaTestsDataSettings
    {
        public IEnumerable<ProductTestsDataSettings> Products { get; set; }

        public PaginationTestsDataSettings Pagination { get; set; }

        public UserLogin UserLogin { get; set; }
    }
}
