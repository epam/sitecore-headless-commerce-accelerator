using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.HCA.Core.Common.Settings;
using AutoTests.HCA.Core.Common.Settings.Product;
using AutoTests.HCA.Core.Common.Settings.Users;

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

        public static HcaUser GetUser(HcaUserRole role = HcaUserRole.User, HcaUserType type = HcaUserType.Default)
        {
            return Users.FirstOrDefault(x => x.Type == type && x.Role == role);
        }

        public static ProductTestsDataSettings GetProduct(HcaProductStatus? status = HcaProductStatus.InStock)
        {
            var filteringList = Products.Where(x => x.Status == status);
            return Products.ElementAt(new Random().Next(filteringList.Count() - 1));
        }
    }
}