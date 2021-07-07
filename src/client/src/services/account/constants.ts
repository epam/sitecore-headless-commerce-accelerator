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

import { keyMirrorReducer, keyMirrorSaga } from 'Foundation/ReactJss';

export const ACCOUNT_NAMESPACE = 'ACCOUNT';

export const sagaActionTypes = keyMirrorSaga(
  {
    ACCOUNT_VALIDATION: null,
    CHANGE_PASSWORD: null,
    CREATE: null,
    DELETE: null,
    RESET_VALIDATION: null,
    UPDATE: null,

    VERIFY_COMMERCE_USER: null,

    ADDRESS_ADD: null,
    ADDRESS_GET_LIST: null,
    ADDRESS_REMOVE: null,
    ADDRESS_UPDATE: null,

    PASSWORD_RESET: null,
    REQUEST_PASSWORD_RESET: null,
  },
  ACCOUNT_NAMESPACE,
);

export const reducerActionTypes = keyMirrorReducer(
  {
    ACCOUNT_VALIDATION_FAILURE: null,
    ACCOUNT_VALIDATION_REQUEST: null,
    ACCOUNT_VALIDATION_SUCCESS: null,
    RESET_EMAIL_VALIDATION: null,

    CHANGE_PASSWORD_FAILURE: null,
    CHANGE_PASSWORD_REQUEST: null,
    CHANGE_PASSWORD_SUCCESS: null,

    CREATE_FAILURE: null,
    CREATE_REQUEST: null,
    CREATE_SUCCESS: null,

    UPDATE_FAILURE: null,
    UPDATE_REQUEST: null,
    UPDATE_SUCCESS: null,

    DELETE_FAILURE: null,
    DELETE_REQUEST: null,
    DELETE_SUCCESS: null,

    ADDRESS_ADD_FAILURE: null,
    ADDRESS_ADD_REQUEST: null,
    ADDRESS_ADD_SUCCESS: null,
    ADDRESS_GET_LIST_FAILURE: null,
    ADDRESS_GET_LIST_REQUEST: null,
    ADDRESS_GET_LIST_SUCCESS: null,
    ADDRESS_REMOVE_FAILURE: null,
    ADDRESS_REMOVE_REQUEST: null,
    ADDRESS_REMOVE_SUCCESS: null,
    ADDRESS_UPDATE_FAILURE: null,
    ADDRESS_UPDATE_REQUEST: null,
    ADDRESS_UPDATE_SUCCESS: null,

    PASSWORD_RESET_FAILURE: null,
    PASSWORD_RESET_REQUEST: null,
    PASSWORD_RESET_SUCCESS: null,
    REQUEST_PASSWORD_RESET_FAILURE: null,
    REQUEST_PASSWORD_RESET_REQUEST: null,
    REQUEST_PASSWORD_RESET_SUCCESS: null,
  },
  ACCOUNT_NAMESPACE,
);
