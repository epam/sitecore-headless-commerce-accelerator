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

import { Action } from 'models';
import { Facet, Product, ProductSearchSuggestion } from 'services/commerce';

import * as Models from './models';

export type ResetSuggestionsState = () => Action;

export type RequestSuggestions = (search: string) => Action<Models.SuggestionsRequestPayload>;

export type ProductsSearchSuggestionsRequest = (
  search: string,
) => Action<Models.ProductsSearchSuggestionsRequestPayload>;

export type ProductsSearchSuggestionsSuccess = (
  products: ProductSearchSuggestion[],
) => Action<Models.ProductsSearchSuggestionsSuccessPayload>;

export type InitialSearch = (payload: Models.InitSearchPayload) => Action<Models.InitSearchPayload>;
export type ClearSearch = () => Action;
export type LoadMore = () => Action;
export type ChangeSortingType = (payload: Models.ChangeSortingTypePayload) => Action<Models.ChangeSortingTypePayload>;

export type ApplyFacet = (name: string, value: string, search: string) => Action<Models.FacetPayload>;
export type DiscardFacet = (name: string, value: string, search: string) => Action<Models.FacetPayload>;

export type ClearItems = () => Action;
export type ResetState = () => Action;

export type ProductSearchRequest = (params: Models.Params) => Action<Models.ProductsSearchRequestPayload>;

export type ProductSearchSuccess = (
  facets: Facet[],
  items: Product[],
  currentPageNumber: number,
  totalPageCount: number,
  totalItemCount: number,
) => Action<Models.ProductsSearchSuccessPayload>;
