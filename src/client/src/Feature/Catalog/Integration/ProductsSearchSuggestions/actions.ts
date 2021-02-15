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

import { ProductSearchSuggestion } from 'Foundation/Commerce/dataModel.Generated';
import { FailureType, LoadingStatus } from 'Foundation/Integration';

import * as actionCreators from './actionCreators';
import { reducerActionTypes, sagaActionTypes } from './constants';

export const ResetState: actionCreators.ResetState = () => ({
  type: reducerActionTypes.RESET_STATE,
});

export const RequestSuggestions: actionCreators.RequestSuggestions = (search) => ({
  payload: {
    search,
  },
  type: sagaActionTypes.REQUEST_SUGGESTIONS,
});

export const ProductsSearchSuggestionsRequest: actionCreators.ProductsSearchSuggestionsRequest = (search: string) => ({
  payload: {
    search,
    status: LoadingStatus.Loading,
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_REQUEST,
});

export const ProductsSearchSuggestionsSuccess: actionCreators.ProductsSearchSuggestionsSuccess = (
  products: ProductSearchSuggestion[],
) => ({
  payload: {
    products,
    status: LoadingStatus.Loaded,
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_SUCCESS,
});

export const ProductsSearchSuggestionsFailure: FailureType = (error: string) => ({
  payload: {
    error,
    status: LoadingStatus.Failure,
  },
  type: reducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_FAILURE,
});
