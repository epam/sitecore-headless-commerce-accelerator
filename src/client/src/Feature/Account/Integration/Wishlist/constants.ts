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

export const WISHLIST_NAMESPACE = 'WISHLIST';

export const reducerTypes = keyMirrorReducer(
  {
    ADD_WISHLIST_ITEM_FAILURE: null,
    ADD_WISHLIST_ITEM_REQUEST: null,
    GET_WISHLIST_FAILURE: null,
    GET_WISHLIST_REQUEST: null,
    REMOVE_WISHLIST_ITEM_FAILURE: null,
    REMOVE_WISHLIST_ITEM_REQUEST: null,
    WISHLIST_SUCCESS: null,
  },
  WISHLIST_NAMESPACE,
);

export const sagaTypes = keyMirrorSaga(
  {
    ADD_WISHLIST_ITEM: null,
    GET_WISHLIST: null,
    REMOVE_WISHLIST_ITEM: null,
  },
  WISHLIST_NAMESPACE,
);
