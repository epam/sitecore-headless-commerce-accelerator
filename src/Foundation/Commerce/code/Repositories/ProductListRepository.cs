//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace Wooli.Foundation.Commerce.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    using Connect.Context;
    using Connect.Managers;
    using Connect.Models;

    using Context;

    using DependencyInjection;

    using Glass.Mapper.Sc;

    using Models;
    using Models.Catalog;

    using Providers;

    using Sitecore;
    using Sitecore.Commerce.Engine.Connect;
    using Sitecore.Commerce.Engine.Connect.Search.Models;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using ProductModel = Models.Catalog.ProductModel;

    [Service(typeof(IProductListRepository), Lifetime = Lifetime.Singleton)]
    public class ProductListRepository : BaseCatalogRepository, IProductListRepository
    {
        private readonly ISearchInformationProvider searchInformationProvider;

        private readonly ISearchManager searchManager;

        private readonly ISettingsProvider settingsProvider;

        private readonly ISiteContext siteContext;

        private readonly IStorefrontContext storefrontContext;

        public ProductListRepository(
            ISiteContext siteContext,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            ICatalogManager catalogManager,
            ISitecoreService sitecoreService,
            ISearchInformationProvider searchInformationProvider,
            ISettingsProvider settingsProvider,
            ISearchManager searchManager,
            ICurrencyProvider currencyProvider)
            : base(currencyProvider, siteContext, storefrontContext, visitorContext, catalogManager, sitecoreService)
        {
            this.storefrontContext = storefrontContext;
            this.searchInformationProvider = searchInformationProvider;
            this.settingsProvider = settingsProvider;
            this.searchManager = searchManager;
            this.siteContext = siteContext;
        }

        public ProductListResultModel GetProductList(
            IVisitorContext visitorContext,
            string currentItemId,
            string currentCatalogItemId,
            string searchKeyword,
            int? pageNumber,
            NameValueCollection facetValues,
            string sortField,
            int? pageSize,
            SortDirection? sortDirection)
        {
            Assert.ArgumentNotNull(visitorContext, nameof(visitorContext));

            var model = new ProductListResultModel();

            var specifiedCatalogItem = !string.IsNullOrEmpty(currentCatalogItemId)
                ? Context.Database.GetItem(currentCatalogItemId)
                : null;
            var currentCatalogItem = specifiedCatalogItem ?? this.storefrontContext.CurrentCatalogItem;
            model.CurrentCatalogItemId = currentCatalogItem.ID.Guid.ToString("D");

            // var currentItem = Sitecore.Context.Database.GetItem(currentItemId);

            // this.siteContext.CurrentCategoryItem = currentCatalogItem;
            // this.siteContext.CurrentItem = currentItem;
            var searchInformation = this.searchInformationProvider.GetCategorySearchInformation(currentCatalogItem);
            this.GetSortParameters(searchInformation, ref sortField, ref sortDirection);

            var itemsPerPage = this.settingsProvider.GetDefaultItemsPerPage(pageSize, searchInformation);
            var commerceSearchOptions = new CommerceSearchOptions(itemsPerPage, pageNumber.GetValueOrDefault(0));

            this.UpdateOptionsWithFacets(searchInformation.RequiredFacets, facetValues, commerceSearchOptions);
            this.UpdateOptionsWithSorting(sortField, sortDirection, commerceSearchOptions);

            var childProducts = this.GetChildProducts(searchKeyword, commerceSearchOptions, specifiedCatalogItem);
            var productEntityList = this.AdjustProductPriceAndStockStatus(
                visitorContext,
                childProducts,
                currentCatalogItem);

            model.Initialize(commerceSearchOptions, childProducts, productEntityList);
            this.ApplySortOptions(model, commerceSearchOptions, searchInformation);

            return model;
        }

        protected IList<ProductModel> AdjustProductPriceAndStockStatus(
            IVisitorContext visitorContext,
            SearchResults searchResult,
            Item currentCategory)
        {
            var result = new List<ProductModel>();
            var products = new List<Product>();

            if (searchResult.SearchResultItems != null && searchResult.SearchResultItems.Count > 0)
            {
                foreach (var searchResultItem in searchResult.SearchResultItems)
                {
                    var variants = new List<Variant>();
                    var product = new Product(searchResultItem, variants);
                    product.CatalogName = this.StorefrontContext.CatalogName;
                    product.CustomerAverageRating = this.CatalogManager.GetProductRating(searchResultItem);
                    products.Add(product);
                }

                this.CatalogManager.GetProductBulkPrices(products);

                // this.InventoryManager.GetProductsStockStatus(products, currentStorefront.UseIndexFileForProductStatusInLists);
                foreach (var product in products)
                {
                    var productModel = new ProductModel(product.Item);
                    productModel.CurrencySymbol = this.CurrencyProvider.GetCurrencySymbolByCode(product.CurrencyCode);
                    productModel.ListPrice = product.ListPrice;
                    productModel.AdjustedPrice = product.AdjustedPrice;
                    productModel.StockStatusName = product.StockStatusName;
                    productModel.CustomerAverageRating = product.CustomerAverageRating;
                    result.Add(productModel);
                }
            }

            return result;
        }

        protected void ApplySortOptions(
            ProductListResultModel model,
            CommerceSearchOptions commerceSearchOptions,
            CategorySearchInformation searchInformation)
        {
            Assert.ArgumentNotNull(model, nameof(model));
            Assert.ArgumentNotNull(commerceSearchOptions, nameof(commerceSearchOptions));
            Assert.ArgumentNotNull(searchInformation, nameof(searchInformation));

            if (searchInformation.SortFields == null || !searchInformation.SortFields.Any())
            {
                return;
            }

            var sortOptions = new List<SortOptionModel>();
            foreach (var sortField in searchInformation.SortFields)
            {
                var isSelected = sortField.Name.Equals(commerceSearchOptions.SortField);

                var sortOptionAsc = new SortOptionModel
                {
                    Name = sortField.Name,
                    DisplayName = sortField.DisplayName,
                    SortDirection = SortDirection.Asc,
                    IsSelected =
                        isSelected && commerceSearchOptions.SortDirection == CommerceConstants.SortDirection.Asc
                };

                sortOptions.Add(sortOptionAsc);

                var sortOptionDesc = new SortOptionModel
                {
                    Name = sortField.Name,
                    DisplayName = sortField.DisplayName,
                    SortDirection = SortDirection.Desc,
                    IsSelected =
                        isSelected && commerceSearchOptions.SortDirection == CommerceConstants.SortDirection.Desc
                };

                sortOptions.Add(sortOptionDesc);
            }

            model.SortOptions = sortOptions;
        }

        protected SearchResults GetChildProducts(
            string searchKeyword,
            CommerceSearchOptions searchOptions,
            Item categoryItem)
        {
            var searchResults = this.searchManager.GetProducts(
                this.storefrontContext.CatalogName,
                categoryItem?.ID,
                searchOptions,
                searchKeyword);

            return searchResults;
        }

        protected virtual void GetSortParameters(
            CategorySearchInformation categorySearchInformation,
            ref string sortField,
            ref SortDirection? sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                return;
            }

            var sortFields = categorySearchInformation.SortFields;
            if (sortFields == null || sortFields.Count <= 0)
            {
                return;
            }

            sortField = sortFields[0].Name;
            sortOrder = (SortDirection?)CommerceConstants.SortDirection.Asc;
        }

        protected virtual void UpdateOptionsWithFacets(
            IList<CommerceQueryFacet> facets,
            NameValueCollection valueQuery,
            CommerceSearchOptions productSearchOptions)
        {
            if (facets == null || !facets.Any())
            {
                return;
            }

            if (valueQuery != null)
            {
                foreach (string name in valueQuery)
                {
                    var commerceQueryFacet = facets.FirstOrDefault(
                        item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (commerceQueryFacet != null)
                    {
                        var facetValues = valueQuery[name];
                        foreach (var facetValue in facetValues.Split('|'))
                        {
                            commerceQueryFacet.Values.Add(facetValue);
                        }
                    }
                }
            }

            productSearchOptions.FacetFields = facets;
        }

        protected virtual void UpdateOptionsWithSorting(
            string sortField,
            SortDirection? sortDirection,
            CommerceSearchOptions productSearchOptions)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                return;
            }

            productSearchOptions.SortField = sortField;
            if (!sortDirection.HasValue)
            {
                return;
            }

            productSearchOptions.SortDirection = sortDirection == SortDirection.Asc
                ? CommerceConstants.SortDirection.Asc
                : CommerceConstants.SortDirection.Desc;
        }
    }
}