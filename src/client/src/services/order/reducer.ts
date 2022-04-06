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

import { Action, LoadingStatus } from 'models';
import { combineReducers } from 'redux';

import { actionTypes } from './actionTypes';
import { CurrentOrderHistoryState, CurrentOrderState, OrderState } from './models';

export const orderInitialState: CurrentOrderState = {
  status: LoadingStatus.NotLoaded,
  trackingNumber: null,
};

const orderReducer = (state: CurrentOrderState = orderInitialState, action: Action) => {
  switch (action.type) {
    case actionTypes.GET_ORDER:
    case actionTypes.GET_ORDER_REQUEST:
    case actionTypes.GET_ORDER_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};

export const orderHistoryInitialState: CurrentOrderHistoryState = {
  currentPageNumber: 0,
  isLastPage: false,
  status: LoadingStatus.NotLoaded,
};

const orderHistoryReducer = (state: CurrentOrderHistoryState = orderHistoryInitialState, action: Action) => {
  switch (action.type) {
    case actionTypes.ORDER_HISTORY_LOAD_MORE:
    case actionTypes.GET_ORDER_HISTORY:
    case actionTypes.GET_ORDER_HISTORY_REQUEST:
    case actionTypes.GET_ORDER_HISTORY_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};

export const allOrdersInitialState = {
  status: LoadingStatus.NotLoaded,
};

const allOrdersReducer = (state: any = allOrdersInitialState, action: Action) => {
  switch (action.type) {
    case actionTypes.GET_ALL_ORDERS:
    case actionTypes.GET_ALL_ORDERS_FAILURE:
    case actionTypes.GET_ALL_ORDERS_REQUEST:
    case actionTypes.GET_ALL_ORDERS_SUCCESS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    default:
      return state;
  }
};

export const initialState: OrderState = {
  currentOrder: orderInitialState,
  orderHistory: orderHistoryInitialState,
  allOrders: allOrdersInitialState,
};

export default combineReducers<OrderState>({
  currentOrder: orderReducer,
  orderHistory: orderHistoryReducer,
  allOrders: allOrdersReducer,
});
