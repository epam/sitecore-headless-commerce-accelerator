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

import { Facet, Product } from 'Foundation/Commerce';
import { ProductSearchSuggestion } from 'Foundation/Commerce/dataModel.Generated';
import { FailureType, LoadingStatus } from 'Foundation/Integration';

import * as actionCreators from './actionCreators';
import {
  productsSearchReducerActionTypes,
  productsSearchSagaActionTypes,
  productsSearchSuggestionsReducerActionTypes,
  productsSearchSuggestionsSagaActionTypes,
} from './constants';
import { Params } from './models';

export const ResetSuggestionsState: actionCreators.ResetSuggestionsState = () => ({
  type: productsSearchSuggestionsReducerActionTypes.RESET_STATE,
});

export const RequestSuggestions: actionCreators.RequestSuggestions = (search) => ({
  payload: {
    search,
  },
  type: productsSearchSuggestionsSagaActionTypes.REQUEST_SUGGESTIONS,
});

export const ProductsSearchSuggestionsRequest: actionCreators.ProductsSearchSuggestionsRequest = (search: string) => ({
  payload: {
    search,
    status: LoadingStatus.Loading,
  },
  type: productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_REQUEST,
});

export const ProductsSearchSuggestionsSuccess: actionCreators.ProductsSearchSuggestionsSuccess = (
  products: ProductSearchSuggestion[],
) => ({
  payload: {
    products,
    status: LoadingStatus.Loaded,
  },
  type: productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_SUCCESS,
});

export const ProductsSearchSuggestionsFailure: FailureType = (error: string) => ({
  payload: {
    error,
    status: LoadingStatus.Failure,
  },
  type: productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_FAILURE,
});

export const InitialSearch: actionCreators.InitialSearch = (payload) => ({
  payload: {
    ...payload,
  },
  type: productsSearchSagaActionTypes.INIT_SEARCH,
});

export const ClearSearch: actionCreators.ClearSearch = () => ({
  type: productsSearchSagaActionTypes.CLEAR_SEARCH,
});

export const LoadMore: actionCreators.LoadMore = () => ({
  type: productsSearchSagaActionTypes.LOAD_MORE,
});

export const ChangeSortingType: actionCreators.ChangeSortingType = (payload) => ({
  payload: {
    ...payload,
  },
  type: productsSearchSagaActionTypes.CHANGE_SORTING,
});

export const ApplyFacet: actionCreators.ApplyFacet = (name: string, value: string, search: string) => ({
  payload: {
    name,
    search,
    value,
  },
  type: productsSearchSagaActionTypes.APPLY_FACET,
});

export const DiscardFacet: actionCreators.DiscardFacet = (name: string, value: string, search: string) => ({
  payload: {
    name,
    search,
    value,
  },
  type: productsSearchSagaActionTypes.DISCARD_FACET,
});

export const ClearItems: actionCreators.ClearItems = () => ({
  type: productsSearchReducerActionTypes.CLEAR_ITEMS,
});

export const ResetState: actionCreators.ResetState = () => ({
  type: productsSearchReducerActionTypes.RESET_STATE,
});

export const ProductsSearchRequest: actionCreators.ProductSearchRequest = (params: Params) => ({
  payload: {
    params,
    status: LoadingStatus.Loading,
  },
  type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_REQUEST,
});

export const ProductsSearchFailure: FailureType = (error: string) => ({
  payload: {
    error,
    status: LoadingStatus.Failure,
  },
  type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_FAILURE,
});

export const ProductsSearchSuccess: actionCreators.ProductSearchSuccess = (
  facets: Facet[],
  items: Product[],
  currentPageNumber: number,
  totalPageCount: number,
  totalItemCount: number,
) => ({
  payload: {
    currentPageNumber,
    facets,
    items,
    status: LoadingStatus.Loaded,
    totalItemCount,
    totalPageCount,
  },
  type: productsSearchReducerActionTypes.PRODUCTS_SEARCH_SUCCESS,
});
