using System;
using System.Collections.Generic;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    [Description("Product search pagination Tests")]
    public class ProductSearchPaginationTests : BaseProductSearchTest
    {
        [TestCase(10, null, Description = "Get the first 10 products")]
        [TestCase(200, 2, Description = "Get 200 products from 2 page")]
        [TestCase(200, 3, Description = "Get 200 products from 3 page")]
        public void T1_GETProductRequest_ValidParams_CorrectList(int pageSize, int? pageNumber)
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
            response.CheckSuccessfulResponse();
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

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

        [TestCase(-1, 1, new[] {"The field PageSize must be between 1 and 2147483647."})]
        [TestCase(1, -1, new[] {"The field PageNumber must be between 0 and 2147483647."})]
        [TestCase(-1, -1, new[]
        {
            "The field PageNumber must be between 0 and 2147483647.",
            "The field PageSize must be between 1 and 2147483647."
        }, Description = "Get 200 products from 3 page")]
        public void T2_GETProductRequest_InvalidParams_BadRequest(int pageSize, int pageNumber,
            IEnumerable<string> expMessages)
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
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessages); });
        }
    }
}