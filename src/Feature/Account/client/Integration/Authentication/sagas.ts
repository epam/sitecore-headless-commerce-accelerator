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

import { CommerceUserModel, VoidResult } from 'Foundation/Commerce/client';
import { Action, Result } from 'Foundation/Integration/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import * as AuthenticationApi from '../api/Authentication';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import { StartAuthenticationPayload } from './models';
import * as selectors from './selectors';

export function* initAuthentication() {
  const commerceUser: CommerceUserModel = yield select(selectors.commerceUser);

  yield put(actions.SetAuthenticated(!!commerceUser && !!commerceUser.contactId));
}

export function* resetAuthProcessState() {
  yield put(actions.ResetAuthProcessState());
}

export function* startAuthentication(action: Action<StartAuthenticationPayload>) {
  const { payload } = action;
  const { email, password, returnUrl } = payload;
  if (!email && !password) {
    return;
  }

  yield put(actions.StartAuthenticationRequest());

  const { error }: Result<VoidResult> = yield call(AuthenticationApi.startAuthentication, email, password);

  if (error) {
    return yield put(actions.StartAuthenticationFailure());
  }

  yield put(actions.StartAuthenticationSuccess());
  yield put(ChangeRoute(returnUrl || '/'));
}

function* watch() {
  yield takeLatest(sagaActionTypes.INIT_AUTHENTICATION, initAuthentication);
  yield takeEvery(sagaActionTypes.START_AUTHENTICATION, startAuthentication);
}

export default [watch()];
