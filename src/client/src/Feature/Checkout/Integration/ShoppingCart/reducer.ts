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

import { Action, LoadingStatus } from 'Foundation/Integration';

import { actionTypes } from './actionTypes';
import { ShoppingCartState } from './models';

export const initialState: ShoppingCartState = {
  actionType: '',
  status: LoadingStatus.NotLoaded,
};

export default (state: ShoppingCartState = initialState, action: Action) => {
  switch (action.type) {
    case actionTypes.CART_GET_FAILURE:
    case actionTypes.CART_GET_REQUEST:
    case actionTypes.CART_GET_SUCCESS:
    case actionTypes.ADD_TO_CART_REQUEST:
    case actionTypes.ADD_TO_CART_SUCCESS:
    case actionTypes.UPDATE_CART_LINE_REQUEST:
    case actionTypes.UPDATE_CART_LINE_SUCCESS:
    case actionTypes.UPDATE_CART_LINE_FAILURE:
    case actionTypes.REMOVE_CART_LINE_REQUEST:
    case actionTypes.REMOVE_CART_LINE_SUCCESS:
    case actionTypes.REMOVE_CART_LINE_FAILURE:
    case actionTypes.ADD_PROMO_CODE_REQUEST:
    case actionTypes.ADD_PROMO_CODE_SUCCESS:
    case actionTypes.ADD_PROMO_CODE_FAILURE: {
      return {
        ...state,
        ...action.payload,
        actionType: action.type,
      };
    }
    default:
      return state;
  }
};
