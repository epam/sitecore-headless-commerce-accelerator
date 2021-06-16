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

import * as Commerce from 'Foundation/Commerce/dataModel.Generated';
import { LoadingStatus } from 'models';

export type SuggestionProduct = Commerce.ProductSearchSuggestion;

export type ProductsSearchSuggestionsState = {
  products: SuggestionProduct[];
  status: LoadingStatus;
};

export type SuggestionsRequestPayload = {
  search: string;
};

export type ProductsSearchSuggestionsRequestPayload = {
  search: string;
  status: LoadingStatus;
};

export type ProductsSearchSuggestionsSuccessPayload = {
  products: SuggestionProduct[];
  status: LoadingStatus;
};

export type SearchProductsParams = {
  /**
   * searchKeyword
   */
  q?: string;
  /**
   * page
   */
  pg?: number;
  /**
   * facetValues
   */
  f?: string;
  /**
   * sortField
   */
  s?: string;
  /**
   * pageSize
   */
  ps?: string;
  /**
   * sortDirection
   */
  sd?: string;
  /**
   * currentCatalogItemId
   */
  cci?: string;
  /**
   * currentItemId
   */
  ci?: string;
};

export type SearchProductsResponse = {
  data: Commerce.ProductSearchResults;
  status: string;
};

export type ProductSearchSuggestionResponse = {
  products: Commerce.ProductSearchSuggestion[];
};

export interface Status {
  status: LoadingStatus;
}

export interface Facets extends Commerce.Facet {}
export interface Product extends Commerce.Product {}
export interface Params extends SearchProductsParams {}
export interface ProductSearchState extends Status {
  facets: Facets[];
  items: Product[];
  params: Params;
  currentPageNumber: number;
  totalPageCount?: number;
  totalItemCount?: number;
}

export interface ProductsSearchRequestPayload extends Status {
  params: Params;
}

export interface ProductsSearchSuccessPayload extends Status {
  facets: Facets[];
  items: Product[];
  currentPageNumber: number;
  totalPageCount: number;
}

export interface FacetPayload {
  search: string;
  name: string;
  value: string;
}

export interface AppliedFacets {
  [name: string]: string[];
}

export interface ChangeSortingTypePayload {
  sortingDirection?: string;
  sortingField?: string;
}

export interface InitSearchPayload extends ChangeSortingTypePayload {
  categoryId?: string;
  search?: string;
}

export interface SearchState {
  productSearch: ProductSearchState;
  productSearchSuggestion: ProductsSearchSuggestionsState;
}

export interface GlobalSearchState {
  search: {
    productSearch: ProductSearchState;
    productSearchSuggestion: ProductsSearchSuggestionsState;
  };
}
