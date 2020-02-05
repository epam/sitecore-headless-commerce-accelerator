//    Copyright 2019 EPAM Systems, Inc.
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

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Glass.Mapper.Sc;
using Sitecore.Commerce.Engine.Connect;
using Sitecore.Commerce.Engine.Connect.Search.Models;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Wooli.Foundation.Commerce.Context;
using Wooli.Foundation.Commerce.Models.Catalog;
using Wooli.Foundation.Commerce.Providers;
using Wooli.Foundation.Connect.Managers;
using Wooli.Foundation.Connect.Models;
using Wooli.Foundation.DependencyInjection;
using ProductModel = Wooli.Foundation.Commerce.Models.Catalog.ProductModel;

namespace Wooli.Foundation.Commerce.Repositories
{
    [Service(typeof(IProductListRepository), Lifetime = Lifetime.Singleton)]
    public class ProductListRepository : BaseCatalogRepository, IProductListRepository
    {
        private readonly ISearchInformationProvider searchInformationProvider;

        private readonly ISearchManager searchManager;

        private readonly ISettingsProvider settingsProvider;

        private readonly ISiteContext siteContext;
        private readonly ISitecoreContext sitecoreContext;

        private readonly IStorefrontContext storefrontContext;

        public ProductListRepository(ISiteContext siteContext,
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            ICatalogManager catalogManager,
            ISitecoreContext sitecoreContext,
            ISearchInformationProvider searchInformationProvider,
            ISettingsProvider settingsProvider,
            ISearchManager searchManager,
            ICurrencyProvider currencyProvider)
            : base(currencyProvider,
                siteContext,
                storefrontContext,
                visitorContext,
                catalogManager,
                sitecoreContext)
        {
            this.sitecoreContext = sitecoreContext;
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

            Item specifiedCatalogItem = !string.IsNullOrEmpty(currentCatalogItemId)
                ? Sitecore.Context.Database.GetItem(currentCatalogItemId)
                : null;
            Item currentCatalogItem = specifiedCatalogItem ?? storefrontContext.CurrentCatalog.Item;
            model.CurrentCatalogItemId = currentCatalogItem.ID.Guid.ToString("D");

            // var currentItem = Sitecore.Context.Database.GetItem(currentItemId);

            // this.siteContext.CurrentCategoryItem = currentCatalogItem;
            // this.siteContext.CurrentItem = currentItem;
            CategorySearchInformation searchInformation =
                searchInformationProvider.GetCategorySearchInformation(currentCatalogItem);
            GetSortParameters(searchInformation, ref sortField, ref sortDirection);

            int itemsPerPage = settingsProvider.GetDefaultItemsPerPage(pageSize, searchInformation);
            var commerceSearchOptions = new CommerceSearchOptions(itemsPerPage, pageNumber.GetValueOrDefault(0));

            UpdateOptionsWithFacets(searchInformation.RequiredFacets, facetValues, commerceSearchOptions);
            UpdateOptionsWithSorting(sortField, sortDirection, commerceSearchOptions);

            SearchResults childProducts = GetChildProducts(searchKeyword, commerceSearchOptions, specifiedCatalogItem);
            IList<ProductModel> productEntityList =
                AdjustProductPriceAndStockStatus(visitorContext, childProducts, currentCatalogItem);

            model.Initialize(commerceSearchOptions, childProducts, productEntityList);
            ApplySortOptions(model, commerceSearchOptions, searchInformation);

            return model;
        }

        protected void ApplySortOptions(ProductListResultModel model, CommerceSearchOptions commerceSearchOptions,
            CategorySearchInformation searchInformation)
        {
            Assert.ArgumentNotNull(model, nameof(model));
            Assert.ArgumentNotNull(commerceSearchOptions, nameof(commerceSearchOptions));
            Assert.ArgumentNotNull(searchInformation, nameof(searchInformation));

            if (searchInformation.SortFields == null || !searchInformation.SortFields.Any()) return;

            var sortOptions = new List<SortOptionModel>();
            foreach (CommerceQuerySort sortField in searchInformation.SortFields)
            {
                bool isSelected = sortField.Name.Equals(commerceSearchOptions.SortField);

                var sortOptionAsc = new SortOptionModel
                {
                    Name = sortField.Name,
                    DisplayName = sortField.DisplayName,
                    SortDirection = SortDirection.Asc,
                    IsSelected = isSelected &&
                                 commerceSearchOptions.SortDirection == CommerceConstants.SortDirection.Asc
                };

                sortOptions.Add(sortOptionAsc);

                var sortOptionDesc = new SortOptionModel
                {
                    Name = sortField.Name,
                    DisplayName = sortField.DisplayName,
                    SortDirection = SortDirection.Desc,
                    IsSelected = isSelected &&
                                 commerceSearchOptions.SortDirection == CommerceConstants.SortDirection.Desc
                };

                sortOptions.Add(sortOptionDesc);
            }

            model.SortOptions = sortOptions;
        }

