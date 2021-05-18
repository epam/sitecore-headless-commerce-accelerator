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

import { combineReducers } from 'redux';

import { Action, LoadingStatus } from 'Foundation/Integration';

import { productsSearchReducerActionTypes, productsSearchSuggestionsReducerActionTypes } from './constants';
import { ProductSearchState, ProductsSearchSuggestionsState, SearchState } from './models';

export const ProductsSearchSuggestionsInitialState: ProductsSearchSuggestionsState = {
  products: [],
  status: LoadingStatus.NotLoaded,
};

const productSearchSuggestionReducer = (
  state: ProductsSearchSuggestionsState = { ...ProductsSearchSuggestionsInitialState },
  action: Action,
) => {
  switch (action.type) {
    case productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_FAILURE:
    case productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_REQUEST:
    case productsSearchSuggestionsReducerActionTypes.PRODUCTS_SEARCH_SUGGESTIONS_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }

    case productsSearchSuggestionsReducerActionTypes.RESET_STATE: {
      return { ...ProductsSearchSuggestionsInitialState };
    }

    default:
      return state;
  }
};

export const ProductSearchInitialState: ProductSearchState = {
  currentPageNumber: 0,
  facets: [],
  items: [],
  params: {},
  status: LoadingStatus.NotLoaded,
};

const productSearchReducer = (state: ProductSearchState = { ...ProductSearchInitialState }, action: Action) => {
  switch (action.type) {
    case productsSearchReducerActionTypes.PRODUCTS_SEARCH_FAILURE:
    case productsSearchReducerActionTypes.PRODUCTS_SEARCH_REQUEST:
    case productsSearchReducerActionTypes.PRODUCTS_SEARCH_SUCCESS:
    case productsSearchReducerActionTypes.UPDATE_APPLIED_FACET:
    case productsSearchReducerActionTypes.UPDATE_SORTING_TYPE: {
      return {
        ...state,
        ...action.payload,
      };
    }
    case productsSearchReducerActionTypes.CLEAR_ITEMS: {
      return {
        ...state,
        items: [],
      };
    }
    case productsSearchReducerActionTypes.RESET_STATE: {
      return { ...ProductSearchInitialState };
    }
    default:
      return state;
  }
};

export const initialState: SearchState = {
  productSearch: ProductSearchInitialState,
  productSearchSuggestion: ProductsSearchSuggestionsInitialState,
};

export default combineReducers<SearchState>({
  productSearch: productSearchReducer,
  productSearchSuggestion: productSearchSuggestionReducer,
});
