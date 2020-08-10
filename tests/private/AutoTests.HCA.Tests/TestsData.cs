using System.Collections.Generic;
using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Common.Settings;

namespace AutoTests.HCA.Tests
{
    public static class TestsData
    {
        private static readonly ConfigurationManager _configurationManager = new ConfigurationManager("testsdata.json");

        private static HcaTestsDataSettings _hcaTestsData;
        private static UserLogin _userLogin;
        private static IEnumerable<ProductTestsDataSettings> _productId;
        private static PaginationTestsDataSettings _pagination;

        public static HcaTestsDataSettings HcaTestsData =>
            _hcaTestsData ??= _configurationManager.Get<HcaTestsDataSettings>("HcaTestsData");

        public static UserLogin UserLogin => _userLogin ??= HcaTestsData.UserLogin;
        public static IEnumerable<ProductTestsDataSettings> Products => _productId ??= HcaTestsData.Products;
        public static PaginationTestsDataSettings Pagination => _pagination ??= HcaTestsData.Pagination;
    }
}