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

import { call, put, takeEvery } from 'redux-saga/effects';

import { Action, Result } from 'Foundation/Integration';

import { actionTypes } from './constants';

import {
  FindStoresFailure,
  FindStoresRequest,
  FindStoresSuccess,
  GetStoresFailure,
  GetStoresRequest,
  GetStoresSuccess,
} from './actions';

import * as api from '../api/StoreLocator';

export function* findStores(request: Action<api.FindStoresPayload>) {
  yield put(FindStoresRequest());

  const { data: getCoordinatesData, error: getCoordinatesError }: Result<api.GetCoordinatesResults[]> = yield call(
    api.getCoordinates,
    request.payload.zipCode,
    request.payload.countryCode,
  );

  if (!getCoordinatesData || getCoordinatesError) {
    return yield put(FindStoresFailure(getCoordinatesError.message, getCoordinatesError.stack));
  }

  const { data, error }: Result<any> = yield call(
    api.findStores,
    getCoordinatesData[0].lat,
    getCoordinatesData[0].lon,
    request.payload.radius,
  );

  if (error) {
    return yield put(FindStoresFailure(error.message, error.stack));
  }

  yield put(FindStoresSuccess(data.locations));
}

export function* getStores(request: Action<any>) {
  yield put(GetStoresRequest());

  const { data, error }: Result<any> = yield call(api.getStores);

  if (error) {
    return yield put(GetStoresFailure(error.message, error.stack));
  }

  yield put(GetStoresSuccess(data.locations));
}

function* watch() {
  yield takeEvery(actionTypes.FIND_STORES_STORE_LOCATOR, findStores);
  yield takeEvery(actionTypes.GET_STORES_STORE_LOCATOR, getStores);
}

export default [watch()];
