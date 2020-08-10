using System;
using System.Linq;
using System.Net;
using AutoTests.HCA.Core.API.Models.Hca;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Catalog;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    public class ProductSearchWithSortingTests : BaseProductSearchTest
    {
        public static readonly (string, Func<Product, object>)[] SortingFieldsTestData =
        {
            ("DisplayName", x => x.DisplayName),
            ("Brand", x => x.Brand),
            ("ProductId", x => x.ProductId)
        };

        [Test(Description = "Find products and ORDER(asc/desc) by default field(BRAND).")]
        public void _01_AcsOrDescOrderTest([Values(SortDirection.Asc, SortDirection.Desc)] SortDirection sortDirection)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortDirection = sortDirection
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                var expectedOrderCollection = sortDirection == SortDirection.Asc
                    ? data.Products.OrderBy(x => x.Brand).ToList()
                    : data.Products.OrderByDescending(x => x.Brand).ToList();
                Assert.That(
                    expectedOrderCollection.Zip(data.Products, (x, y) => x.ProductId == y.ProductId).All(x => x),
                    $"Collection must be sorted. Expected sort type: {sortDirection}");
            });
        }

        [Test(Description = "Find products and ORDER by INVALID sort type.")]
        public void _02_AcsOrDescOrderTest()
        {
            // Arrange
            const string expMessage = "The field SortDirection is invalid.";
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortDirection = SortDirection.Invalid
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var errorResponse = response.Errors;
            Assert.False(response.IsSuccessful, "The GetProducts POST request is passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(HcaStatus.Error, errorResponse.Status);

                Assert.AreEqual(HcaStatus.Error, errorResponse.Status);
                Assert.AreEqual(expMessage, errorResponse.Error,
                    $"Expected {nameof(errorResponse.Error)} text: {expMessage}. Actual:{errorResponse.Error}.");
                Assert.That(errorResponse.Errors.Count == 1 && errorResponse.Errors.All(x => x == expMessage),
                    "The error list does not contain all validation errors");
            });
        }

        [Test(Description = "Find products and ORDER result by SORT FIELD.")]
        [Pairwise]
        public void _03_OrderBySortFieldTest([ValueSource(nameof(SortingFieldsTestData))] (string, Func<Product, object>) sortField)
        {
            // Arrange
            var (nameField, field) = sortField;
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortField = nameField
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request is not passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                var expectedOrderCollection = data.Products.OrderBy(field).ToList();
                Assert.That(
                    expectedOrderCollection.Zip(data.Products, (x, y) => x.ProductId == y.ProductId).All(x => x),
                    $"Collection must be sorted by {nameField} field.");
            });
        }

        [Test(Description = "Find products and order result by INVALID search field.")]
        public void _04_OrderByInvalidSortFieldTest()
        {
            // Arrange
            const string expMessage = "Invalid Sort Field.";
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                CategoryId = new Guid(DefProductTestsData.ProductCategoryId),
                SortField = "InvalidSortField"
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var errorResponse = response.Errors;
            Assert.False(response.IsSuccessful, "The invalid GetProducts POST request is passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.AreEqual(HcaStatus.Error, errorResponse.Status);

                Assert.AreEqual(HcaStatus.Error, errorResponse.Status);
                Assert.AreEqual(expMessage, errorResponse.Error,
                    $"Expected {nameof(errorResponse.Error)} text: {expMessage}. Actual:{errorResponse.Error}.");
                Assert.That(errorResponse.Errors.Count == 1 && errorResponse.Errors.All(x => x == expMessage),
                    "The error list doesn't contain all validation errors");
            });
        }

        [Test(Description = "Find products and ORDER(asc/desc) result by SORT FIELD.")]
        [Pairwise]
        public void _05_OrderAcsOrDescBySortFieldTest([Values(SortDirection.Asc, SortDirection.Desc)] SortDirection sortDirection,
            [ValueSource(nameof(SortingFieldsTestData))] (string, Func<Product, object>) sortField)
        {
            // Arrange
            var (nameField, field) = sortField;
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortDirection = sortDirection,
                SortField = nameField
            };

            // Act
            var response = HcaService.SearchProducts(searchOptions);

            // Assert
            var data = response.OkResponseData.Data;
            Assert.True(response.IsSuccessful, "The GetProducts POST request isn't passed");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual(HcaStatus.Ok, response.OkResponseData.Status);
                Assert.NotNull(data, $"Invalid {nameof(data)}. Expected: NotNull");
                Assert.NotNull(data.Facets, $"Invalid {nameof(data.Facets)}. Expected: NotNull");
                Assert.NotNull(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotNull");
                Assert.IsNotEmpty(data.Products, $"Invalid {nameof(data.Products)}. Expected: NotEmpty");
                var expectedOrderCollection = sortDirection == SortDirection.Asc
                    ? data.Products.OrderBy(field).ToList()
                    : data.Products.OrderByDescending(field).ToList();
                Assert.That(
                    expectedOrderCollection.Zip(data.Products, (x, y) => x.ProductId == y.ProductId).All(x => x),
                    $"Collection must be sorted. Expected sort type: {sortDirection}");
            });
        }

        //TODO test with invalid sort direction and invalid type of sort.
    }
}
