using System;
using System.Linq;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Catalog;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.BaseTests;
using AutoTests.HCA.Core.Common.Settings;
using AutoTests.HCA.Core.Common.Settings.Products;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture(Description = "Base product search Test")]
    [ApiTest]
    public class BaseProductSearchTest : BaseHcaApiTest
    {
        protected readonly IHcaApiService HcaService = TestsHelper.CreateHcaApiClient();

        protected static readonly ProductTestsDataSettings DefProductTestsData = TestsData.Products.First();
        protected static readonly HcaPagination DefPagination = TestsData.Pagination;

        protected static readonly Func<Product, string, bool> SearchByKeywordCondition = (product, text) =>
            product.ProductId.Contains(text) || product.DisplayName.Contains(text,
                                                 StringComparison.InvariantCultureIgnoreCase)
                                             || product.Tags.Any(y =>
                                                 y.Contains(text, StringComparison.InvariantCultureIgnoreCase));

        protected static readonly Func<Product, string, bool> SearchByCategoryIdCondition = (product, text) =>
            product.DisplayName.Contains(text, StringComparison.InvariantCultureIgnoreCase)
            || product.Tags.Any(y => y.Contains(text, StringComparison.InvariantCultureIgnoreCase)
                                     || product.Brand.Contains(text, StringComparison.InvariantCultureIgnoreCase)
                                     || text.Split(' ').Any(y => product.Description.Contains(y)));
    }
}