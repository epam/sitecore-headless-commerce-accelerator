using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Configuration;
using AutoTests.HCA.Core.Common.Settings;
using AutoTests.HCA.Core.Common.Settings.Products;
using AutoTests.HCA.Core.Common.Settings.Promotions;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Tests
{
    public static class TestsData
    {
        private static readonly ConfigurationManager _configurationManager = new ConfigurationManager("testsdata.json");

        private static HcaTestsDataSettings _hcaTestsData;
        private static IEnumerable<HcaUserTestsDataSettings> _users;
        private static IEnumerable<ProductTestsDataSettings> _productId;
        private static HcaPagination _pagination;
        private static IEnumerable<HcaPromotionTestsDataSettings> _promotions;

        private static HcaTestsDataSettings HcaTestsData =>
            _hcaTestsData ??= _configurationManager.Get<HcaTestsDataSettings>("HcaTestsData");

        public static IEnumerable<HcaUserTestsDataSettings> Users => _users ??= HcaTestsData.Users;
        public static IEnumerable<ProductTestsDataSettings> Products => _productId ??= HcaTestsData.Products;
        public static HcaPagination Pagination => _pagination ??= HcaTestsData.Pagination;
        public static IEnumerable<HcaPromotionTestsDataSettings> Promotions => _promotions ??= HcaTestsData.Promotions;

        public static ProductTestsDataSettings GetDefaultProduct()
        {
            return GetDefault(Products);
        }

        public static HcaUserTestsDataSettings GetDefaultUser()
        {
            return GetDefault(Users);
        }

        public static HcaPromotionTestsDataSettings GetDefaultPromotion()
        {
            return GetDefault(Promotions);
        }

        public static HcaUserTestsDataSettings GetUser(HcaUserRole role = HcaUserRole.User)
        {
            return Users.FirstOrDefault(x => x.Role == role);
        }

        public static ProductTestsDataSettings GetProduct(HcaProductStatus? status = HcaProductStatus.InStock,
            string productId = null)
        {
            var filteringList = Products.Where(x =>
                x.StockStatus == status && (productId == null || x.ProductId == productId)).ToList();
            return filteringList.ElementAt(new Random().Next(filteringList.Count() - 1));
        }

        public static IEnumerable<ProductTestsDataSettings> GetProducts(int qty)
        {
            return Products.Where(x => x.StockStatus == HcaProductStatus.InStock && x.DefaultVariant).Take(qty);
        }

        public static HcaPromotionTestsDataSettings GetPromotion(HcaPromotionName couponName)
        {
            return Promotions.FirstOrDefault(x => x.Name == couponName);
        }

        private static T GetDefault<T>(IEnumerable<T> collection)
            where T : BaseHcaEntityTestsDataSettings
        {
            return collection.First(x => x.Default);
        }

        public static IEnumerable<ProductTestsDataSettings> GetDifferentVariantProducts()
        {
            var secondaryProduct = Products.First(x => !x.DefaultVariant);
            return Products.Where(x => x.ProductId == secondaryProduct.ProductId);
        }
    }
}