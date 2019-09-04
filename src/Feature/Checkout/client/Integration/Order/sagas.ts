//    Copyright 2019 EPAM Systems, Inc.
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

import { SagaIterator } from 'redux-saga';
import { call, put, select, takeEvery } from 'redux-saga/effects';

import { Order } from 'Feature/Checkout/client/Integration/api';
import * as Commerce from 'Foundation/Commerce/client';
import { Action, LoadingStatus, Result } from 'Foundation/Integration/client';

import * as actions from './actions';
import { actionTypes } from './actionTypes';
import { OrderRequestPayload } from './models';
import * as selectors from './selectors';

export function* getCurrentOrder(requestData: Action<OrderRequestPayload>) {
  try {
    const stateTrackingNumber = yield select(selectors.currentOrderTrackingNumber);
    const stateStatus: LoadingStatus = yield select(selectors.currentOrderStatus);

    const requestDataModel = requestData.payload;
    if (requestDataModel.requestTrackingNumber === stateTrackingNumber && stateStatus !== LoadingStatus.NotLoaded) {
      return;
    }

    yield put(actions.GetOrderRequest(requestDataModel.requestTrackingNumber));
    const { data, error }: Result<Commerce.OrderModel> = yield call(Order.getOrder, requestDataModel.requestTrackingNumber);

    if (error) {
      return yield put(actions.GetOrderFailure(error.message || 'Error Occured'));
    }

    yield put(actions.GetOrderSuccess(data));
  } catch (e) {
    yield put(actions.GetOrderFailure(e.message));
  }
}

function* watch(): SagaIterator {
  yield takeEvery(actionTypes.GET_ORDER, getCurrentOrder);
}

export default [watch()];
