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

import { FailureType, LoadingStatus } from 'Foundation/Integration';

import * as Commerce from 'Foundation/Commerce';

import * as actionCreators from './actionCreators';
import { actionTypes } from './actionTypes';

export const GetOrder: actionCreators.GetOrder = (trackingNumber: string) => ({
  payload: {
    requestTrackingNumber: trackingNumber
  },
  type: actionTypes.GET_ORDER,
});

export const GetOrderRequest: actionCreators.GetOrderRequest = (trackingNumber: string) => ({
  payload: {
    requestTrackingNumber: null,
    status: LoadingStatus.Loading,
    trackingNumber,
  },
  type: actionTypes.GET_ORDER_REQUEST,
});

export const GetOrderFailure: FailureType = (error: string, stack?) => ({
  payload: {
    error,
    stack,
    status: LoadingStatus.Failure,
  },
  type: actionTypes.GET_ORDER_FAILURE,
});

export const GetOrderSuccess: actionCreators.GetOrderSuccess = (order: Commerce.Order) => ({
  payload: {
    order,
    status: LoadingStatus.Loaded,
  },
  type: actionTypes.GET_ORDER_SUCCESS,
});
