using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    [Description("Product search pagination Tests")]
    public class ProductSearchPaginationTests : BaseProductSearchTest
    {
        public static IEnumerable<TestCaseData> GetInvalidPaginationTestsData()
        {
            yield return new TestCaseData(-1, DefPagination.PageNumber,
                new List<string> {"The field PageSize must be between 1 and 2147483647."});
            yield return new TestCaseData(DefPagination.PageSize, -1,
                new List<string> {"The field PageNumber must be between 0 and 2147483647."});
            yield return new TestCaseData(-1, -1,
                new List<string>
                {
                    "The field PageNumber must be between 0 and 2147483647.",
                    "The field PageSize must be between 1 and 2147483647."
                });
        }

        [TestCase(10, null, Description = "Get the first 10 products")]
        [TestCase(200, 2, Description = "Get 200 products from 2 page")]
        [TestCase(200, 3, Description = "Get 200 products from 3 page")]
        public void _01_PaginationWithValidParamsTest(int pageSize, int? pageNumber)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed.");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                var data = response.OkResponseData.Data;
                var expPageCount = (int) Math.Ceiling(data.TotalItemCount / (double) pageSize);
                Assert.NotNull(data, $"Invalid {nameof(response.OkResponseData.Data)}. Expected: NotNull.");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull.");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull.");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty.");
                Assert.AreEqual(expPageCount, data.TotalPageCount);
                Assert.True(data.TotalItemCount <
                            pageSize * (pageNumber.HasValue && pageNumber.Value != 0 ? pageNumber.Value + 1 : 2)
                        ? pageSize > data.Products.Count
                        : pageSize == data.Products.Count,
                    "Pagination is not working correctly.");
            });
        }

        [TestCaseSource(nameof(GetInvalidPaginationTestsData))]
        public void _02_PaginationWithInvalidParamsTest(int pageSize, int pageNumber, IEnumerable<string> expMessage)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            Assert.False(response.IsSuccessful, "The GetProducts POST request is passed.");
            var dataResult = response.Errors;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(HcaStatus.Error, dataResult.Status);
                Assert.AreEqual(expMessage.First(), dataResult.Error,
                    $"Expected {nameof(dataResult.Error)} text: {expMessage}. Actual:{dataResult.Error}.");
                Assert.That(expMessage.All(x => dataResult.Errors.Contains(x)),
                    "The error list does not contain all validation errors");
            });
        }
    }
}