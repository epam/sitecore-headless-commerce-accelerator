using System;
using System.Collections.Generic;
using System.Linq;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    public class ProductSearchByCombinationOfParametersTests : BaseProductSearchTest
    {
        [Test(Description = "Find products by category id & by search keyword")]
        [TestCase("3198ffe4-83d3-a856-6964-62a6a2e9b488", "Smartphone Case", "Sherpa")]
        [TestCase("0bde885c-c3aa-7bf3-9cab-d93a2af168ac", "Health, Beauty and Fitness", "6042944")]
        public void _01_SearchByCategoryIdAndSearchKeywordTest(string categoryId, string categoryName,
            string searchKeyword)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                CategoryId = new Guid(categoryId),
                PageSize = DefPagination.PageSize,
                SearchKeyword = searchKeyword
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                Assert.That(
                    data.Products.All(x =>
                        SearchByKeywordCondition(x, searchKeyword) && SearchByCategoryIdCondition(x, categoryName)),
                    "The products list should contain products of the specified category");
            });
        }

        [Test(Description = "Find products by search keyword with brand facet")]
        [TestCase("Phone", "EXT Accessories")]
        [TestCase("habitat", "Centerpiece Microwaves")]
        public void _02_SearchByKeywordWithFacetTest(string searchKeyword, string facet)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SearchKeyword = searchKeyword,
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
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                Assert.That(data.Products.All(x => x.Brand == facet),
                    $"The list should contain products from the category of \'{facet}\'");
            });
        }
    }
}
