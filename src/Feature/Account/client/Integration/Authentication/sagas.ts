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

import { VoidResult } from 'Foundation/Base/client/dataModel.Generated';
import { User } from 'Foundation/Commerce/client';
import { Action, Result } from 'Foundation/Integration/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import * as AuthenticationApi from '../api/Authentication';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import { AuthenticationPayload } from './models';
import * as selectors from './selectors';

export function* initAuthentication() {
  const commerceUser: User = yield select(selectors.commerceUser);

  yield put(actions.SetAuthenticated(!!commerceUser && !!commerceUser.contactId));
}

export function* resetState() {
  yield put(actions.ResetAuthenticationProcessState());
}

export function* authentication(action: Action<AuthenticationPayload>) {
  const { payload } = action;
  const { email, password, returnUrl } = payload;
  if (!email && !password) {
    return;
  }

  yield put(actions.AuthenticationRequest());

  const { error }: Result<VoidResult> = yield call(AuthenticationApi.authentication, email, password);

  if (error) {
    return yield put(actions.AuthenticationFailure());
  }

  yield put(actions.AuthenticationSuccess());
  yield put(ChangeRoute(returnUrl || '/'));
}

function* watch() {
  yield takeEvery(sagaActionTypes.AUTHENTICATION, authentication);
  yield takeLatest(sagaActionTypes.INIT_AUTHENTICATION, initAuthentication);
  yield takeEvery(sagaActionTypes.RESET_STATE, resetState);
}

export default [watch()];
