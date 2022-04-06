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

import { keyMirrorReducer, keyMirrorSaga } from 'Foundation/ReactJss';

export const KEYWORD_PARAMETER_NAME = 'q';
export const FACET_PARAMETER_NAME = 'f';
export const DEFAULT_START_PAGE_NUMBER = 0;
export const DEFAULT_ITEMS_PER_PAGE = '12';
export const PRODUCTS_PER_PAGE = 12;

export const FACET_SEPARATOR = '&';
export const FACET_NAME_VALUE_SEPARATOR = '=';
export const FACET_VALUE_SEPARATOR = '|';

export const ACTION_TYPES_NAMESPACE = 'PRODUCTS_SEARCH';

export const SORTING_COOKIE_EXPIRATION_TIME = 86400e6;

export const productsSearchReducerActionTypes = keyMirrorReducer(
  {
    CLEAR_ITEMS: null,
    PRODUCTS_SEARCH_FAILURE: null,
    PRODUCTS_SEARCH_REQUEST: null,
    PRODUCTS_SEARCH_SUCCESS: null,
    RESET_STATE: null,
    UPDATE_APPLIED_FACET: null,
    UPDATE_SORTING_TYPE: null,

    GET_PRODUCTS_BY_IDS_REQUEST: null,
    GET_PRODUCTS_BY_IDS_FAILURE: null,
    GET_PRODUCTS_BY_IDS_SUCCESS: null,
  },
  ACTION_TYPES_NAMESPACE,
);

export const productsSearchSagaActionTypes = keyMirrorSaga(
  {
    APPLY_FACET: null,
    DISCARD_FACET: null,

    CHANGE_SORTING: null,
    CLEAR_SEARCH: null,
    INIT_SEARCH: null,
    LOAD_MORE: null,

    GET_PRODUCTS_BY_IDS: null,
  },
  ACTION_TYPES_NAMESPACE,
);

export const ACTION_TYPES_NAMESPACE_SUGGESTIONS = 'PRODUCTS_SEARCH_SUGGESTIONS';

export const productsSearchSuggestionsReducerActionTypes = keyMirrorReducer(
  {
    RESET_STATE: null,

    PRODUCTS_SEARCH_SUGGESTIONS_FAILURE: null,
    PRODUCTS_SEARCH_SUGGESTIONS_REQUEST: null,
    PRODUCTS_SEARCH_SUGGESTIONS_SUCCESS: null,
  },
  ACTION_TYPES_NAMESPACE_SUGGESTIONS,
);

export const productsSearchSuggestionsSagaActionTypes = keyMirrorSaga(
  {
    REQUEST_SUGGESTIONS: null,
  },
  ACTION_TYPES_NAMESPACE_SUGGESTIONS,
);
