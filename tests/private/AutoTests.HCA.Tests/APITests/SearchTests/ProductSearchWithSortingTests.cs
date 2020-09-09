using System;
using System.Linq;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Catalog;
using AutoTests.HCA.Core.API.HcaApi.Models.Entities.Search;
using NUnit.Framework;

namespace AutoTests.HCA.Tests.APITests.SearchTests
{
    public class ProductSearchWithSortingTests : BaseProductSearchTest
    {
        private static readonly (string, Func<Product, object>)[] SortingFieldsTestData =
        {
            ("DisplayName", x => x.DisplayName),
            ("Brand", x => x.Brand),
            ("ProductId", x => x.ProductId)
        };

        [Test(Description = "Find products and ORDER(asc/desc) by default field(BRAND).")]
        public void T1_GETProductRequest_SortType_CorrectList(
            [Values(SortDirection.Asc, SortDirection.Desc)]
            SortDirection sortDirection)
        {
            // Arrange
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortDirection = sortDirection
            };

            // Act
            var response = Product.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();
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
        public void T2_GETProductRequest_InvalidSortType_BadRequest()
        {
            // Arrange
            const string expMessage = "The field SortDirection is invalid.";
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortDirection = SortDirection.Invalid
            };

            // Act
            var response = Product.SearchProducts(searchOptions);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessage); });
        }

        [Test(Description = "Find products and ORDER result by SORT FIELD.")]
        [Pairwise]
        public void T3_GETProductRequest_SortField_CorrectList(
            [ValueSource(nameof(SortingFieldsTestData))]
            (string, Func<Product, object>) sortField)
        {
            // Arrange
            var (nameField, field) = sortField;
            var searchOptions = new ProductSearchOptionsRequest
            {
                PageSize = DefPagination.PageSize,
                SortField = nameField
            };

            // Act
            var response = Product.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();
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
        public void T4_GETProductRequest_InvalidSortField_CorrectList()
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
            var response = Product.SearchProducts(searchOptions);

            // Assert
            response.CheckUnSuccessfulResponse();
            Assert.Multiple(() => { response.VerifyErrors(expMessage); });
        }

        [Test(Description = "Find products and ORDER(asc/desc) result by SORT FIELD.")]
        [Pairwise]
        public void T5_GETProductRequest_SortTypeAndSortField_CorrectList(
            [Values(SortDirection.Asc, SortDirection.Desc)]
            SortDirection sortDirection,
            [ValueSource(nameof(SortingFieldsTestData))]
            (string, Func<Product, object>) sortField)
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
            var response = Product.SearchProducts(searchOptions);

            // Assert
            response.CheckSuccessfulResponse();
            var data = response.OkResponseData.Data;
            Assert.Multiple(() =>
            {
                response.VerifyOkResponseData();

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