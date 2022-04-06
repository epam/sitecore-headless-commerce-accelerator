//    Copyright 2021 EPAM Systems, Inc.
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

import { Action, LoadingStatus } from 'models';

import { actionTypes } from './actionTypes';
import {
  ShoppingCartState,
  UpdateCartItemFailurePayload,
  UpdateCartItemRequestPayload,
  UpdateCartItemSuccessPayload,
} from './models';

export const initialState: ShoppingCartState = {
  actionType: '',
  cartItemsState: {},
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
    case actionTypes.CLEAN_CART_FAILURE:
    case actionTypes.CLEAN_CART_REQUEST:
    case actionTypes.CLEAN_CART_SUCCESS: {
      return {
        ...state,
        ...action.payload,
        actionType: action.type,
      };
    }

    case actionTypes.UPDATE_CART_ITEM_REQUEST: {
      const { productId, variantId } = action.payload as UpdateCartItemRequestPayload;
      const id = `${productId}-${variantId}`;

      return {
        ...state,
        cartItemsState: {
          ...state.cartItemsState,
          [id]: {
            ...state.cartItemsState[id],
            status: LoadingStatus.Loading,
          },
        },
      };
    }

    case actionTypes.UPDATE_CART_ITEM_SUCCESS: {
      const { productId, variantId } = action.payload as UpdateCartItemSuccessPayload;
      const id = `${productId}-${variantId}`;

      return {
        ...state,
        cartItemsState: {
          ...state.cartItemsState,
          [id]: {
            ...state.cartItemsState[id],
            status: LoadingStatus.Loaded,
          },
        },
      };
    }

    case actionTypes.UPDATE_CART_ITEM_FAILURE: {
      const { error, productId, variantId } = action.payload as UpdateCartItemFailurePayload;
      const id = `${productId}-${variantId}`;

      return {
        ...state,
        cartItemsState: {
          ...state.cartItemsState,
          [id]: {
            ...state.cartItemsState[id],
            error,
            status: LoadingStatus.Loaded,
          },
        },
      };
    }

    default:
      return state;
  }
};