        protected SearchResults GetChildProducts(string searchKeyword, CommerceSearchOptions searchOptions,
            Item categoryItem)
        {
            SearchResults searchResults = searchManager.GetProducts(storefrontContext.CatalogName, categoryItem?.ID,
                searchOptions, searchKeyword);

            return searchResults;
        }

        protected IList<ProductModel> AdjustProductPriceAndStockStatus(IVisitorContext visitorContext,
            SearchResults searchResult, Item currentCategory)
        {
            var result = new List<ProductModel>();
            var products = new List<Product>();

            if (searchResult.SearchResultItems != null && searchResult.SearchResultItems.Count > 0)
            {
                foreach (Item searchResultItem in searchResult.SearchResultItems)
                {
                    var variants = new List<Variant>();
                    var product = new Product(searchResultItem, variants);
                    product.CatalogName = StorefrontContext.CatalogName;
                    product.CustomerAverageRating = CatalogManager.GetProductRating(searchResultItem);
                    products.Add(product);
                }

                CatalogManager.GetProductBulkPrices(products);
                //this.InventoryManager.GetProductsStockStatus(products, currentStorefront.UseIndexFileForProductStatusInLists);
                foreach (Product product in products)
                {
                    var productModel = new ProductModel();
                    var commerceProductModel = SitecoreContext.Cast<ICommerceProductModel>(product.Item);
                    productModel.Initialize(commerceProductModel);
                    productModel.CurrencySymbol = CurrencyProvider.GetCurrencySymbolByCode(product.CurrencyCode);
                    productModel.ListPrice = product.ListPrice;
                    productModel.AdjustedPrice = product.AdjustedPrice;
                    productModel.StockStatusName = product.StockStatusName;
                    productModel.CustomerAverageRating = product.CustomerAverageRating;
                    result.Add(productModel);
                }
            }

            return result;
        }

        protected virtual void GetSortParameters(CategorySearchInformation categorySearchInformation,
            ref string sortField, ref SortDirection? sortOrder)
        {
            if (!string.IsNullOrWhiteSpace(sortField)) return;

            IList<CommerceQuerySort> sortFields = categorySearchInformation.SortFields;
            if (sortFields == null || sortFields.Count <= 0) return;

            sortField = sortFields[0].Name;
            sortOrder = (SortDirection?) CommerceConstants.SortDirection.Asc;
        }

        protected virtual void UpdateOptionsWithFacets(IList<CommerceQueryFacet> facets, NameValueCollection valueQuery,
            CommerceSearchOptions productSearchOptions)
        {
            if (facets == null || !facets.Any()) return;

            if (valueQuery != null)
                foreach (string name in valueQuery)
                {
                    CommerceQueryFacet commerceQueryFacet = facets.FirstOrDefault(item =>
                        item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (commerceQueryFacet != null)
                    {
                        string facetValues = valueQuery[name];
                        foreach (string facetValue in facetValues.Split('|')) commerceQueryFacet.Values.Add(facetValue);
                    }
                }

            productSearchOptions.FacetFields = facets;
        }

        protected virtual void UpdateOptionsWithSorting(string sortField, SortDirection? sortDirection,
            CommerceSearchOptions productSearchOptions)
        {
            if (string.IsNullOrEmpty(sortField)) return;

            productSearchOptions.SortField = sortField;
            if (!sortDirection.HasValue) return;

            productSearchOptions.SortDirection = sortDirection == SortDirection.Asc
                ? CommerceConstants.SortDirection.Asc
                : CommerceConstants.SortDirection.Desc;
        }
    }
}