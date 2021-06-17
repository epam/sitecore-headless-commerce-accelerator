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

import { call, put, select, takeEvery, takeLatest } from 'redux-saga/effects';

import { User } from 'services/commerce';
import { Action, Result, VoidResult } from 'models';
import { ChangeRoute } from 'Foundation/ReactJss/SitecoreContext';

import * as AuthenticationApi from './api';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import { AuthenticationPayload, LogoutPayload } from './models';
import * as selectors from './selectors';

import { eventHub, events } from 'services/eventHub';

export function* authentication(action: Action<AuthenticationPayload>) {
  const { payload } = action;
  const { request, returnUrl } = payload;
  if (!request.email && !request.password) {
    return;
  }

  yield put(actions.AuthenticationRequest());

  const { error }: Result<VoidResult> = yield call(AuthenticationApi.authentication, request);

  if (error) {
    return yield put(actions.AuthenticationFailure());
  }

  eventHub.publish(events.AUTHENTICATION.LOGIN);

  yield put(actions.AuthenticationSuccess());
  yield put(ChangeRoute(returnUrl || '/'));
}

export function* initAuthentication() {
  const commerceUser: User = yield select(selectors.commerceUser);

  yield put(actions.SetAuthenticated(!!commerceUser && !!commerceUser.contactId));
}

export function* logout(action: Action<LogoutPayload>) {
  const { returnUrl } = action.payload;
  yield put(actions.LogoutRequest());

  const { error }: Result<VoidResult> = yield call(AuthenticationApi.logout);

  if (error) {
    return yield put(actions.LogoutFailure());
  }

  eventHub.publish(events.AUTHENTICATION.LOGOUT);

  yield put(actions.LogoutSuccess());
  yield put(ChangeRoute(returnUrl || '/'));
}

export function* resetState() {
  yield put(actions.ResetAuthenticationProcessState());
}

function* watch() {
  yield takeEvery(sagaActionTypes.AUTHENTICATION, authentication);
  yield takeLatest(sagaActionTypes.INIT_AUTHENTICATION, initAuthentication);
  yield takeEvery(sagaActionTypes.LOGOUT, logout);
  yield takeEvery(sagaActionTypes.RESET_STATE, resetState);
}

export default [watch()];
