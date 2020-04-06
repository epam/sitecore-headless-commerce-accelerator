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
  SHIPPING_EDIT_ADDRESS: null,

  GET_BILLING_INFO: null,
  GET_BILLING_INFO_FAILURE: null,
  GET_BILLING_INFO_REQUEST: null,
  GET_BILLING_INFO_SUCCESS: null,

  GET_DELIVERY_INFO: null,
  GET_DELIVERY_INFO_FAILURE: null,
  GET_DELIVERY_INFO_REQUEST: null,
  GET_DELIVERY_INFO_SUCCESS: null,

  GET_SHIPPING_INFO: null,
  GET_SHIPPING_INFO_FAILURE: null,
  GET_SHIPPING_INFO_REQUEST: null,
  GET_SHIPPING_INFO_SUCCESS: null,

  UPDATE_SHIPPING: null,
  UPDATE_SHIPPING_FAILURE: null,
  UPDATE_SHIPPING_REQUEST: null,
  UPDATE_SHIPPING_SUCCESS: null,

  UPDATE_SHIPPING_PARAMETERS: null,

  SET_BILLING_ADDRESS: null,
  UPDATE_BILLING: null,

  UPDATE_PAYMENT: null,
  UPDATE_PAYMENT_FAILURE: null,
  UPDATE_PAYMENT_REQUEST: null,
  UPDATE_PAYMENT_SUCCESS: null,

  PLACE_ORDER: null,
  PLACE_ORDER_REQUEST: null,
});
