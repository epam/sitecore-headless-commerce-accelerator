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

import { FailureType, LoadingStatus } from 'Foundation/Integration/client';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';
import { Params } from './models';

import { Facet, Product } from 'Foundation/Commerce/client/dataModel.Generated';

export const InitialSearch: actionCreators.InitialSearch = (payload) => ({
  payload: {
    ...payload,
  },
  type: sagaActionTypes.INIT_SEARCH,
});

export const ClearSearch: actionCreators.ClearSearch = () => ({
  type: sagaActionTypes.CLEAR_SEARCH,
});

export const LoadMore: actionCreators.LoadMore = () => ({
  type: sagaActionTypes.LOAD_MORE,
});

export const ApplyFacet: actionCreators.ApplyFacet = (name: string, value: string, search: string) => ({
  payload: {
    name,
    search,
    value,
  },
  type: sagaActionTypes.APPLY_FACET,
});

export const DiscardFacet: actionCreators.DiscardFacet = (name: string, value: string, search: string) => ({
  payload: {
    name,
    search,
    value,
  },
  type: sagaActionTypes.DISCARD_FACET,
});

export const ClearItems: actionCreators.ClearItems = () => ({
  type: reducerActionTypes.CLEAR_ITEMS,
});

export const ResetState: actionCreators.ResetState = () => ({
  type: reducerActionTypes.RESET_STATE,
});

export const ProductsSearchRequest: actionCreators.ProductSearchRequest = (params: Params) => ({
  payload: {
    params,
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_REQUEST,
});

export const ProductsSearchFailure: FailureType = (error: string) => ({
  payload: {
    error,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_FAILURE,
});

export const ProductsSearchSuccess: actionCreators.ProductSearchSuccess = (
  facets: Facet[],
  items: Product[],
  currentPageNumber: number,
  totalPageCount: number,
  totalItemCount: number
) => ({
  payload: {
    currentPageNumber,
    facets,
    items,
    status: LoadingStatus.Loaded,
    totalItemCount,
    totalPageCount
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_SUCCESS,
});
