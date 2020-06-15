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

export const FACET_SEPARATOR = '&';
export const FACET_NAME_VALUE_SEPARATOR = '=';
export const FACET_VALUE_SEPARATOR = '|';

export const ACTION_TYPES_NAMESPACE = 'PRODUCTS_SEARCH';

export const reducerActionTypes = keyMirrorReducer(
  {
    CLEAR_ITEMS: null,
    PRODUCTS_SEARCH_FAILURE: null,
    PRODUCTS_SEARCH_REQUEST: null,
    PRODUCTS_SEARCH_SUCCESS: null,
    RESET_STATE: null,
    UPDATE_APPLIED_FACET: null,
  },
  ACTION_TYPES_NAMESPACE
);

export const sagaActionTypes = keyMirrorSaga(
  {
    APPLY_FACET: null,
    DISCARD_FACET: null,

    CLEAR_SEARCH: null,
    INIT_SEARCH: null,
    LOAD_MORE: null,
  },
  ACTION_TYPES_NAMESPACE
);
