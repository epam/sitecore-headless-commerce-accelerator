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

import * as Commerce from 'Foundation/Commerce/client';
import { Action, LoadingStatus, Result } from 'Foundation/Integration/client';
import { ChangeRoute } from 'Foundation/ReactJss/client/SitecoreContext';

import * as Api from './../api/Account';

import * as DataModel from '../../dataModel.Generated';

import * as actions from './actions';
import { sagaActionTypes } from './constants';
import {
  AccountValidationState,
  ChangePasswordPayload,
  CreateAccountPayload,
  UpdateAccountPayload,
  ValidateEmailPayload,
} from './models';
import * as selectors from './selectors';
import { signIn } from './utils';

export function* create(action: Action<CreateAccountPayload>) {
  const payload: DataModel.CreateAccountRequest = action.payload;
  yield put(actions.CreateAccountRequest());

  const { data, error }: Result<Commerce.User> = yield call(Api.createAccount, payload);

  if (error) {
    return yield put(actions.CreateAccountFailure(error.message, error.stack));
  }

  yield put(actions.CreateAccountSuccess(data));

  signIn(payload.email, payload.password);
}

export function* validation(action: Action<ValidateEmailPayload>) {
  const payload: DataModel.ValidateEmailRequest = action.payload;

  const accountValidationState: AccountValidationState = yield select(selectors.accountValidation);

  if (accountValidationState.status === LoadingStatus.Loading) {
    return;
  }

  const { email } = payload;

  if (email === accountValidationState.email) {
    return;
  }

  yield put(actions.AccountValidationRequest(email));
  const { data, error }: Result<Commerce.ValidateEmail> = yield call(Api.emailValidation, payload);

  console.log(error);

  if (error) {
    return yield put(actions.AccountValidationFailure(error.message, error.stack));
  }

  yield put(actions.AccountValidationSuccess(data.invalid, data.inUse));
}

export function* resetValidation() {
  yield put(actions.ResetEmailValidation());
}

export function* changePassword(action: Action<ChangePasswordPayload>) {
  const commerceUser: Commerce.User = yield select(selectors.commerceUser);
  if (!commerceUser) {
    return;
  }

  const payload: DataModel.ChangePasswordRequest = {
    ...action.payload,
    email: commerceUser.email,
  };

  yield put(actions.ChangePasswordRequest());
  const { data, error }: Result<boolean> = yield call(Api.changePassword, payload);

  if (error) {
    return yield put(actions.ChangePasswordFailure(error.message, error.stack));
  }

  if (!data) {
    return yield put(actions.ChangePasswordFailure('can not change password'));
  }

  yield put(actions.ChangePasswordSuccess());
}

export function* verifyCommerceUser() {
  const commerceUser: Commerce.User = yield select(selectors.commerceUser);

  if (!commerceUser) {
    yield put(ChangeRoute('/'));
  }

  if (!commerceUser.customerId) {
    yield put(ChangeRoute('/'));
  }
}

export function* getAddressList() {
  yield put(actions.GetAddressListRequest());

  const { data, error }: Result<Commerce.Address[]> = yield call(Api.getAddressList);

  if (error || !data) {
    return yield put(actions.GetAddressListFailure(error.message, error.stack));
  }

  yield put(actions.GetAddressListSuccess(data));
}

export function* updateAddress(action: Action<Commerce.Address>) {
  const payload: DataModel.AddressRequest = action.payload;

  yield put(actions.UpdateAddressRequest());

  const { data, error }: Result<Commerce.Address[]> = yield call(Api.updateAddress, payload);

  if (error || !data) {
    return yield put(actions.UpdateAddressFailure(error.message, error.stack));
  }

  yield put(actions.UpdateAddressSuccess(data));
}

export function* addAddress(action: Action<Commerce.Address>) {
  const payload: DataModel.AddressRequest = action.payload;

  yield put(actions.AddAddressRequest());

  const { data, error }: Result<Commerce.Address[]> = yield call(Api.addAddress, payload);

  if (error || !data) {
    return yield put(actions.AddAddressFailure(error.message, error.stack));
  }

  yield put(actions.AddAddressSuccess(data));
}

export function* removeAddress(action: Action<string>) {
  const { payload } = action;

  yield put(actions.UpdateAddressRequest());

  const { data, error }: Result<Commerce.Address[]> = yield call(Api.removeAddress, payload);

  if (error || !data) {
    return yield put(actions.UpdateAddressFailure(error.message, error.stack));
  }

  yield put(actions.UpdateAddressSuccess(data));
}

export function* updateAccount(action: Action<UpdateAccountPayload>) {
  const commerceUser: Commerce.User = yield select(selectors.commerceUser);
  if (!commerceUser) {
    return;
  }

  const payload: DataModel.UpdateAccountRequest = {
    ...action.payload,
    contactId: commerceUser.contactId,
  };

  yield put(actions.UpdateAccountRequest());

  const { data, error }: Result<boolean> = yield call(Api.updateAccountInfo, payload);

  if (!data && !error) {
    return yield put(actions.UpdateAddressFailure(error.message, error.stack));
  }

  yield put(actions.UpdateAccountSuccess());

  // we want to reload commerce user properties
  yield put(ChangeRoute(window.location.pathname));
}

function* watch() {
  yield takeEvery(sagaActionTypes.ACCOUNT_VALIDATION, validation);
  yield takeEvery(sagaActionTypes.RESET_VALIDATION, resetValidation);
  yield takeEvery(sagaActionTypes.ADDRESS_GET_LIST, getAddressList);
  yield takeEvery(sagaActionTypes.ADDRESS_UPDATE, updateAddress);
  yield takeEvery(sagaActionTypes.ADDRESS_ADD, addAddress);
  yield takeEvery(sagaActionTypes.ADDRESS_REMOVE, removeAddress);
  yield takeEvery(sagaActionTypes.CREATE, create);
  yield takeEvery(sagaActionTypes.UPDATE, updateAccount);
  yield takeEvery(sagaActionTypes.CHANGE_PASSWORD, changePassword);

  yield takeLatest(sagaActionTypes.VERIFY_COMMERCE_USER, verifyCommerceUser);
}

export default [watch()];
