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

export const AUTHENTICATION_NAMESPACE = 'AUTHENTICATION';

export const reducerActionTypes = keyMirrorReducer(
  {
    AUTHENTICATION_FAILURE: null,
    AUTHENTICATION_REQUEST: null,
    AUTHENTICATION_SUCCESS: null,

    LOGOUT_FAILURE: null,
    LOGOUT_REQUEST: null,
    LOGOUT_SUCCESS: null,

    RESET_AUTHENTICATION_PROCESS_STATE: null,

    SET_AUTHENTICATED: null,
  },
  AUTHENTICATION_NAMESPACE,
);

export const sagaActionTypes = keyMirrorSaga(
  {
    AUTHENTICATION: null,
    INIT_AUTHENTICATION: null,
    LOGOUT: null,
    RESET_STATE: null,
  },
  AUTHENTICATION_NAMESPACE,
);
