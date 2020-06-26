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

import keyMirror from 'keymirror';

export const actionTypes = keyMirror({
  LOAD_CART: null,

  CART_GET_FAILURE: null,
  CART_GET_REQUEST: null,
  CART_GET_SUCCESS: null,

  ADD_TO_CART: null,
  ADD_TO_CART_FAILURE: null,
  ADD_TO_CART_REQUEST: null,
  ADD_TO_CART_SUCCESS: null,

  UPDATE_CART_LINE: null,
  UPDATE_CART_LINE_FAILURE: null,
  UPDATE_CART_LINE_REQUEST: null,
  UPDATE_CART_LINE_SUCCESS: null,

  REMOVE_CART_LINE: null,
  REMOVE_CART_LINE_FAILURE: null,
  REMOVE_CART_LINE_REQUEST: null,
  REMOVE_CART_LINE_SUCCESS: null,

  ADD_PROMO_CODE: null,
  ADD_PROMO_CODE_FAILURE: null,
  ADD_PROMO_CODE_REQUEST: null,
  ADD_PROMO_CODE_SUCCESS: null,
});
