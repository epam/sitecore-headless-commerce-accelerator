using System;
using System.Collections.Generic;
using System.Linq;
using HCA.Api.Core.Models.Hca;
using HCA.Api.Core.Models.Hca.Entities.Catalog;
using HCA.Api.Core.Models.Hca.Entities.Search;
using HCA.Api.Core.Services.HcaService;
using NUnit.Framework;

namespace HCA.Tests.APITests.SearchTests
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture, Description("Product search Tests")]
    [ApiTest]
    public class ProductSearchTests : BaseApiTest
    {
        private readonly IHcaApiService _hcaService = new HcaApiService();

        public const int PAGE_SIZE = 5;
        public const string CATEGORY_ID = "8e456d84-4251-dba1-4b86-ce103dedcd02";

        public static readonly (string, Func<Product, object>)[] SortingFieldsTestData =
        {
            ("DisplayName", x => x.DisplayName), 
            ("Brand", x => x.Brand), 
            ("ProductId",x => x.ProductId)
        };

        public static readonly Func<Product, string, bool> SearchByKeywordCondition = (product, text) =>
            product.ProductId.Contains(text) || product.DisplayName.Contains(text, StringComparison.InvariantCultureIgnoreCase)
                                             || product.Tags.Any(y => y.Contains(text, StringComparison.InvariantCultureIgnoreCase));

        public static readonly Func<Product, string, bool> SearchByCategoryIdCondition = (product, text) =>
             product.DisplayName.Contains(text, StringComparison.InvariantCultureIgnoreCase)
             || product.Tags.Any(y => y.Contains(text, StringComparison.InvariantCultureIgnoreCase)
             || product.Brand.Contains(text, StringComparison.InvariantCultureIgnoreCase)
             ||text.Split(' ').Any(y=>product.Description.Contains(y)));


        [TestCase(10, null, Description = "Get the first 10 products")]
        [TestCase(200, 2, Description = "Get 200 products from 2 page")]
        [TestCase(200, 3, Description = "Get 200 products from 3 page")]
        public void _01_PaginationTest(int pageSize, int? pageNumber)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed.");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            var data = response.OkResponseData.Data;
            var expPageCount = (int)Math.Ceiling(data.TotalItemCount / (double)pageSize);
            Assert.NotNull(data, $"Invalid {nameof(response.OkResponseData.Data)}. Expected: NotNull.");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull.");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull.");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty.");
            Assert.AreEqual(expPageCount, data.TotalPageCount);
            Assert.True(data.TotalItemCount < pageSize * (pageNumber.HasValue && pageNumber.Value != 0 ? pageNumber.Value+1 : 2)
                    ? pageSize > data.Products.Count
                    : pageSize == data.Products.Count,
                "Pagination is not working correctly.");
        }

        [Test, Description("Find products by search keyword.")]
        [TestCase("123", Description = "Keyword's a product id.")]
        [TestCase("phone", Description = "Keyword's a product name.")]
        [TestCase("sports", Description = "Keyword's a tag.")]
        public void _02_SearchBySearchKeywordTest(string searchKeyword)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = PAGE_SIZE,
                SearchKeyword = searchKeyword
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(response.OkResponseData.Data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            Assert.That(data.Products.All(x => SearchByKeywordCondition(x, searchKeyword)),
                "Invalid product in collection");
        }

        [Test, Description("Find products by category id")]
        [TestCase("8e456d84-4251-dba1-4b86-ce103dedcd02", "Phone")]
        [TestCase("3198ffe4-83d3-a856-6964-62a6a2e9b488", "Smartphone")]
        [TestCase("57585cf7-7af6-f5e3-eb7e-a57736343091", "Home theater")]
        [TestCase("dc456a04-4709-48a5-fdbb-ead9bd1e957b", "Drone")]
        public void _03_SearchByCategoryIdTest(string categoryId, string categoryName)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                CategoryId = new Guid(categoryId),
                PageSize = PAGE_SIZE
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            Assert.That(data.Products.All(x => SearchByCategoryIdCondition(x, categoryName)),
                $"The products list should contain products of the '{categoryName}' category");
        }

        [Test, Description("Find products by category id & by search keyword")]
        [TestCase("3198ffe4-83d3-a856-6964-62a6a2e9b488", "Smartphone Case", "Sherpa")]
        [TestCase("0bde885c-c3aa-7bf3-9cab-d93a2af168ac", "Health, Beauty and Fitness", "6042944")]
        public void _04_SearchByCategoryIdAndSearchKeywordTest(string categoryId, string categoryName,
             string searchKeyword)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                CategoryId = new Guid(categoryId),
                PageSize = PAGE_SIZE,
                SearchKeyword = searchKeyword
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            Assert.That(data.Products.All(x => SearchByKeywordCondition(x, searchKeyword) && SearchByCategoryIdCondition(x, categoryName)),
                "The products list should contain products of the specified category");
        }

        [Test, Description("Find products by search keyword with brand facet")]
        [TestCase("Phone", "EXT Accessories")]
        [TestCase("habitat", "Centerpiece Microwaves")]
        public void _05_SearchByKeywordWithFacetTest(string searchKeyword, string facet)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = PAGE_SIZE,
                SearchKeyword = searchKeyword,
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Name = "brand",
                        Values = new List<string> { facet }
                    }
                }
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            Assert.That(data.Products.All(x => x.Brand == facet), $"The list should contain products from the category of \'{facet}\'");
        }

        [Test, Description("Find products by category id with brand facet")]
        [TestCase("EXT Accessories")]
        [TestCase("Shark Smartphone Cases")]
        [TestCase("GoGo Prepaid Smartphone")]
        public void _06_SearchByCategoryIdWithFacetTest(string facet)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = PAGE_SIZE,
                CategoryId = new Guid(CATEGORY_ID),
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Name = "brand",
                        Values = new List<string> { facet }
                    }
                }
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.IsNotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            Assert.That(data.Products.All(x => x.Brand == facet), $"The list should contain products from the category of \'{facet}\'");
        }

        [Test, Pairwise, Description("Find products by category and order result by search field")]
        public void _07_SearchAndOrderBySortFieldTest([Values] SortDirection sortDirection,
            [ValueSource(nameof(SortingFieldsTestData))] (string, Func<Product, object>) sortField)
        {
            // Arrange
            var (nameField, field) = sortField;
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = PAGE_SIZE,
                CategoryId = new Guid(CATEGORY_ID),
                SortDirection = sortDirection,
                SortField = nameField
            };

            // Act
            var response = _hcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
            Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
            Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
            Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
            Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
            var expectedOrderCollection = sortDirection == SortDirection.Asc
                ? data.Products.OrderBy(field).ToList()
                : data.Products.OrderByDescending(field).ToList();
            Assert.That(expectedOrderCollection.Zip(data.Products, (x, y) => x.ProductId == y.ProductId).All(x => x),
                $"Collection must be sorted.Expected type of sort. Expected sort type: {sortDirection}");
        }
    }
}
