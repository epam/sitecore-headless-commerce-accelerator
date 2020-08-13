using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.HCA.Common.Settings;
using AutoTests.HCA.Common.Settings.Users;

namespace AutoTests.HCA.Tests
{
    public static class TestsData
    {
        private static readonly ConfigurationManager _configurationManager = new ConfigurationManager("testsdata.json");

        private static HcaTestsDataSettings _hcaTestsData;
        private static IEnumerable<HcaUser> _users;
        private static IEnumerable<ProductTestsDataSettings> _productId;
        private static PaginationTestsDataSettings _pagination;

        private static HcaTestsDataSettings HcaTestsData =>
            _hcaTestsData ??= _configurationManager.Get<HcaTestsDataSettings>("HcaTestsData");

        public static IEnumerable<HcaUser> Users => _users ??= HcaTestsData.Users;
        public static IEnumerable<ProductTestsDataSettings> Products => _productId ??= HcaTestsData.Products;
        public static PaginationTestsDataSettings Pagination => _pagination ??= HcaTestsData.Pagination;

        public static HcaUser GetUser(HcaUserType? type = HcaUserType.Default, string id = null)
        {
                return Users.FirstOrDefault(x => x.Type == type);
        }

        public static ProductTestsDataSettings GetDefProduct()
        {
            return Products.FirstOrDefault();
        }
    }
}