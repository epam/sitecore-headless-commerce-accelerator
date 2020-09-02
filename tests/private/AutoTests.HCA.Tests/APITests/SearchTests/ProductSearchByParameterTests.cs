using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    public class ProductSearchByParameterTests : BaseProductSearchTest
    {
        [Test(Description = "Find products by search keyword.")]
        [TestCase("123", Description = "Keyword's a product id.")]
        [TestCase("phone", Description = "Keyword's a product name.")]
        [TestCase("sports", Description = "Keyword's a tag.")]
        public void T1_GETProductRequest_BySearchKeyword_CorrectList(string searchKeyword)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SearchKeyword = searchKeyword
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                Assert.NotNull(data, $"Invalid {nameof(response.OkResponseData.Data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                Assert.That(data.Products.All(x => SearchByKeywordCondition(x, searchKeyword)),
                    "Invalid product in collection");
            });
        }

        [Test(Description = "Find products by category id.")]
        [TestCase("8e456d84-4251-dba1-4b86-ce103dedcd02", "Phone")]
        [TestCase("3198ffe4-83d3-a856-6964-62a6a2e9b488", "Smartphone")]
        [TestCase("57585cf7-7af6-f5e3-eb7e-a57736343091", "Home theater")]
        [TestCase("dc456a04-4709-48a5-fdbb-ead9bd1e957b", "Drone")]
        public void T2_GETProductRequest_ByCategoryId_CorrectList(string categoryId, string categoryName)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                CategoryId = new Guid(categoryId),
                PageSize = DefPagination.PageSize
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                Assert.That(data.Products.All(x => SearchByCategoryIdCondition(x, categoryName)),
                    $"The products list should contain products of the '{categoryName}' category");
            });
        }

        [Test(Description = "Find products by non-existent category id")]
        public void T3_GETProductRequest_ByNonExistentCategoryId_BadRequest()
        {
            // Arrange
            var invalidCategoryId = new Guid("8e456d84-4251-dba1-4b86-c2103dedcd02");
            var expMessage = "Products not found.";
            var searchOptions = new ProductSearchOptionsRequest
            {
                CategoryId = invalidCategoryId,
                PageSize = DefPagination.PageSize
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessage); });
        }

        [Test(Description = "Find products by brand facet")]
        [TestCase("EXT Accessories")]
        [TestCase("Shark Smartphone Cases")]
        [TestCase("GoGo Prepaid Smartphone")]
        public void T4_GETProductRequest_ByFacetTest_CorrectList(string facet)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                Facets = new List<Facet>
                {
                    new Facet
                    {
                        Name = "brand",
                        Values = new List<string> {facet}
                    }
                }
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.IsNotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                Assert.That(data.Products.All(x => x.Brand == facet),
                    $"The list should contain products from the category of \'{facet}\'");
            });
        }
    }
}